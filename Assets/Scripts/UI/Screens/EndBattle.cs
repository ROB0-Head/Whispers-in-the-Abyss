using System;
using BattleSystem;
using Map;
using Navigation;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class EndBatte : MonoBehaviour
    {
        [SerializeField] private GameObject _endScreen;
        [SerializeField] private GameObject _victory;
        [SerializeField] private GameObject _defeat;
        [SerializeField] private Button _endButton;

        public void EndBattle(BattleState battleState)
        {
            _endScreen.SetActive(true);
            if (battleState == BattleState.Victory)
            {
                _victory.SetActive(true);
                _defeat.SetActive(false);
                _endButton.onClick.AddListener(() => { NavigationController.Instance.ScreenTransition<MapManager>(); });
            }
            else if (battleState == BattleState.Defeat)
            {
                _defeat.SetActive(true);
                _victory.SetActive(false);
                _endButton.onClick.AddListener(() => { NavigationController.Instance.ScreenTransition<MainScreen>(); });
            }
        }
    }
}