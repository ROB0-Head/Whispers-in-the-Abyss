using System.Collections.Generic;
using Settings;
using UnityEngine;

namespace BattleSystem.Characters.Reward
{
    [CreateAssetMenu(fileName = "RelicLibrary", menuName = "WITA/Battle/RelicLibrary")]
    public class RelicLibrary : ScriptableObject
    {
        public List<Relic> Relics;

        public Relic GetRandomRelic()
        {
            if (Relics.Count > 0)
            {
                int randomIndex = Random.Range(0, Relics.Count);
                return Relics[randomIndex];
            }

            return null;
        }
    }

    [CreateAssetMenu(fileName = "Relic", menuName = "WITA/Battle/Relic")]
    public class Relic : ScriptableObject
    {
        public RelicType RelicType;
        public string relicDescription;
        public Sprite relicIcon;
    }
}