using System;
using System.Collections.Generic;
using BattleSystem;

namespace Settings
{
    [Serializable]
    public class UserData
    {
        public int Health;
        public int Energy;
        public List<Card> Cards;
        public UserData()
        {
            Cards = new List<Card>();
        }
    }
}
