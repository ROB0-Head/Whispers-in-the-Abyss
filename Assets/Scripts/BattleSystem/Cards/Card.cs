using System;

namespace BattleSystem.Cards
{
    [Serializable]
    public class SkillCard : Card
    {
        public SkillCardType SkillType;

        public SkillCard(string cardTitle, bool isUpgraded, CardDescription cardDescription, CardAmount cardCost,
            CardAmount cardEffect, CardAmount buffAmount, CardType cardType, SkillCardType skillType) : base(cardTitle,
            isUpgraded,
            cardDescription, cardCost, cardEffect, buffAmount, cardType)
        {
            SkillType = skillType;
        }
    }

    [Serializable]
    public class AttackCard : Card
    {
        public AttackCardType AttackType;

        public AttackCard(string cardTitle, bool isUpgraded, CardDescription cardDescription, CardAmount cardCost,
            CardAmount cardEffect, CardAmount buffAmount, CardType cardType, AttackCardType attackType) : base(
            cardTitle, isUpgraded,
            cardDescription, cardCost, cardEffect, buffAmount, cardType)
        {
            AttackType = attackType;
        }
    }

    [Serializable]
    public class DefenseCard : Card
    {
        public DefenceCardType DefenseType;

        public DefenseCard(string cardTitle, bool isUpgraded, CardDescription cardDescription, CardAmount cardCost,
            CardAmount cardEffect, CardAmount buffAmount, CardType cardType, DefenceCardType defenseType) : base(
            cardTitle, isUpgraded,
            cardDescription, cardCost, cardEffect, buffAmount, cardType)
        {
            DefenseType = defenseType;
        }
    }

    [Serializable]
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
        None,
        Attack,
        Bash,
        Clothesline,
        Bodyslam,
        IronWave
    }

    public enum DefenceCardType
    {
        None,
        Defense,
        Entrench,
        ShrugItOff,
    }

    public enum SkillCardType
    {
        None,
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
        public BuffType buffType;
        public CardAmount buffAmount;
    }
}