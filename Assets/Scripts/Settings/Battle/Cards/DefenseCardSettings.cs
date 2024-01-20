using BattleSystem.Cards;
using UnityEngine;

namespace Settings.Battle.Cards
{
    [CreateAssetMenu(fileName = "DefenseCardSettings", menuName = "WITA/Battle/Cards/DefenseCard")]
    public class DefenseCardSettings : CardSettings
    {
        public DefenceCardType DefenseType;
    }
}