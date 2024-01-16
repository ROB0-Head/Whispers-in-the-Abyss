using BattleSystem.Cards;
using UnityEngine;

namespace Settings.BattleManager.Cards
{
    [CreateAssetMenu(fileName = "AttackCardSettings", menuName = "WITA/Battle/Cards/AttackCard")]
    public class AttackCardSettings : CardSettings
    {
        public AttackCardType AttackType;
    }
}