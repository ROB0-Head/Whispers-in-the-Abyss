using System.Collections.Generic;
using UnityEngine;

namespace TJ
{
    [CreateAssetMenu]
    public class Character : ScriptableObject
    {
        public CharacterClass characterClass;
        public GameObject characterPrefab;
        public Relic startingRelic;
        public Sprite splashArt;
        public List<Card> startingDeck;
    }

    public enum CharacterClass
    {
        ironChad,
        silent
    }
}