using System.Collections.Generic;
using UnityEngine;

namespace Settings.Storyline
{
    [CreateAssetMenu(fileName = "HistoriesLibrary", menuName = "WITA/Story/HistoriesLibrary", order = 0)]
    public class HistoriesLibrary : ScriptableObject
    {
        public List<HistorySettings> Histories;
    }
}