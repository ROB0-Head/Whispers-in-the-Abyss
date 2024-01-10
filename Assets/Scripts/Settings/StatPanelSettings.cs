using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "StatPanelSettings", menuName = "WITA/StatPanelSettings", order = 0)]
    public class StatPanelSettings: ScriptableObject
    {
        [field: SerializeField] public List<Stat> Stats { get; private set; }
    }
     
    [Serializable]
    public class Stat
    {
        public string StatName;
        public int CurrentStatValue;
    }
    
}
