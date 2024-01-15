using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings.BattleManager.Cards
{
    public class SkillCard : Card
    {
        public SkillCardType SkillType;

        public SkillCard(bool isUpgraded, CardDescription cardDescription, CardAmount cardCost, CardAmount cardEffect, CardAmount buffAmount, CardType cardType) : base(isUpgraded, cardDescription, cardCost, cardEffect, buffAmount, cardType)
        {
        }
    }

    public class AttackCard : Card
    {
        public AttackCardType AttackType;

        public AttackCard(bool isUpgraded, CardDescription cardDescription, CardAmount cardCost, CardAmount cardEffect, CardAmount buffAmount, CardType cardType) : base(isUpgraded, cardDescription, cardCost, cardEffect, buffAmount, cardType)
        {
        }
    }

    public class DefenseCard : Card
    {
        public DefenceCardType DefenseType;

        public DefenseCard(bool isUpgraded, CardDescription cardDescription, CardAmount cardCost, CardAmount cardEffect, CardAmount buffAmount, CardType cardType) : base(isUpgraded, cardDescription, cardCost, cardEffect, buffAmount, cardType)
        {
        }
    }

    public class Card : ScriptableObject
    {
        public bool isUpgraded;
        public CardDescription cardDescription;
        public CardAmount cardCost;
        public CardAmount cardEffect;
        public CardAmount buffAmount;

        public CardType CardType;

        public Card(bool isUpgraded, CardDescription cardDescription, CardAmount cardCost, CardAmount cardEffect, CardAmount buffAmount, CardType cardType)
        {
            this.isUpgraded = isUpgraded;
            this.cardDescription = cardDescription;
            this.cardCost = cardCost;
            this.cardEffect = cardEffect;
            this.buffAmount = buffAmount;
            this.CardType = cardType;
        }
        
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
        Defense,
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
        Defense,
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