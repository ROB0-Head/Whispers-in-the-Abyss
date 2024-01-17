using BattleSystem.Cards;
using Settings.BattleManager.Cards;
using UnityEngine;

namespace Settings.Battle
{
    [CreateAssetMenu(fileName = "AttackCardSettings", menuName = "WITA/Battle/Cards/AttackCard")]
    public class AttackCardSettings : CardSettings
    {
        public AttackCardType AttackType;
    }
}