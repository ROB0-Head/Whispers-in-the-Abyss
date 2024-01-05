using UI.Battle;
using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Card", menuName = "WITA/Card", order = 0)]
    public class Card : ScriptableObject
    {
        public int damage;
        public int defense;
        public int energyCost;
        public CardType cardType;
        public GameObject cardPrefab;

        public void Play()
        {
            CardUI.Instance.DisplayCardInfo(this);
            switch (cardType)
            {
                case CardType.Attack:
                    break;

                case CardType.Defense:
                    break;

                case CardType.Special:
                    break;
            }
        }

        public enum CardType
        {
            Attack,
            Defense,
            Special
        }
    }
}