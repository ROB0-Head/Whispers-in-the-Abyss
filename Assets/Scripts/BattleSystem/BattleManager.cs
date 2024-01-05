using System.Collections;
using System.Collections.Generic;
using SaveSystem;
using Settings;
using UnityEngine;

namespace BattleSystem
{
    public class BattleManager : MonoBehaviour
    {
        public BattleManager Instance { get; private set; }
        [SerializeField] private GameObject _deck;

        private enum BattleState
        {
            PlayerTurn,
            EnemyTurn,
            Victory,
            Defeat
        }

        private BattleState currentBattleState;

        private List<Card> playerHand;

        private void Awake()
        {
            Instance = this;
        }

        void Start()
        {
            var userData = SaveManager.LoadUserData();
            var zOffSet = 15f;
            foreach (var card in userData.Cards)
            {
                var cards = SettingsProvider.Get<Deck>().CardDeck[0];
                var cardTransform = Instantiate(cards.cardPrefab , _deck.transform);
                card.Play();
                cardTransform.transform.rotation = Quaternion.Euler(0, 0, zOffSet);

                zOffSet -= 5;
            }
            InitializeBattle();
        }

        void InitializeBattle()
        {
            currentBattleState = BattleState.PlayerTurn;
            playerHand = new List<Card>();

            StartCoroutine(StartBattle());
        }

        IEnumerator StartBattle()
        {
            while (currentBattleState != BattleState.Victory && currentBattleState != BattleState.Defeat)
            {
                yield return null;

                switch (currentBattleState)
                {
                    case BattleState.PlayerTurn:
                        PlayerTurn();
                        break;

                    case BattleState.EnemyTurn:
                        EnemyTurn();
                        break;
                }
            }
        }

        void PlayerTurn()
        {
            currentBattleState = BattleState.EnemyTurn;
        }

        void EnemyTurn()
        {
            // Логика для хода врагов

            // Реализация стратегии врагов, например, атака случайного игрока

            // Проверка условий поражения

            /*if (playerController.IsDefeated())
        {
            currentBattleState = BattleState.Defeat;
            StartCoroutine(EndBattle());
            return;
        }*/

            // Переключение на ход игрока
            currentBattleState = BattleState.PlayerTurn;
        }


        IEnumerator EndBattle()
        {
            // Логика завершения битвы

            // Отображение результатов (победа или поражение)

            yield return new WaitForSeconds(2f);

            // Возврат на карту или другой экран
            // Например, вызов функции для выбора следующего пути на карте
        }
    }
}