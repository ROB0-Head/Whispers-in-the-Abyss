using System.Collections.Generic;
using System.Linq;
using BattleSystem.Characters.BattleScreen;
using SaveSystem;
using Settings;
using Settings.BattleManager;
using UnityEngine;

namespace BattleSystem.Characters
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private List<Buff> _buffList = new List<Buff>();

        [SerializeField] private FighterHealthBar _fighterHealthBar;
        [SerializeField] private Transform buffParent;
        [SerializeField] private GameObject damageIndicator;

        private int _maxHealth;
        private int _currentHealth;
        private int _currentBlock = 0;
        public int CurrentBlock => _currentBlock;
        public FighterHealthBar FighterHealthBar => _fighterHealthBar;
        public List<Buff> BuffList => _buffList;

        private void Awake()
        {
            var characterData = SaveManager.LoadCharacterData();
            _maxHealth = characterData.MaxHealth;
            _currentHealth = characterData.CurrentHealth;
            _fighterHealthBar.healthSlider.maxValue = _maxHealth;
            _fighterHealthBar.DisplayHealth(_currentHealth);
        }

        public void TakeDamage(int amount)
        {
            if (_currentBlock > 0)
                amount = BlockDamage(amount);

            var di = Instantiate(damageIndicator, transform);
            di.GetComponent<DamageIndicator>().DisplayDamage(amount);
            Destroy(di, 2f);

            _currentHealth -= amount;
            UpdateHealthUI(_currentHealth);

            if (_currentHealth <= 0)
            {
                if (BattleManager.Instance.Enemies != null)
                    BattleManager.Instance.EndFight(BattleState.Defeat);
                else
                    BattleManager.Instance.EndFight(BattleState.Victory);

                Destroy(gameObject);
            }
        }

        public void UpdateHealthUI(int newAmount)
        {
            _currentHealth = newAmount;
            _fighterHealthBar.DisplayHealth(newAmount);
        }

        public void AddBlock(int amount)
        {
            _currentBlock += amount;
            _fighterHealthBar.DisplayBlock(_currentBlock);
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

            _fighterHealthBar.DisplayBlock(_currentBlock);
            return amount;
        }

        public void AddBuff(Buff.Type type, int amount)
        {
            var currentBuff = _buffList.FirstOrDefault(x => x.BuffType == type);

            if (currentBuff.BuffValue <= 0)
            {
                currentBuff.BuffGo = Instantiate(SettingsProvider.Get<BattlePrefabSet>().BuffUIPrefab, buffParent);
            }

            currentBuff.BuffValue += amount;
            currentBuff.BuffGo.DisplayBuff(currentBuff);
        }

        public void EvaluateBuffsAtTurnEnd()
        {
            foreach (var buffs in _buffList)
            {
                var buff = buffs;
                if (buff.BuffValue > 0)
                {
                    switch (buff.BuffType)
                    {
                        case Buff.Type.Ritual:
                            AddBuff(Buff.Type.Strength, buff.BuffValue);
                            break;
                        default:
                            buff.BuffValue -= 1;
                            buff.BuffGo.DisplayBuff(buff);

                            if (buff.BuffValue <= 0)
                                Destroy(buff.BuffGo.gameObject);
                            break;
                    }
                }
            }
        }

        public void ResetBuffs()
        {
            foreach (var buffs in _buffList)
            {
                var buff = buffs;
                if (buff.BuffValue > 0)
                {
                    buff.BuffValue = 0;
                    Destroy(buff.BuffGo.gameObject);
                }
            }

            _currentBlock = 0;
            _fighterHealthBar.DisplayBlock(0);
        }
    }
}