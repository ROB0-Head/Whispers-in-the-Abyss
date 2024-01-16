using System.Collections.Generic;
using BattleSystem;
using Map;
using Navigation;
using SaveSystem;
using Settings;
using Settings.BattleManager.Cards;
using TJ;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class BattleScreen : DefaultScreen
    {
        public static BattleScreen Instance { get; private set; }

        [SerializeField] private Button _backButton;
        [SerializeField] private Button endTurnButton;
        [SerializeField] private Animator banner;
        [SerializeField] private TMP_Text turnText;
        [SerializeField] private TMP_Text drawPileCountText;
        [SerializeField] private TMP_Text discardPileCountText;
        [SerializeField] private TMP_Text energyText;
        [SerializeField] private Transform enemyParent;
        [SerializeField] private Transform _deck;

        private BattleTabType _currentTab;

        private void Awake()
        {
            Instance = this;
            _backButton.onClick.AddListener(() => { SelectTab(BattleTabType.Exit); });
        }

        public override void Setup(ScreenSettings settings)
        {
            if (settings is not BattleScreenSettings battleScreenSettings)
                return;

            SelectTab(battleScreenSettings.TabType);
            BattleManager.Instance.StartBattle(battleScreenSettings.EnemyType);
        }

        public List<Card> DrawCards()
        {
            var deck = SaveManager.LoadDeck();
            var zOffSet = 15f;
            foreach (var card in deck)
            {
                var cardTransform = Instantiate(SettingsProvider.Get<BattlePrefabSet>().DeckLibrary.AttackCardPrefab,
                    _deck);
                cardTransform.transform.rotation = Quaternion.Euler(0, 0, zOffSet);
                zOffSet -= 5;
                var cardUI = cardTransform.GetComponent<CardUI>();
                cardUI.LoadCard(card);
                cardTransform.gameObject.SetActive(false);
            }

            return deck;
        }

        public void DisplayCardInHand(Card card)
        {
            foreach (Transform cardUI in _deck)
            {
                if (card.CardTitle == cardUI.GetComponent<CardUI>().CardTitle)
                {
                    cardUI.gameObject.SetActive(true);
                }
            }
        }

        public override void UpdateScreen()
        {
            SelectTab(_currentTab);
        }

        public override void Deactivate()
        {
        }

        public void SelectTab(BattleTabType tabType)
        {
            switch (tabType)
            {
                case BattleTabType.Battle:
                    _currentTab = tabType;
                    break;
                case BattleTabType.Exit:
                    Home();
                    break;
                default:
                    break;
            }
        }

        public void ChangeTurn(BattleState battleState)
        {
            switch (battleState)
            {
                case BattleState.EnemyTurn:
                    turnText.text = "Enemy's Turn";
                    banner.Play("bannerIn");
                    break;

                case BattleState.PlayerTurn:
                    turnText.text = "Player's Turn";
                    banner.Play("bannerOut");
                    break;
            }
        }

        public void Home()
        {
            NavigationController.Instance.ScreenTransition<MainScreen>();
        }

        public enum BattleTabType
        {
            Battle = 0,
            Exit = 1
        }
    }

    public class BattleScreenSettings : ScreenSettings
    {
        public BattleScreen.BattleTabType TabType;
        public EnemyType EnemyType;
    }
}