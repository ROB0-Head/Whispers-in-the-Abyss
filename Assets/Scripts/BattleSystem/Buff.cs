using System;
using UnityEngine;

namespace BattleSystem
{
    [Serializable]
    public struct Buff
    {
        public Type BuffType;
        public Sprite BuffIcon;
        public int BuffValue;
        public BuffUI BuffGo;

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