using Map;
using Navigation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class BattleScreen : DefaultScreen
    {
        private BattleTabType _currentTab;
        [SerializeField] private Button _backButton;
        
        private void Awake()
        {
            _backButton.onClick.AddListener(() => { SelectTab(BattleTabType.Exit); });
        }

        public override void Setup(ScreenSettings settings)
        {
            if (settings is not BattleScreenSettings mainScreenSettings)
                return;

            SelectTab(mainScreenSettings.TabType);
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
    }
}
