using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Navigation;
using SaveSystem;
using Settings;
using Settings.BattleManager;
using Settings.BattleManager.Cards;
using TJ;
using UI.Screens;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        private BattleState _currentBattleState;
        [SerializeField] private CardActions _cardActions;

        private List<Card> _drawPile;
        private List<Card> _cardsInHand;
        private List<Card> _discardPile;
        private int _currentEnergy;
        private int _maxEnergy;

        public Transform topParent;
        public CardUI SelectedCard;

        [SerializeField] private Fighter _cardTarget;
        [SerializeField] private Fighter player;

        [SerializeField] private List<Enemy> _enemies = new List<Enemy>();
        [SerializeField] private Transform _enemyParent;

        public int Energy => _currentEnergy;
        private void Awake()
        {
            Instance = this;
            _cardsInHand = new List<Card>();
            _drawPile = new List<Card>();
        }

        public void StartBattle(EnemyType enemyType)
        {
            BattleScreen.Instance.ChangeTurn(BattleState.PlayerTurn);
            _currentBattleState = BattleState.PlayerTurn;
            var characterData = SaveManager.LoadCharacterData();
            _maxEnergy = characterData.Energy;
            foreach (var card in BattleScreen.Instance.DrawCards())
            {
                _drawPile.Add(card);
            }
            switch (enemyType)
            {
                case EnemyType.Default:
                    int numberOfEnemies = Random.Range(0, 2);
                    //Цикл для нескольких противников
                    _enemies.Add(Instantiate(SettingsProvider.Get<BattlePrefabSet>().GetEnemy<Enemy>(),
                        _enemyParent));
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
                        _enemies[0].GetComponent<Fighter>().currentHealth =
                            (int)(_enemies[0].GetComponent<Fighter>().currentHealth * 0.25);
                    break;
                case RelicType.Anchor:
                    player.AddBlock(10);
                    break;
                case RelicType.Lantern:
                    _maxEnergy += 1;
                    break;
                case RelicType.Marbles:
                    _enemies[0].GetComponent<Fighter>().AddBuff(Buff.Type.Vulnerable, 1);
                    break;
                case RelicType.Bag:
                    DrawCards(2);
                    break;
                case RelicType.Varja:
                    player.AddBuff(Buff.Type.Strength, 1);
                    break;
            }
            _currentEnergy = _maxEnergy;
            DrawCards(5);
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


        public void ShuffleCards()
        {
            _discardPile.AddRange(_cardsInHand);
            _cardsInHand.Clear();

            Utils.Shuffle(_discardPile);
            _drawPile = _discardPile;
            _discardPile = new List<Card>();
        }

        public void DiscardCard(Card card)
        {
            _discardPile.Add(card);
            /*
            discardPileCountText.text = discardPile.Count.ToString();
        */
        }

        public void PlayCard(CardUI cardUI)
        {
            /*foreach (var buffs in _enemies[0].GetComponent<Fighter>().BuffList)
            {
                if (cardUI.card.cardType != Card.CardType.Attack && buffs.BuffType == Buff.Type.Enrage &&
                    buffs.BuffValue > 0)
                    _enemies[0].GetComponent<Fighter>()
                        .AddBuff(Buff.Type.Strength, buffs.BuffValue);
            }

            _cardActions.PerformAction(cardUI.card, _cardTarget);

            _currentEnergy -= cardUI.card.GetCardCostAmount();
            /*energyText.text = energy.ToString();#1#

            Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);
            SelectedCard = null;
            cardUI.gameObject.SetActive(false);
            _cardsInHand.Remove(cardUI.card);
            DiscardCard(cardUI.card);*/
        }

        public void ChangeTurn()
        {
            if (_currentBattleState == BattleState.PlayerTurn)
            {
                _currentBattleState = BattleState.EnemyTurn;
                /*
                endTurnButton.enabled = false;
                */

                ClearHandAndTransferToDiscard();

                foreach (var card in _cardsInHand)
                {
                    var cardUI = card.GetComponent<CardUI>();
                    if (cardUI.gameObject.activeSelf)
                        Instantiate(cardUI.DiscardEffect, cardUI.transform.position, Quaternion.identity, topParent);

                    cardUI.gameObject.SetActive(false);
                    _cardsInHand.Remove(card);
                }


                foreach (Enemy e in _enemies)
                {
                    if (e.thisEnemy == null)
                        e.thisEnemy = e.GetComponent<Fighter>();

                    e.thisEnemy.currentBlock = 0;
                    e.thisEnemy.fighterHealthBar.DisplayBlock(0);
                }

                player.EvaluateBuffsAtTurnEnd();
                StartCoroutine(HandleEnemyTurn());
            }
            else
            {
                foreach (Enemy e in _enemies)
                {
                    e.DisplayIntent();
                }

                _currentBattleState = BattleState.PlayerTurn;
                player.currentBlock = 0;
                player.fighterHealthBar.DisplayBlock(0);
                _currentEnergy = _maxEnergy;
                /*energyText.text = energy.ToString();
                endTurnButton.enabled = true;*/
                DrawCards(5);
                BattleScreen.Instance.ChangeTurn(BattleState.PlayerTurn);
            }
        }


        private IEnumerator HandleEnemyTurn()
        {
            BattleScreen.Instance.ChangeTurn(BattleState.EnemyTurn);

            yield return new WaitForSeconds(1.5f);

            foreach (Enemy enemy in _enemies)
            {
                enemy.midTurn = true;
                enemy.TakeTurn();
                while (enemy.midTurn)
                    yield return new WaitForEndOfFrame();
            }

            Debug.Log("Turn Over");
            ChangeTurn();
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
                    player.currentHealth += 6;
                    if (player.currentHealth > player.maxHealth)
                        player.currentHealth = player.maxHealth;
                    player.UpdateHealthUI(player.currentHealth);
                }

                player.ResetBuffs();
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