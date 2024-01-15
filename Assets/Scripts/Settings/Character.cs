using System;
using System.Collections.Generic;
using TJ;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class Character
    {
        public int Health;
        public int Energy;
        public Relic startingRelic;
        public int CurrentTowerFloor;
        public int CurrentGold;
        public DateTime FirstLaunchDateTime;

        // Default Constructor
        public Character()
        {
            Health = 100;  
            Energy = 3;   
            startingRelic = null; 
            CurrentTowerFloor = 1;  
            CurrentGold = 0;
        }
    }

    
    [CreateAssetMenu(fileName = "CharacterDeck", menuName = "WITA/Battle/CharacterDeck")]
    public class CharacterDeck : ScriptableObject
    {
        public List<Card> Deck;
    }
    [CreateAssetMenu(fileName = "DeckLibrary", menuName = "WITA/Battle/DeckLibrary")]
    public class DeckLibrary : ScriptableObject
    {
        public List<Card> Deck;
        
        public Card GetRandomCard()
        {
            if (Deck.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, Deck.Count);
                return Deck[randomIndex];
            }

            return null;
        }
    }
    [CreateAssetMenu(fileName = "TarotLibrary", menuName = "WITA/Battle/TarotLibrary")]
    public class TarotLibrary : ScriptableObject
    {
        public List<Relic> Tarots;
    }
    
    public enum RelicType
    {
        PreservedInsect,
        Anchor,
        Lantern,
        Marbles,
        Bag,
        Varja,
        BurningBlood
    }
}
