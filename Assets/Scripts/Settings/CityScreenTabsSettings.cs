using System;
using System.Collections.Generic;
using UI.Screen;
using UI.Screens;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "CityScreenTabsSettings", menuName = "WITA/CityScreenTabsSettings", order = 0)]
    public class CityScreenTabsSettings  : ScriptableObject
    {
        [field: SerializeField] public List<CityTabSetting> TabSettings { get; private set; }
    }

    [Serializable]
    public struct CityTabSetting
    {
        public CityScreen.CityTabType TabType;
        public List<ElementType> Elements;

        public enum ElementType
        {
            Start = 0,
            Settings = 1,
            Back = 2,
            Exit = 3,
        }
    }
}