using System;
using UnityEngine;

namespace Settings.BattleManager
{
    [Serializable]
    public struct Buff
    {
        public Type BuffType { get; set; }
        public Sprite BuffIcon { get; set; }
        public int BuffValue { get; set; }
        public BuffUI BuffGo { get; set; }

        public enum Type
        {
            Strength,
            Vulnerable,
            Weak,
            Ritual,
            Enrage
        }
    }
}
