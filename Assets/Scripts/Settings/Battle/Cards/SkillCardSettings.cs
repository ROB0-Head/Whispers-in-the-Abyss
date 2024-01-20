using BattleSystem.Cards;
using UnityEngine;

namespace Settings.Battle.Cards
{
    [CreateAssetMenu(fileName = "SkillCardSettings", menuName = "WITA/Battle/Cards/SkillCard")]
    public class SkillCardSettings : CardSettings
    {
        public SkillCardType SkillType;
    }
}
    
