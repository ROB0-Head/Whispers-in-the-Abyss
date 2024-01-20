using BattleSystem.Cards;
using UnityEngine;

namespace Settings.Battle.Cards
{
    public class CardSettings : ScriptableObject
    {
        public string CardTitle;
        public bool IsUpgraded;
        public CardDescription CardDescription;
        public CardAmount CardCost;
        public CardAmount CardEffect;
        public CardAmount BuffAmount;
        public CardType CardType;
        public Sprite CardSprite;
    }
}