using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TJ
{
    public class Fighter : MonoBehaviour
    {
        [SerializeField] private List<Buff> _buffList;

        public int currentHealth;
        public int maxHealth;
        public int currentBlock = 0;
        public FighterHealthBar fighterHealthBar;

        public GameObject buffPrefab;
        public Transform buffParent;
        public bool isPlayer;
        Enemy enemy;
        BattleSceneManager battleSceneManager;
        GameManager gameManager;
        public GameObject damageIndicator;

        public List<Buff> BuffList { get; private set; }

        private void Awake()
        {
            BuffList = _buffList;
            enemy = GetComponent<Enemy>();
            battleSceneManager = FindObjectOfType<BattleSceneManager>();
            gameManager = FindObjectOfType<GameManager>();

            currentHealth = maxHealth;
            fighterHealthBar.healthSlider.maxValue = maxHealth;
            fighterHealthBar.DisplayHealth(currentHealth);
            /*if(isPlayer)
                gameManager.DisplayHealth(currentHealth, currentHealth);*/
        }

        public void TakeDamage(int amount)
        {
            if (currentBlock > 0)
                amount = BlockDamage(amount);

            if (enemy != null && enemy.wiggler && currentHealth == maxHealth)
                enemy.CurlUp();

            Debug.Log($"dealt {amount} damage");

            var di = Instantiate(damageIndicator, transform);
            di.GetComponent<DamageIndicator>().DisplayDamage(amount);
            Destroy(di, 2f);

            currentHealth -= amount;
            UpdateHealthUI(currentHealth);

            if (currentHealth <= 0)
            {
                if (enemy != null)
                    battleSceneManager.EndFight(true);
                else
                    battleSceneManager.EndFight(false);

                Destroy(gameObject);
            }
        }

        public void UpdateHealthUI(int newAmount)
        {
            currentHealth = newAmount;
            fighterHealthBar.DisplayHealth(newAmount);

            if (isPlayer)
                gameManager.DisplayHealth(newAmount, maxHealth);
        }

        public void AddBlock(int amount)
        {
            currentBlock += amount;
            fighterHealthBar.DisplayBlock(currentBlock);
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }

        private int BlockDamage(int amount)
        {
            if (currentBlock >= amount)
            {
                currentBlock -= amount;
                amount = 0;
            }
            else
            {
                amount -= currentBlock;
                currentBlock = 0;
            }

            fighterHealthBar.DisplayBlock(currentBlock);
            return amount;
        }

        public void AddBuff(Buff.Type type, int amount)
        {
            var currentBuff = _buffList.FirstOrDefault(x => x.BuffType == type);

            if (currentBuff.BuffValue <= 0)
            {
                var buffUI = Instantiate(buffPrefab, buffParent);
                currentBuff.BuffGO = buffUI.GetComponent<BuffUI>();
            }

            currentBuff.BuffValue += amount;
            currentBuff.BuffGO.DisplayBuff(currentBuff);
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
                            buff.BuffGO.DisplayBuff(buff);

                            if (buff.BuffValue <= 0)
                                Destroy(buff.BuffGO.gameObject);
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
                    Destroy(buff.BuffGO.gameObject);
                }
            }

            currentBlock = 0;
            fighterHealthBar.DisplayBlock(0);
        }
    }
}