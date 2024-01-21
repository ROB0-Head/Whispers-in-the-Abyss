using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace BattleSystem.Characters.Reward
{
    [CreateAssetMenu(fileName = "Relic", menuName = "WITA/Battle/Relic")]
    public class Relic : ScriptableObject
    {
        public RelicType RelicType;
        public string relicDescription;
        public Sprite relicIcon;
    }

    public enum RelicType
    {
        None,
        PreservedInsect,
        Anchor,
        Lantern,
        Marbles,
        Bag,
        Varja,
        BurningBlood
    }
}