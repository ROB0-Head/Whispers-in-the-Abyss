using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SaveSystem;
using Settings;
using TJ;
using UI.Screens;
using UnityEngine;

namespace BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        public static BattleManager Instance { get; private set; }

        private BattleState _currentBattleState;
        private CardActions cardActions;

        private List<Card> _drawPile;
        private List<Card> _cardsInHand;
        private List<Card> _discardPile;
        private int _currentEnergy;
        private int _maxEnergy;

        public Transform topParent;
        public CardUI selectedCard;

        [SerializeField] private Fighter cardTarget;
        [SerializeField] private Fighter player;

        [SerializeField] private List<Enemy> enemies = new List<Enemy>();


        private void Awake()
        {
            Instance = this;
            _cardsInHand = new List<Card>();
        }

        public void StartBattle(EnemyType enemyType)
        {
            BattleScreen.ChangeTurn(BattleState.PlayerTurn);
            _currentBattleState = BattleState.PlayerTurn;
            var characterData = SaveManager.LoadCharacterData();

            switch (enemyType)
            {
                case EnemyType.Default:
                    int numberOfEnemies = Random.Range(0, 2);
                    for (int i = 0; i <= numberOfEnemies; i++)
                    {
                        enemies.Add(SettingsProvider.Get<BattlePrefabSet>().GetEnemy<Enemy>());
                    }

                    break;
                case EnemyType.Elite:
                    enemies.Add(SettingsProvider.Get<BattlePrefabSet>().GetEnemy<Enemy>());
                    break;
                case EnemyType.Boss:
                    enemies.Add(SettingsProvider.Get<BattlePrefabSet>().GetEnemy<Enemy>());
                    break;
            }

            switch (characterData.startingRelic.RelicType)
            {
                case RelicType.PreservedInsect:
                    if (enemyType == EnemyType.Elite)
                        enemies[0].GetComponent<Fighter>().currentHealth =
                            (int)(enemies[0].GetComponent<Fighter>().currentHealth * 0.25);
                    break;
                case RelicType.Anchor:
                    player.AddBlock(10);
                    break;
                case RelicType.Lantern:
                    _maxEnergy = characterData.Energy;
                    _maxEnergy += 1;
                    break;
                case RelicType.Marbles:
                    enemies[0].GetComponent<Fighter>().AddBuff(Buff.Type.Vulnerable, 1);
                    break;
                case RelicType.Bag:
                    DrawCards(2);
                    break;
                case RelicType.Varja:
                    player.AddBuff(Buff.Type.Strength, 1);
                    break;
            }

            _currentEnergy = _maxEnergy;
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
            if (cardUI.card.cardType != Card.CardType.Attack && enemies[0].GetComponent<Fighter>().enrage.buffValue > 0)
                enemies[0].GetComponent<Fighter>()
                    .AddBuff(Buff.Type.Strength, enemies[0].GetComponent<Fighter>().enrage.buffValue);

            cardActions.PerformAction(cardUI.card, cardTarget);

            _currentEnergy -= cardUI.card.GetCardCostAmount();
            /*energyText.text = energy.ToString();*/

            Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);
            selectedCard = null;
            cardUI.gameObject.SetActive(false);
            _cardsInHand.Remove(cardUI.card);
            DiscardCard(cardUI.card);
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

                /*foreach (CardUI cardUI in cardsInHandGameObjects)
                {
                    if (cardUI.gameObject.activeSelf)
                        Instantiate(cardUI.discardEffect, cardUI.transform.position, Quaternion.identity, topParent);

                    cardUI.gameObject.SetActive(false);
                    cardsInHand.Remove(cardUI.card);
                }*/


                foreach (Enemy e in enemies)
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
                foreach (Enemy e in enemies)
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
                BattleScreen.ChangeTurn(BattleState.PlayerTurn);
            }
        }


        private IEnumerator HandleEnemyTurn()
        {
            BattleScreen.ChangeTurn(BattleState.EnemyTurn);

            yield return new WaitForSeconds(1.5f);

            foreach (Enemy enemy in enemies)
            {
                enemy.midTurn = true;
                enemy.TakeTurn();
                while (enemy.midTurn)
                    yield return new WaitForEndOfFrame();
            }

            Debug.Log("Turn Over");
            ChangeTurn();
        }

        public void EndFight(bool win)
        {
            var characterData = SaveManager.LoadCharacterData();
            if (characterData.startingRelic.RelicType ==RelicType.BurningBlood)
            {
                player.currentHealth += 6;
                if (player.currentHealth > player.maxHealth)
                    player.currentHealth = player.maxHealth;
                player.UpdateHealthUI(player.currentHealth);
            }

            player.ResetBuffs();
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