using System.Collections.Generic;
using BattleSystem.Characters.Enemy;
using UnityEngine;

namespace Settings.Battle
{
    [CreateAssetMenu(fileName = "EnemySetting", menuName = "WITA/Battle/Enemies/EnemySetting")]
    public class EnemySetting : ScriptableObject
    {
        public Enemy EnemyPrefab;
        public List<EnemyAction> enemyActions;
        public int MaxHeath;
    }
}