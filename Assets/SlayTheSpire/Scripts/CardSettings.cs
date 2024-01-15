using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    public class CardSettings : ScriptableObject
    {
        public bool IsUpgraded;
        public CardDescription CardDescription;
        public CardAmount CardCost;
        public CardAmount CardEffect;
        public CardAmount BuffAmount;
        public CardType CardType;
    }

    [CreateAssetMenu(fileName = "AttackCardSettings", menuName = "WITA/Battle/Cards/AttackCard")]
    public class AttackCardSettings : CardSettings
    {
        public AttackCardType AttackType;
    }
     [CreateAssetMenu(fileName = "DefenceCardSettings", menuName = "WITA/Battle/Cards/DefenceCard")]
    public class DefenceCardSettings : CardSettings
    {
        public DefenceCardType DefenceType;
    }
     [CreateAssetMenu(fileName = "SkillCardSettings", menuName = "WITA/Battle/Cards/SkillCard")]
    public class SkillCardSettings : CardSettings
    {
        public SkillCardType SkillType;
    }
    
}