using BattleSystem;
using Map;
using Navigation;
using SaveSystem;
using Settings;
using TJ;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class BattleScreen : DefaultScreen
    {
        private BattleTabType _currentTab;
        [SerializeField] private Button _backButton;

        [SerializeField] private Button endTurnButton;
        public TMP_Text drawPileCountText;
        public TMP_Text discardPileCountText;
        public TMP_Text energyText;

        public Transform enemyParent;
        public EndScreen endScreen;

        public static Animator banner;
        public static TMP_Text turnText;

        [SerializeField] private GameObject _deck;


        private void Awake()
        {
            _backButton.onClick.AddListener(() => { SelectTab(BattleTabType.Exit); });
        }

        public override void Setup(ScreenSettings settings)
        {
            if (settings is not BattleScreenSettings battleScreenSettings)
                return;

            SelectTab(battleScreenSettings.TabType);
            BattleManager.Instance.StartBattle(battleScreenSettings.EnemyType);
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
                    var deck = SettingsProvider.Get<BattlePrefabSet>().CharacterDeck.Deck;
                    var zOffSet = 15f;
                    foreach (var card in deck)
                    {
                        var cardTransform = Instantiate(card.cardPrefab, _deck.transform);
                        cardTransform.transform.rotation = Quaternion.Euler(0, 0, zOffSet);
                        zOffSet -= 5;
                    }

                    _currentTab = tabType;
                    break;
                case BattleTabType.Exit:
                    Home();
                    break;
                default:
                    break;
            }
        }

        public static void ChangeTurn(BattleState battleState)
        {
            /*switch (battleState)
            {
                case BattleState.EnemyTurn:
                    turnText.text = "Enemy's Turn";
                    banner.Play("bannerIn");
                    break;

                case BattleState.PlayerTurn:
                    turnText.text = "Player's Turn";
                    banner.Play("bannerOut");
                    break;
            }*/
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