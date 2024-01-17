using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace BattleSystem.Characters.Enemy
{
    [Serializable]
    public class EnemyAction
    {
        public IntentType IntentType;
        public int IntentAmount;
        public BuffType BuffType;
        public int DebuffAmount;
        public int Chance;
        public Sprite Icon;
    }

    public enum IntentType
    {
        Attack,
        Block,
        StrategicBuff,
        StrategicDebuff,
        AttackDebuff
    }
}