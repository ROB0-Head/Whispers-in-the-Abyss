using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleSystem
{
    [Serializable]
    public class Buff
    {
        public BuffType BuffsType;
        public Sprite BuffIcon;
        public int BuffValue;
        public BuffUI BuffGo;
        
    }
    public enum BuffType
    {
        Strength,
        Vulnerable,
        Weak,
        Ritual,
        Enrage
    }
}