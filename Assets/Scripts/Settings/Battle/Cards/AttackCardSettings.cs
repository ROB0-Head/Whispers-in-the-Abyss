using BattleSystem.Cards;
using UnityEngine;

namespace Settings.Battle.Cards
{
    [CreateAssetMenu(fileName = "AttackCardSettings", menuName = "WITA/Battle/Cards/AttackCard")]
    public class AttackCardSettings : CardSettings
    {
        public AttackCardType AttackType;
    }
}