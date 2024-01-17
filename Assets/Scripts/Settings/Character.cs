using System;
using BattleSystem.Characters.Reward;

namespace Settings
{
    public class Character
    {
        public int MaxHealth;
        public int CurrentHealth;
        public int Energy;
        public Relic startingRelic;
        public int CurrentTowerFloor;
        public int CurrentGold;
        public DateTime FirstLaunchDateTime;

        public Character()
        {
            MaxHealth = 100;
            CurrentHealth = 100;
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