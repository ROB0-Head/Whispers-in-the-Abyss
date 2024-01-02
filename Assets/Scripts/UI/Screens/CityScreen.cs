using Navigation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class CityScreen : DefaultScreen
    {
        private CityTabType _currentTab;
        [SerializeField] private TMP_Text _titleText;
        /*
        [SerializeField] private GameObject _contentParent;
        */

        [SerializeField] private Button _guildButton;
        [SerializeField] private Button _forgeButton;
        [SerializeField] private Button _backButton;

        /*
        [SerializeField] private List<ScreenBottomIcons> _screenBottomIcons;
        */

        private void Awake()
        {
            _guildButton.onClick.AddListener(() => { SelectTab(CityTabType.Guild); });
            _forgeButton.onClick.AddListener(() => { SelectTab(CityTabType.Forge); });
            _backButton.onClick.AddListener(() => { SelectTab(CityTabType.Exit); });
        }

        public override void Setup(ScreenSettings settings)
        {
            if (settings is not CityScreenSettings mainScreenSettings)
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

        public void SelectTab(CityTabType tabType)
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

            _titleText.text = tabType.ToString();
            switch (tabType)
                {
                    case CityTabType.Guild:

                        break;
                    case CityTabType.Forge:

                        break;
                    case CityTabType.Backpack:
                        
                        break;
                    case CityTabType.Exit:
                        Home();
                        break;
                    default:
                        break;
                }
            
        }
        public void Home()
        {
            NavigationController.Instance.ScreenTransition<MainScreen>(new MainScreenSettings()
            {
                TabType = MainScreen.MainTabType.Main
            });
        }
        /*public void ShowBuyHeartsPopup()
        {
            PopupSystem.ShowPopup<HeartPurchasePopup>();
        }*/

        public enum CityTabType
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

    public class CityScreenSettings : ScreenSettings
    {
        public CityScreen.CityTabType TabType;
    }
}