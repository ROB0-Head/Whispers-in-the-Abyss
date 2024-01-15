using UnityEngine;

namespace Settings.BattleManager.Cards
{
    [CreateAssetMenu(fileName = "DefenseCardSettings", menuName = "WITA/Battle/Cards/DefenseCard")]
    public class DefenseCardSettings : CardSettings
    {
        public DefenceCardType DefenseType;
    }
}