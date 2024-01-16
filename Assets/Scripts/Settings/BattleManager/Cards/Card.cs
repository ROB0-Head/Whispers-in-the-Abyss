using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings.BattleManager.Cards
{
    public class SkillCard : Card
    {
        public SkillCardType SkillType;

        public SkillCard(string cardTitle, bool isUpgraded, CardDescription cardDescription, CardAmount cardCost,
            CardAmount cardEffect, CardAmount buffAmount, CardType cardType) : base(cardTitle, isUpgraded,
            cardDescription, cardCost, cardEffect, buffAmount, cardType)
        {
        }
    }

    public class AttackCard : Card
    {
        public AttackCardType AttackType;

        public AttackCard(string cardTitle, bool isUpgraded, CardDescription cardDescription, CardAmount cardCost,
            CardAmount cardEffect, CardAmount buffAmount, CardType cardType) : base(cardTitle, isUpgraded,
            cardDescription, cardCost, cardEffect, buffAmount, cardType)
        {
        }
    }

    public class DefenseCard : Card
    {
        public DefenceCardType DefenseType;

        public DefenseCard(string cardTitle, bool isUpgraded, CardDescription cardDescription, CardAmount cardCost,
            CardAmount cardEffect, CardAmount buffAmount, CardType cardType) : base(cardTitle, isUpgraded,
            cardDescription, cardCost, cardEffect, buffAmount, cardType)
        {
        }
    }

    public class Card
    {
        public string CardTitle;
        public bool IsUpgraded;
        public CardDescription CardDescription;
        public CardAmount CardEnergy;
        public CardAmount CardStat;
        public CardAmount BuffAmount;
        public CardType CardType;
        
        public Card(string cardTitle, bool isUpgraded, CardDescription cardDescription, CardAmount cardCost,
            CardAmount cardEffect, CardAmount buffAmount, CardType cardType)
        {
            CardTitle = cardTitle;
            IsUpgraded = isUpgraded;
            CardDescription = cardDescription;
            CardEnergy = cardCost;
            CardStat = cardEffect;
            BuffAmount = buffAmount;
            CardType = cardType;
        }

        public int GetCardEnergyAmount()
        {
            if (!IsUpgraded)
                return CardEnergy.baseAmount;

            return CardEnergy.upgradedAmount;
        }

        public int GetCardStatAmount()
        {
            if (!IsUpgraded)
                return CardStat.baseAmount;

            return CardStat.upgradedAmount;
        }

        public string GetCardDescriptionAmount()
        {
            if (!IsUpgraded)
                return CardDescription.baseAmount;

            return CardDescription.upgradedAmount;
        }

        public int GetBuffAmount()
        {
            if (!IsUpgraded)
                return BuffAmount.baseAmount;

            return BuffAmount.upgradedAmount;
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
        Clothesline,
        Bodyslam,
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