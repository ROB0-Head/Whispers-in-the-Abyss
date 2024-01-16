using UnityEngine;
using UnityEngine.Serialization;

namespace Settings.BattleManager.Cards
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
    }
}