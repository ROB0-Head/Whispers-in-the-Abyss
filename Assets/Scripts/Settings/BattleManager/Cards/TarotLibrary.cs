using System.Collections.Generic;
using TJ;
using UnityEngine;

namespace Settings.BattleManager.Cards
{
    [CreateAssetMenu(fileName = "TarotLibrary", menuName = "WITA/Battle/TarotLibrary")]
    public class TarotLibrary : ScriptableObject
    {
        public List<Relic> Tarots;
    }
}