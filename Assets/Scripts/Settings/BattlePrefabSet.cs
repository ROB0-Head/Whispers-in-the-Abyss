using System.Collections.Generic;
using System.Linq;
using BattleSystem;
using BattleSystem.Characters.Enemy;
using BattleSystem.Characters.Reward;
using Settings.BattleManager.Cards;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "BattlePrefabSet", menuName = "WITA/BattlePrefabSet", order = 0)]
    public class BattlePrefabSet : ScriptableObject
    {
        [field: SerializeField] public DeckLibrary DeckLibrary { private set; get; }
        [field: SerializeField] public RelicLibrary RelicLibrary { private set; get; }
        [field: SerializeField] public TarotLibrary TarotLibrary { private set; get; }
        [field: SerializeField] public List<Enemy> Enemies { private set; get; }
        [field: SerializeField] public BuffUI BuffUIPrefab { private set; get; }

        public T GetEnemy<T>() where T : Enemy
        {
            return Enemies.First(x => x is T) as T;
        }
    }
}