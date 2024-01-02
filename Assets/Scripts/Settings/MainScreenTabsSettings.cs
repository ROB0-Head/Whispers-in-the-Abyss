using System;
using System.Collections.Generic;
using UI.Screens;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "MainScreenTabsSettings", menuName = "WITA/MainScreenTabsSettings", order = 0)]
    public class MainScreenTabsSettings  : ScriptableObject
    {
        [field: SerializeField] public List<TabSetting> TabSettings { get; private set; }
    }

    [Serializable]
    public struct TabSetting
    {
        public MainScreen.MainTabType TabType;
        public List<ElementType> Elements;

        public enum ElementType
        {
            None = 0,
            Start = 1,
            Settings = 2,
            Exit = 4,
        }
    }
}