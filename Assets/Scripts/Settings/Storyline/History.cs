using System.Collections.Generic;
using UI.Screens;
using UnityEngine;

namespace Settings.Storyline
{
    [CreateAssetMenu(fileName = "HistorySettings", menuName = "WITA/Story/HistorySettings", order = 0)]
    public class HistorySettings : ScriptableObject
    {
        public string Name;
        public List<SlideshowScreen> History;
    }
}