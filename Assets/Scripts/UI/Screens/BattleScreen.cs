using BattleSystem;
using Map;
using Navigation;
using SaveSystem;
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
            if (settings is BattleScreenSettings battleScreenSettings)
            {
                var userData = SaveManager.LoadCharacterData();
                var zOffSet = 15f;
                foreach (var card in userData.CurrentDeck.Deck)
                {
                    /*var cardTransform = Instantiate(card.cardPrefab, _deck.transform);
                    card.Play();
                    cardTransform.transform.rotation = Quaternion.Euler(0, 0, zOffSet);
                    zOffSet -= 5;*/
                }

                BattleManager.Instance.StartBattle(battleScreenSettings.EnemyType);
                turnText.text = "Player's Turn";
                banner.Play("bannerOut");
            }
            else
                return;

            SelectTab(battleScreenSettings.TabType);
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
            _currentTab = tabType;
            /*foreach (var screenBottomIcon in _screenBottomIcons)
            {
                if (screenBottomIcon.TabType == tabType)
                {
                    screenBottomIcon.Icon.sprite = screenBottomIcon.Selected;
                    screenBottomIcon.Icon.color = new Color(screenBottomIcon.Icon.color.r,
                        screenBottomIcon.Icon.color.g, screenBottomIcon.Icon.color.b, 1);
                }
                else
                {
                    screenBottomIcon.Icon.sprite = screenBottomIcon.Unselected;
                    screenBottomIcon.Icon.color = new Color(screenBottomIcon.Icon.color.r,
                        screenBottomIcon.Icon.color.g, screenBottomIcon.Icon.color.b, 0.5f);
                }
            }

            var allChildren = new List<GameObject>();

            foreach (Transform child in _contentParent.transform)
            {
                allChildren.Add(child.gameObject);
            }

            foreach (var child in allChildren)
            {
                Destroy(child);
            }*/

            switch (tabType)
            {
                case BattleTabType.Guild:
                    NavigationController.Instance.ScreenTransition<MapManager>();
                    break;
                case BattleTabType.Forge:
                    NavigationController.Instance.ScreenTransition<DialogManager>();
                    break;
                case BattleTabType.Backpack:

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
            City = 0,
            Guild = 1,
            Forge = 2,
            Backpack = 3,
            Exit = 4
        }

        /*[Serializable]
        public class ScreenBottomIcons
        {
            public CityTabType TabType;
            public Image Icon;
            public Sprite Unselected;
            public Sprite Selected;
        }*/
    }

    public class BattleScreenSettings : ScreenSettings
    {
        public BattleScreen.BattleTabType TabType;
        public EnemyType EnemyType;
    }
}