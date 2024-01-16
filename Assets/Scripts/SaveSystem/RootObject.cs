using System;
using System.Collections.Generic;
using BattleSystem.Cards;
using UnityEngine.Serialization;

namespace SaveSystem
{
    [Serializable]
    public class RootObject
    {
        public List<CardData> CardsList;
    }

    [Serializable]
    public class CardData
    {
        public AttackCardType AttackType;
        public DefenceCardType DefenseType;
        public SkillCardType SkillType;
        public string CardTitle;
        public bool IsUpgraded;
        public CardDescription CardDescription;
        public CardAmount CardEnergy;
        public CardAmount CardStat;
        public CardAmount BuffAmount;
        public CardType CardType;
    }
}