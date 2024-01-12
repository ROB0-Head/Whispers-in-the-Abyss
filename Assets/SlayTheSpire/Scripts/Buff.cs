using System;
using UnityEngine;

namespace TJ
{
    [Serializable]
    public struct Buff
    {
        public Sprite buffIcon;
        public int buffValue;
        public BuffUI buffGO;

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