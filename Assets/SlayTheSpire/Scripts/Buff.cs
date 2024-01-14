using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TJ
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