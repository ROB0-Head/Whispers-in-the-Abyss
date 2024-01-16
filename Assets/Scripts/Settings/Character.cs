using System;
using System.Collections.Generic;
using Settings.BattleManager.Cards;
using TJ;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings
{
    public class Character
    {
        public int Health;
        public int Energy;
        public Relic startingRelic;
        public int CurrentTowerFloor;
        public int CurrentGold;
        public DateTime FirstLaunchDateTime;

        public Character()
        {
            Health = 100;
            Energy = 3;
            startingRelic = null;
            CurrentTowerFloor = 1;
            CurrentGold = 0;
        }
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