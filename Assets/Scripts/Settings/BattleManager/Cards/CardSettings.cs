using UnityEngine;

namespace Settings.BattleManager.Cards
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
}