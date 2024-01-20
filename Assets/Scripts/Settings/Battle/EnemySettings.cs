using System.Collections.Generic;
using UnityEngine;

namespace Settings.Battle
{
    public class EnemySettings : ScriptableObject
    {
        public List<Sprite> EnemySprites;
        public List<EnemySetting> EnemySetting;

        public int EnemySettingCount => EnemySetting.Count;
        public int EnemySpritesCount => EnemySprites.Count;

        public EnemySetting GetEnemySetting(int index)
        {
            if (index >= 0 && index < EnemySetting.Count)
            {
                return EnemySetting[index];
            }

            return null;
        }

        public Sprite GetSprite(int index)
        {
            if (index >= 0 && index < EnemySprites.Count)
            {
                return EnemySprites[index];
            }

            return null;
        }
    }
}