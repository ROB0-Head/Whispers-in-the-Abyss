using Map;
using Navigation;
using Settings;
using TMPro;
using UI.Panels;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ReceptionScreen : DefaultScreen
    {
        [SerializeField] private Button _cityButton;
        [SerializeField] private Button _towerButton;
        [SerializeField] private Button _taskBoardButton;
        [SerializeField] private Button _backButton;

        [SerializeField] private GameObject receptionContent;

        private void Awake()
        {
            /*_cityButton.onClick.AddListener(() => { SelectTab(ReceptionTabType.City); });
            _towerButton.onClick.AddListener(() => { SelectTab(ReceptionTabType.Tower); });
            _taskBoardButton.onClick.AddListener(() => { SelectTab(ReceptionTabType.Backpack); });*/
            _backButton.onClick.AddListener(() => { SelectTab(ReceptionTabType.Back); });
        }

        public override void Setup(ScreenSettings settings)
        {
            var prefabPopup = SettingsProvider.Get<PrefabSet>().GetPanel<StatPanel>();
            var popup = Instantiate(prefabPopup, receptionContent.transform);
            popup.Setup(SettingsProvider.Get<StatPanelSettings>());
            if (settings is not ReceptionScreenSettings mainScreenSettings)
                return;
            SelectTab(mainScreenSettings.TabType);
        }

        public override void Deactivate()
        {
        }

        public void SelectTab(ReceptionTabType tabType)
        {
            switch (tabType)
            {
                case ReceptionTabType.City:
                    Home();
                    break;

                case ReceptionTabType.Tower:
                    NavigationController.Instance.ScreenTransition<MapManager>();
                    break;

                case ReceptionTabType.Backpack:
                    PopupSystem.ShowPopup<TaskBoardPopup>();
                    break;

                case ReceptionTabType.Back:
                    NavigationController.Instance.ScreenTransition<GuildScreen>();
                    break;
            }
        }

        public void Home()
        {
            NavigationController.Instance.ScreenTransition<CityScreen>();
        }

        public enum ReceptionTabType
        {
            City = 0,
            Tower = 1,
            Backpack = 2,
            Back = 3
        }
    }

    public class ReceptionScreenSettings : ScreenSettings
    {
        public ReceptionScreen.ReceptionTabType TabType;
    }
}