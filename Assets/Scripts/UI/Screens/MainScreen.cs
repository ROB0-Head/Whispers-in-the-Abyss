using System;
using System.Linq;
using Navigation;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class MainScreen : DefaultScreen
    {
        private MainTabType _currentTab;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        private void Awake()
        {
            _startButton.onClick.AddListener(() => { SelectTab(MainTabType.Start); });
            _settingsButton.onClick.AddListener(() => { SelectTab(MainTabType.Settings); });
            _exitButton.onClick.AddListener(() => { SelectTab(MainTabType.Exit); });
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

        public void SelectTab(MainTabType tabType)
        {
            _currentTab = tabType;

            _titleText.text = tabType.ToString();

            var elements = SettingsProvider.Get<MainScreenTabsSettings>().TabSettings.First(x => x.TabType == tabType)
                .Elements;

            foreach (var elementType in elements)
            {
                switch (elementType)
                {
                    case TabSetting.ElementType.None:
                        break;
                    case TabSetting.ElementType.Start:
                        NavigationController.Instance.ScreenTransition<CityScreen>(new CityScreenSettings()
                        {
                            TabType = CityScreen.CityTabType.City
                        });

                        break;
                    case TabSetting.ElementType.Settings:
                        break;
                    case TabSetting.ElementType.Exit:
                        Exit();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void Exit()
        {
            Debug.Log("Exit button clicked");
        }

        public enum MainTabType
        {
            Main = 0,
            Start = 1,
            Settings = 2,
            Exit = 3
        }
    }

    public class MainScreenSettings : ScreenSettings
    {
        public MainScreen.MainTabType TabType;
    }
}