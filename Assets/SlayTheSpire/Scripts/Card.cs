using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    public class SkillCard : Card
    {
        public SkillCardType SkillType;
    }

    public class AttackCard : Card
    {
        public AttackCardType AttackType;
    }

    public class DefenseCard : Card
    {
        public DefenceCardType DefenceType;
    }

    public class Card : MonoBehaviour
    {
        protected bool isUpgraded;
        protected CardDescription cardDescription;
        protected CardAmount cardCost;
        protected CardAmount cardEffect;
        protected CardAmount buffAmount;

        public CardType CardType;

        public int GetCardCostAmount()
        {
            if (!isUpgraded)
                return cardCost.baseAmount;

            return cardCost.upgradedAmount;
        }

        public int GetCardEffectAmount()
        {
            if (!isUpgraded)
                return cardEffect.baseAmount;

            return cardEffect.upgradedAmount;
        }

        public string GetCardDescriptionAmount()
        {
            if (!isUpgraded)
                return cardDescription.baseAmount;

            return cardDescription.upgradedAmount;
        }

        public int GetBuffAmount()
        {
            if (!isUpgraded)
                return buffAmount.baseAmount;

            return buffAmount.upgradedAmount;
        }
    }

    public enum CardType
    {
        Attack,
        Defence,
        Skill,
        Power
    }

    public enum AttackCardType
    {
        Strike,
        Bash,
        Inflame,
        Clothesline,
        IronWave,
        Bloodletting,
        Bodyslam,
        Entrench,
    }

    public enum DefenceCardType
    {
        Defence,
        Entrench,
        ShrugItOff,
        IronWave
    }

    public enum SkillCardType
    {
        Bloodletting,
        Inflame
    }

    [Serializable]
    public struct CardAmount
    {
        public int baseAmount;
        public int upgradedAmount;
    }

    [Serializable]
    public struct CardDescription
    {
        public string baseAmount;
        public string upgradedAmount;
    }

    [Serializable]
    public struct CardBuffs
    {
        public Buff.Type buffType;
        public CardAmount buffAmount;
    }
}