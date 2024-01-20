using UnityEngine;

namespace Settings.Battle
{
    [CreateAssetMenu(fileName = "EnemyLibrary", menuName = "WITA/Battle/Enemies/EnemyLibrary")]
    public class EnemyLibrary : ScriptableObject
    {
        public EnemySettings DefaultEnemySetting;
        public EnemySettings EliteEnemySetting;
        public EnemySettings BossEnemySetting;


        private EnemySetting GetRandomEnemySetting(EnemySettings enemySettings)
        {
            if (enemySettings.EnemySettingCount > 0)
            {
                int randomIndex = Random.Range(0, enemySettings.EnemySettingCount);
                return enemySettings.GetEnemySetting(randomIndex);
            }

            return null;
        }

        public EnemySetting GetRandomDefaultEnemySetting()
        {
            return GetRandomEnemySetting(DefaultEnemySetting);
        }

        public EnemySetting GetRandomEliteEnemySetting()
        {
            return GetRandomEnemySetting(EliteEnemySetting);
        }

        public EnemySetting GetRandomBossEnemySetting()
        {
            return GetRandomEnemySetting(BossEnemySetting);
        }

        public Sprite GetRandomDefaultEnemySprite()
        {
            return GetRandomSprite(DefaultEnemySetting);
        }

        public Sprite GetRandomEliteEnemySprite()
        {
            return GetRandomSprite(EliteEnemySetting);
        }

        public Sprite GetRandomBossEnemySprite()
        {
            return GetRandomSprite(BossEnemySetting);
        }

        private Sprite GetRandomSprite(EnemySettings enemySettings)
        {
            if (enemySettings.EnemySpritesCount > 0)
            {
                int randomIndex = Random.Range(0, enemySettings.EnemySpritesCount);
                return enemySettings.GetSprite(randomIndex);
            }

            return null;
        }
    }
}