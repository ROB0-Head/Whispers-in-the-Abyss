using System;
using System.Collections.Generic;
using TJ;
using UnityEngine;
using Card = BattleSystem.Card;

namespace Settings
{
    [Serializable]
    public class Character
    {
        public int Health;
        public int Energy;
        public CharacterDeck CurrentDeck;
        public Relic startingRelic;
        public int CurrentTowerFloor;
        public int CurrentGold;
    }
    
    [CreateAssetMenu(fileName = "CharacterDeck", menuName = "WITA/CharacterDeck")]
    public class CharacterDeck : ScriptableObject
    {
        public List<Card> Deck;
    }
    [CreateAssetMenu(fileName = "DeckLibrary", menuName = "WITA/DeckLibrary")]
    public class DeckLibrary : ScriptableObject
    {
        public List<Card> Deck;
    }
    [CreateAssetMenu(fileName = "TarotLibrary", menuName = "WITA/DeckLibrary")]
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
