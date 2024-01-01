using System;
using System.Collections.Generic;
using System.Linq;
using Settings;
using TMPro;
using UI.Buttons;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class MainScreen : DefaultScreen
    {
        private TabType _currentTab;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private GameObject _contentParent;

        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private List<ScreenBottomIcons> _screenBottomIcons;

        private void Awake()
        {
            _startButton.onClick.AddListener(() => { SelectTab(TabType.Start); });
            _settingsButton.onClick.AddListener(() => { SelectTab(TabType.Settings); });
            _exitButton.onClick.AddListener(() => { SelectTab(TabType.Exit); });
        }

        public override void Setup(ScreenSettings settings)
        {
            if (settings is not MainScreenSettings mainScreenSettings)
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

        public void SelectTab(TabType tabType)
        {
            _currentTab = tabType;
            foreach (var screenBottomIcon in _screenBottomIcons)
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
            }

            _titleText.text = tabType.ToString();
            var elements = SettingsProvider.Get<MainScreenTabsSettings>().TabSettings.First(x => x.TabType == tabType)
                .Elements;

            foreach (var elementType in elements)
            {
                switch (elementType)
                {
                    case TabSetting.ElementType.Start:

                        break;
                    case TabSetting.ElementType.Settings:

                        break;
                    case TabSetting.ElementType.Exit:
                        
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /*public void ShowBuyHeartsPopup()
        {
            PopupSystem.ShowPopup<HeartPurchasePopup>();
        }*/

        public enum TabType
        {
            None = 0,
            Start = 1,
            Settings = 2,
            Exit = 3,
        }

        [Serializable]
        public class ScreenBottomIcons
        {
            public TabType TabType;
            public Image Icon;
            public Sprite Unselected;
            public Sprite Selected;
        }
    }

    public class MainScreenSettings : ScreenSettings
    {
        public MainScreen.TabType TabType;
    }
}