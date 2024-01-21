using System;
using System.Collections.Generic;
using BattleSystem.Characters.Reward;

namespace Settings
{
    public class Character
    {
        public int MaxHealth;
        public int CurrentHealth;
        public int Energy;
        public RelicType startingRelic;
        public int CurrentTowerFloor;
        public int CurrentGold;
        public DateTime FirstLaunchDateTime;
        public List<string> HistoriesNames;
        public Character()
        {
            MaxHealth = 100;
            CurrentHealth = 100;
            Energy = 3;
            startingRelic = RelicType.None;
            CurrentTowerFloor = 1;
            CurrentGold = 0;
        }
    }
}