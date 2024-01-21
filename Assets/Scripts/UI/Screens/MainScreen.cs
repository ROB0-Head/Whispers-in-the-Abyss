using System;
using System.Linq;
using Navigation;
using SaveSystem;
using Settings;
using Settings.Storyline;
using TMPro;
using UI.Storyline;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class MainScreen : DefaultScreen
    {
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

        public void SelectTab(MainTabType tabType)
        {
            _titleText.text = tabType.ToString();
            var characterData = SaveManager.LoadCharacterData();
            switch (tabType)
            {
                case MainTabType.Start:
                    NavigationController.Instance.ScreenTransition<CityScreen>(new CityScreenSettings()
                    {
                        TabType = CityScreen.CityTabType.City
                    });
                    var needStartHistory = characterData.HistoriesNames.FirstOrDefault(x => x.Contains("StartHistory"));
                    if (needStartHistory.IsNullOrEmpty())
                    {
                        characterData.HistoriesNames.Add("StartHistory");
                        SlideshowManager.Instance.StartSlideshow(SettingsProvider.Get<HistoriesLibrary>()
                            .Histories.FirstOrDefault(x => x.Name == "StartHistory")?.History);
                        
                    }
                    SaveManager.SaveCharacterData(characterData);
                    break;
                case MainTabType.Settings:

                    break;
                case MainTabType.Exit:

                    Exit();

                    break;
            }
        }

        public void Exit()
        {
            Debug.Log("Exit button clicked");
        }

        public enum MainTabType
        {
            Start = 0,
            Settings = 1,
            Exit = 2
        }
    }

    public class MainScreenSettings : ScreenSettings
    {
        public MainScreen.MainTabType TabType;
    }
}