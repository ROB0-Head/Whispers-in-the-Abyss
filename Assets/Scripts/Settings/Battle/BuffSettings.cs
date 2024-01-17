using System;
using System.Collections.Generic;
using BattleSystem;

using UnityEngine;

namespace Settings.Battle
{
    [CreateAssetMenu(fileName = "BuffSettings", menuName = "WITA/Battle/BuffSettings")]
    public class BuffSettings : ScriptableObject
    {
        public List<Buffs> Buffs;
    }
    
    [Serializable]
    public class Buffs
    {
        public BuffType BuffType;
        public Sprite BuffIcon;
    }
}