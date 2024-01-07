using Map;
using Navigation;
using Settings;
using TMPro;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class ReceptionScreen : DefaultScreen
    {
        [SerializeField] private Button _receptionButton;
        [SerializeField] private Button _taskBoardButton;
        [SerializeField] private Button _backButton;
        

        private void Awake()
        {
            _receptionButton.onClick.AddListener(() => { SelectTab(ReceptionTabType.Reception); });
            _taskBoardButton.onClick.AddListener(() => { SelectTab(ReceptionTabType.TaskBoard); });
            _backButton.onClick.AddListener(() => { SelectTab(ReceptionTabType.Back); });
        }

        public override void Setup(ScreenSettings settings)
        {
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
                case ReceptionTabType.Reception:
                    
                    break;
                
                case ReceptionTabType.TaskBoard:
                    PopupSystem.ShowPopup<TaskBoardPopup>();
                    break;
                
                case ReceptionTabType.Back:
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

        public enum ReceptionTabType
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

    public class ReceptionScreenSettings : ScreenSettings
    {
        public ReceptionScreen.ReceptionTabType TabType;
    }
}