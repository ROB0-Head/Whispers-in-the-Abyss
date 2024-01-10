using Map;
using Navigation;
using Settings;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class GuildScreen : DefaultScreen
    {
        [SerializeField] private Button _receptionButton;
        [SerializeField] private Button _taskBoardButton;
        [SerializeField] private Button _backButton;
        

        private void Awake()
        {
            _receptionButton.onClick.AddListener(() => { SelectTab(GuildTabType.Reception); });
            _taskBoardButton.onClick.AddListener(() => { SelectTab(GuildTabType.TaskBoard); });
            _backButton.onClick.AddListener(() => { SelectTab(GuildTabType.Back); });
        }

        public override void Setup(ScreenSettings settings)
        {
            if (settings is not GuildScreenSettings mainScreenSettings)
                return;

            SelectTab(mainScreenSettings.TabType);
        }

        public override void Deactivate()
        {
        }

        public void SelectTab(GuildTabType tabType)
        {

            switch (tabType)
            {
                case GuildTabType.Reception:
                    NavigationController.Instance.ScreenTransition<ReceptionScreen>();

                    break;
                
                case GuildTabType.TaskBoard:
                    PopupSystem.ShowPopup<TaskBoardPopup>();
                    break;
                
                case GuildTabType.Back:
                    Home();
                    break;
                
            }
        }

        public void Home()
        {
            NavigationController.Instance.ScreenTransition<CityScreen>();
        }
        /*public void ShowBuyHeartsPopup()
        {
        }*/

        public enum GuildTabType
        {
            City = 0,
            Reception = 1,
            TaskBoard = 2,
            Back = 3
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

    public class GuildScreenSettings : ScreenSettings
    {
        public GuildScreen.GuildTabType TabType;
    }
}