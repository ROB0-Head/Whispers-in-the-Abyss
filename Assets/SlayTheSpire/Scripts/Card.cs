using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    [CreateAssetMenu]
    public class Card : ScriptableObject
    {
        public string cardTitle;
        public bool isUpgraded;
        public CardDescription cardDescription;
        public CardAmount cardCost;
        public CardAmount cardEffect;
        public CardAmount buffAmount;
        public Sprite cardIcon;
        public CardType cardType;

        public enum CardType
        {
            Attack,
            Skill,
            Power
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