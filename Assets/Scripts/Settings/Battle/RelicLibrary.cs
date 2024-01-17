using System.Collections.Generic;
using BattleSystem.Characters.Reward;
using UnityEngine;
namespace Settings.Battle
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
}