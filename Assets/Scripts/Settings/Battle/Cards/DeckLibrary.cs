using System.Collections.Generic;
using UnityEngine;

namespace Settings.Battle.Cards
{
    [CreateAssetMenu(fileName = "DeckLibrary", menuName = "WITA/Battle/DeckLibrary")]
    public class DeckLibrary : ScriptableObject
    {
        public List<CardSettings> Deck;
        public GameObject CardPrefab;
        public GameObject SkillCardPrefab;

        public CardSettings GetRandomCard()
        {
            if (Deck.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, Deck.Count);
                return Deck[randomIndex];
            }

            return null;
        }
    }
}