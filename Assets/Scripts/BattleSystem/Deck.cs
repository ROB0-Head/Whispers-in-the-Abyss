using System.Collections.Generic;
using UnityEngine;

namespace BattleSystem
{
    [CreateAssetMenu(fileName = "Deck", menuName = "WITA/CardDeck", order = 0)]
    public class Deck : ScriptableObject
    {
        public List<Card> CardDeck;
    }
}
