using System.Collections.Generic;
using BattleSystem.Characters.Reward;
using UnityEngine;

namespace Settings.Battle
{
    [CreateAssetMenu(fileName = "TarotLibrary", menuName = "WITA/Battle/TarotLibrary")]
    public class TarotLibrary : ScriptableObject
    {
        public List<Relic> Tarots;
    }
}