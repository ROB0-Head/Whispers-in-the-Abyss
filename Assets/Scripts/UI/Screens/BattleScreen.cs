using System.Collections.Generic;
using System.Linq;
using BattleSystem;
using BattleSystem.Cards;
using BattleSystem.Characters.Enemy;
using Map;
using Navigation;
using SaveSystem;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class BattleScreen : DefaultScreen
    {
        public static BattleScreen Instance { get; private set; }

        [SerializeField] private Button _backButton;
        [SerializeField] private Button _endTurnButton;
        [SerializeField] private Animator _banner;
        [SerializeField] private TMP_Text _turnText;
        [SerializeField] private TMP_Text _drawPileCountText;
        [SerializeField] private TMP_Text _discardPileCountText;
        [SerializeField] private TMP_Text _energyText;
        [SerializeField] private Transform _deck;
        [SerializeField] private Transform _discardParent;

        private BattleTabType _currentTab;
        private List<CardUI> _cardList = new List<CardUI>();

        public Transform DiscardPileCount => _discardPileCountText.transform;
        public Transform DiscardParent => _discardParent;

        private void Awake()
        {
            Instance = this;
            _backButton.onClick.AddListener(() => { SelectTab(BattleTabType.Exit); });
        }

        private void OnDisable()
        {
            BattleManager.Instance.DrawPileCountUpdated -= UpdateDrawPileCountText;
            BattleManager.Instance.DiscardPileCountUpdated -= UpdateDiscardPileCountText;
            BattleManager.Instance.CurrentEnergyUpdated -= UpdateEnergyText;
        }

        public void UpdateDrawPileCountText(int newDrawPileCount)
        {
            _drawPileCountText.text = newDrawPileCount.ToString();
        }

        public void UpdateDiscardPileCountText(int newDiscardPileCount)
        {
            _discardPileCountText.text = newDiscardPileCount.ToString();
        }

        public void UpdateEnergyText(int newEnergy)
        {
            _energyText.text = newEnergy.ToString();
        }

        public override void Setup(ScreenSettings settings)
        {
            BattleManager.Instance.DrawPileCountUpdated += UpdateDrawPileCountText;
            BattleManager.Instance.DiscardPileCountUpdated += UpdateDiscardPileCountText;
            BattleManager.Instance.CurrentEnergyUpdated += UpdateEnergyText;
            if (settings is not BattleScreenSettings battleScreenSettings)
                return;

            SelectTab(battleScreenSettings.TabType);
            BattleManager.Instance.StartBattle(battleScreenSettings.EnemyType);
        }

        public List<Card> DrawCards()
        {
            var deck = new List<Card>();
            var cardDeck = SaveManager.LoadDeck();
            foreach (var card in cardDeck)
            {
                var cardTransform = Instantiate(SettingsProvider.Get<BattlePrefabSet>().DeckLibrary.CardPrefab,
                    _deck);
                var cardUI = cardTransform.GetComponentInChildren<CardUI>();
                cardUI.LoadCard(card);
                deck.Add(cardUI.Card);
                _cardList.Add(cardUI);
                cardTransform.gameObject.SetActive(false);
            }

            return deck;
        }

        public void DisplayCardInHand(Card card)
        {
            var existingCardUI = _cardList.FirstOrDefault(x =>
                x.CardTitle == card.CardTitle && x.CardEnergy == card.CardEnergy.baseAmount.ToString() &&
                x.CardStat == card.CardStat.baseAmount.ToString() && !x.gameObject.activeSelf);

            if (existingCardUI != null)
            {
                existingCardUI.gameObject.SetActive(true);
            }
            else
            {
                var inactiveCardUI = _cardList.FirstOrDefault(x => !x.transform.parent.gameObject.activeSelf);

                if (inactiveCardUI != null)
                {
                    inactiveCardUI.LoadCard(card);
                    inactiveCardUI.transform.parent.gameObject.SetActive(true);
                }
            }
        }

        public void SortingCardInHand()
        {
            var activeCards = _cardList.FindAll(x => x.transform.parent.gameObject.activeSelf);

            int centralCardIndex = Mathf.CeilToInt(activeCards.Count / 2f);

            for (int i = 0; i < activeCards.Count; i++)
            {
                GameObject currentCard = activeCards[i].transform.parent.gameObject;

                float angle = (i + 1 - centralCardIndex) * 5;

                currentCard.transform.rotation = Quaternion.Euler(0, 0, angle);
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

        public void ChangeTurn(BattleState battleState, bool endTurn)
        {
            _endTurnButton.enabled = endTurn;
            switch (battleState)
            {
                case BattleState.EnemyTurn:
                    _turnText.text = "Enemy's Turn";
                    _banner.Play("bannerIn");
                    break;

                case BattleState.PlayerTurn:
                    _turnText.text = "Player's Turn";
                    _banner.Play("bannerOut");
                    break;
            }
        }

        public void DiscardCardInHand()
        {
            var activeCards = _cardList.FindAll(x => x.transform.parent.gameObject.activeSelf);

            foreach (var cardUI in activeCards)
            {
                Instantiate(cardUI.DiscardEffect, cardUI.transform.position, Quaternion.identity, _discardParent);
                cardUI.transform.parent.gameObject.SetActive(false);
            }
        }

        public void Home()
        {
            NavigationController.Instance.ScreenTransition<MapManager>();
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