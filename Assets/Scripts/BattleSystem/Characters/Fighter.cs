using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters.BattleScreen;
using SaveSystem;
using Settings;
using Settings.Battle;
using UnityEngine;

namespace BattleSystem.Characters
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private FighterHealthBar FighterHealthBar;
        [SerializeField] private Transform _buffParent;
        [SerializeField] private GameObject _damageIndicator;

        public int MaxHealth { get; protected set; }
        public int CurrentHealth { get; protected set; }
        public List<Buff> BuffList { get; protected set; }

        private int _currentBlock;
        public int CurrentBlock => _currentBlock;
        public FighterHealthBar HealthBar => FighterHealthBar;

        public virtual void Init(EnemySetting setting = null, Sprite enemyCard = null)
        {
            var characterData = SaveManager.LoadCharacterData();
            BuffList = new List<Buff>();
            MaxHealth = characterData.MaxHealth;
            CurrentHealth = characterData.CurrentHealth;
            FighterHealthBar.DisplayHealth(CurrentHealth);
        }

        public void TakeDamage(int amount)
        {
            if (_currentBlock > 0)
                amount = BlockDamage(amount);

            var di = Instantiate(_damageIndicator, transform);
            di.GetComponent<DamageIndicator>().DisplayDamage(amount);
            Destroy(di, 2f);
            
            CurrentHealth -= amount;

           
            UpdateHealthUI(CurrentHealth);

            if (CurrentHealth <= 0)
            {
                if (this is Enemy.Enemy enemy)
                {
                    Destroy(gameObject);
                    BattleManager.Instance.RemoveEnemy(enemy);
                    if (BattleManager.Instance.Enemies.IsNullOrEmpty() && BattleManager.Instance.Enemies.Count <= 0)
                        BattleManager.Instance.EndFight(BattleState.Victory);
                }
                else
                {
                    if (!BattleManager.Instance.Enemies.IsNullOrEmpty() && BattleManager.Instance.Enemies.Count >= 0)
                        BattleManager.Instance.EndFight(BattleState.Defeat);
                }
            }
        }

        public void UpdateHealthUI(int newAmount)
        {
            CurrentHealth = newAmount;
            FighterHealthBar.DisplayHealth(newAmount);
        }

        public void AddBlock(int amount)
        {
            _currentBlock += amount;
            FighterHealthBar.DisplayBlock(_currentBlock);
        }

        private int BlockDamage(int amount)
        {
            if (_currentBlock >= amount)
            {
                _currentBlock -= amount;
                amount = 0;
            }
            else
            {
                amount -= _currentBlock;
                _currentBlock = 0;
            }

            FighterHealthBar.DisplayBlock(_currentBlock);
            return amount;
        }

        public void AddBuff(BuffType type, int amount)
        {
            var currentBuff = BuffList.FirstOrDefault(x => x.BuffsType == type);
            var buffSettings = SettingsProvider.Get<BattlePrefabSet>().BuffSettings;

            if (currentBuff == null)
            {
                currentBuff = new Buff()
                {
                    BuffGo = Instantiate(SettingsProvider.Get<BattlePrefabSet>().BuffUIPrefab, _buffParent),
                    BuffIcon = buffSettings.Buffs.FirstOrDefault(x => x.BuffType == type)?.BuffIcon,
                    BuffsType = buffSettings.Buffs.FirstOrDefault(x => x.BuffType == type)!.BuffType,
                };
                BuffList.Add(currentBuff);
            }

            currentBuff.BuffValue += amount;
            currentBuff.BuffGo.DisplayBuff(currentBuff);
        }

        public void EvaluateBuffsAtTurnEnd()
        {
            var listToRemove = new List<Buff>();

            foreach (var buff in BuffList)
            {
                if (buff.BuffValue > 0)
                {
                    switch (buff.BuffsType)
                    {
                        case BuffType.Ritual:
                            AddBuff(BuffType.Strength, buff.BuffValue);
                            break;
                        default:
                            buff.BuffValue -= 1;
                            buff.BuffGo.DisplayBuff(buff);

                            if (buff.BuffValue <= 0)
                            {
                                listToRemove.Add(buff);
                            }

                            break;
                    }
                }
            }

            foreach (var buff in listToRemove)
            {
                Destroy(buff.BuffGo.gameObject);
                BuffList.Remove(buff);
            }
        }

        public void ResetBuffs()
        {
            var listToRemove = new List<Buff>();

            foreach (var buffs in BuffList)
            {
                var buff = buffs;
                if (buff.BuffValue > 0)
                {
                    buff.BuffValue = 0;
                    listToRemove.Add(buff);
                }
            }

            foreach (var buff in listToRemove)
            {
                Destroy(buff.BuffGo.gameObject);
                BuffList.Remove(buff);
            }

            _currentBlock = 0;
            FighterHealthBar.DisplayBlock(0);
        }
    }
}