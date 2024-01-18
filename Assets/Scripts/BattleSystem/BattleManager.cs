using System;
using System.Collections;
using System.Collections.Generic;
using BattleSystem.Cards;
using BattleSystem.Characters;
using BattleSystem.Characters.Enemy;
using Navigation;
using SaveSystem;
using Settings;
using UI.Screens;
using UnityEngine;
using Random = UnityEngine.Random;

namespace BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        [SerializeField] private CardActions _cardActions;
        [SerializeField] private Fighter _cardTarget;
        [SerializeField] private Fighter _player;
        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
        [SerializeField] private Transform _enemyParent;
        [SerializeField] private Transform _topParent;

        private BattleState _currentBattleState;
        private List<Card> _drawPile;
        private List<Card> _cardsInHand;
        private List<Card> _discardPile;
        private int _currentEnergy;
        private int _maxEnergy;

        public CardUI SelectedCard;
        public int Energy => _currentEnergy;
        public List<Enemy> Enemies => _enemies;
        public Fighter Player => _player;
        public event Action<int> DrawPileCountUpdated;
        public event Action<int> DiscardPileCountUpdated;
        public event Action<int> CurrentEnergyUpdated;


        private void Awake()
        {
            Instance = this;
            _cardsInHand = new List<Card>();
            _drawPile = new List<Card>();
            _discardPile = new List<Card>();
        }

        private void OnEnable()
        {
            DrawPileCountUpdated += BattleScreen.Instance.UpdateDrawPileCountText;
            DiscardPileCountUpdated += BattleScreen.Instance.UpdateDiscardPileCountText;
            CurrentEnergyUpdated += BattleScreen.Instance.UpdateEnergyText;
        }

        private void OnDisable()
        {
            DrawPileCountUpdated -= BattleScreen.Instance.UpdateDrawPileCountText;
            DiscardPileCountUpdated -= BattleScreen.Instance.UpdateDiscardPileCountText;
            CurrentEnergyUpdated -= BattleScreen.Instance.UpdateEnergyText;
        }

        private void UpdateTextValueCount()
        {
            DrawPileCountUpdated?.Invoke(_drawPile.Count);
            DiscardPileCountUpdated?.Invoke(_discardPile.Count);
            CurrentEnergyUpdated?.Invoke(_currentEnergy);
        }

        public void DrawCards(int amountToDraw)
        {
            int cardsDrawn = 0;
            while (cardsDrawn < amountToDraw && _cardsInHand.Count < 10)
            {
                if (_drawPile.Count < 1)
                    ShuffleCards();
                if (_drawPile.Count > 0)
                {
                    AddCardToHand(_drawPile[0]);
                    BattleScreen.Instance.DisplayCardInHand(_drawPile[0]);
                    _drawPile.Remove(_drawPile[0]);
                    cardsDrawn++;
                }
                else
                {
                    Debug.LogWarning("Draw pile is empty!");
                    break;
                }
            }

            BattleScreen.Instance.SortingCardInHand();
            UpdateTextValueCount();
        }


        private void AddCardToHand(Card card)
        {
            if (_cardsInHand.Count < 10)
            {
                _cardsInHand.Add(card);
            }
            else
            {
                Debug.LogWarning("Hand is full, cannot add more cards!");
            }
        }

        private void ClearHandAndTransferToDiscard()
        {
            if (_cardsInHand.Count > 0)
            {
                _discardPile.AddRange(_cardsInHand);
                _cardsInHand.Clear();
            }
        }


        private void ShuffleCards()
        {
            _discardPile.Shuffle();
            _drawPile.AddRange(_discardPile);
            _discardPile = new List<Card>();
        }

        private void DiscardCard(Card card)
        {
            _discardPile.Add(card);
            UpdateTextValueCount();
        }

        private IEnumerator HandleEnemyTurn()
        {
            BattleScreen.Instance.ChangeTurn(BattleState.EnemyTurn, false);

            yield return new WaitForSeconds(1.5f);

            foreach (Enemy enemy in _enemies)
            {
                enemy.MidTurn = true;
                enemy.TakeTurn();
                while (enemy.MidTurn)
                    yield return new WaitForEndOfFrame();
            }

            Debug.Log("Turn Over");
            ChangeTurn();
        }

        public void UpdateEnergy(int count)
        {
            _currentEnergy += count;
            UpdateTextValueCount();
        }

        public void SetTarget(Fighter fighter)
        {
            _cardTarget = fighter;
        }

        public void StartBattle(EnemyType enemyType)
        {
            BattleScreen.Instance.ChangeTurn(BattleState.PlayerTurn, true);
            _currentBattleState = BattleState.PlayerTurn;
            var characterData = SaveManager.LoadCharacterData();
            _maxEnergy = characterData.Energy;
            _drawPile = BattleScreen.Instance.DrawCards();
            _player.Init();
            switch (enemyType)
            {
                case EnemyType.Default:
                    int numberOfEnemies = Random.Range(0, 2);
                    //Цикл для нескольких противников
                    var enemy = Instantiate(SettingsProvider.Get<BattlePrefabSet>().GetEnemy<Enemy>(),
                        _enemyParent);
                    enemy.Init();
                    _enemies.Add(enemy);
                    break;
                case EnemyType.Elite:
                    _enemies.Add(Instantiate(SettingsProvider.Get<BattlePrefabSet>().GetEnemy<Enemy>(),
                        _enemyParent));
                    break;
                case EnemyType.Boss:
                    _enemies.Add(Instantiate(SettingsProvider.Get<BattlePrefabSet>().GetEnemy<Enemy>(),
                        _enemyParent));
                    break;
            }

            switch (characterData.startingRelic.RelicType)
            {
                case RelicType.PreservedInsect:
                    if (enemyType == EnemyType.Elite)
                        _enemies[0].SetupCurrentHealth((int)(_enemies[0].CurrentHealth * 0.25));
                    break;
                case RelicType.Anchor:
                    _player.AddBlock(10);
                    break;
                case RelicType.Lantern:
                    _maxEnergy += 1;
                    break;
                case RelicType.Marbles:
                    _enemies[0].GetComponent<Fighter>().AddBuff(BuffType.Vulnerable, 1);
                    break;
                case RelicType.Bag:
                    DrawCards(2);
                    break;
                case RelicType.Varja:
                    _player.AddBuff(BuffType.Strength, 1);
                    break;
            }

            _currentEnergy = _maxEnergy;
            _drawPile.Shuffle();
            DrawCards(5);
            UpdateTextValueCount();
            foreach (Enemy e in _enemies)
            {
                e.DisplayIntent();
            }
        }

        public void ChangeTurn()
        {
            if (_currentBattleState == BattleState.PlayerTurn)
            {
                _currentBattleState = BattleState.EnemyTurn;
                ClearHandAndTransferToDiscard();
                BattleScreen.Instance.DiscardCardInHand();
                foreach (Enemy enemy in _enemies)
                {
                    if (enemy == null)
                    {
                        enemy.AddBlock(-enemy.CurrentBlock);
                        enemy.HealthBar.DisplayBlock(0);
                    }
                }

                _player.EvaluateBuffsAtTurnEnd();
                UpdateTextValueCount();
                StartCoroutine(HandleEnemyTurn());
            }
            else
            {
                foreach (Enemy e in _enemies)
                {
                    e.DisplayIntent();
                }

                _currentBattleState = BattleState.PlayerTurn;
                _player.AddBlock(-_player.CurrentBlock);
                _player.HealthBar.DisplayBlock(0);
                _currentEnergy = _maxEnergy;
                DrawCards(5);
                UpdateTextValueCount();
                BattleScreen.Instance.ChangeTurn(BattleState.PlayerTurn, true);
            }
        }

        public void PlayCard(CardUI cardUI)
        {
            foreach (var buffs in _enemies[0].BuffList)
            {
                if (cardUI.Card.CardType != CardType.Attack && buffs.BuffsType == BuffType.Enrage &&
                    buffs.BuffValue > 0)
                    _enemies[0].AddBuff(BuffType.Strength, buffs.BuffValue);
            }

            _cardActions.PerformAction(cardUI.Card, _cardTarget);
            _currentEnergy -= cardUI.Card.GetCardEnergyAmount();
            SelectedCard = null;
            cardUI.gameObject.SetActive(false);
            DiscardCard(cardUI.Card);
            UpdateTextValueCount();
            _cardsInHand.Remove(cardUI.Card);
        }

        public void RemoveEnemy(Fighter fighter)
        {
            if (fighter is Enemy enemy)
            {
                _enemies.Remove(enemy);
            }
        }

        public void EndFight(BattleState battleState)
        {
            if (battleState == BattleState.Defeat)
                NavigationController.Instance.ScreenTransition<MainScreen>();
            else
            {
                var characterData = SaveManager.LoadCharacterData();
                if (characterData.startingRelic.RelicType == RelicType.BurningBlood)
                {
                    characterData.CurrentHealth += 6;
                    if (characterData.CurrentHealth > characterData.MaxHealth)
                        characterData.CurrentHealth = characterData.MaxHealth;
                    _player.UpdateHealthUI(characterData.CurrentHealth);
                }

                _player.ResetBuffs();
            }
        }
    }

    public enum BattleState
    {
        PlayerTurn,
        EnemyTurn,
        Victory,
        Defeat
    }
}