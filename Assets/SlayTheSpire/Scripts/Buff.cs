using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TJ
{
    [Serializable]
    public struct Buff
    {
        public Type BuffType;
        public Sprite BuffIcon;
        public int BuffValue;
        public BuffUI BuffGO;

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