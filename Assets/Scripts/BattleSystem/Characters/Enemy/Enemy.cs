using System.Collections;
using System.Collections.Generic;
using Settings.BattleManager;
using TJ;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem.Characters.Enemy
{
    public class Enemy : Fighter
    {
        public List<EnemyAction> enemyActions;
        public List<EnemyAction> turns = new List<EnemyAction>();
        public int turnNumber;
        public bool shuffleActions;

        public Image intentIcon;
        public TMP_Text intentAmount;
        public BuffUI intentUI;

        private Animator animator;
        public bool midTurn;

        private void Start()
        {
            animator = GetComponent<Animator>();
            if (shuffleActions)
                GenerateTurns();
        }

        private void LoadEnemy()
        {
            if (shuffleActions)
                GenerateTurns();
        }

        public void TakeTurn()
        {
            intentUI.animator.Play("IntentFade");

            switch (turns[turnNumber].intentType)
            {
                case EnemyAction.IntentType.Attack:
                    StartCoroutine(AttackPlayer());
                    break;
                case EnemyAction.IntentType.Block:
                    PerformBlock();
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.StrategicBuff:
                    ApplyBuffToSelf(turns[turnNumber].buffType);
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.StrategicDebuff:
                    ApplyDebuffToPlayer(turns[turnNumber].buffType);
                    StartCoroutine(ApplyBuff());
                    break;
                case EnemyAction.IntentType.AttackDebuff:
                    ApplyDebuffToPlayer(turns[turnNumber].buffType);
                    StartCoroutine(AttackPlayer());
                    break;
                default:
                    Debug.Log("lmao how did you fuck this up");
                    break;
            }
        }

        public void GenerateTurns()
        {
            foreach (EnemyAction eA in enemyActions)
            {
                for (int i = 0; i < eA.chance; i++)
                {
                    turns.Add(eA);
                }
            }

            ListExtensions.Shuffle(turns);
        }

        private IEnumerator AttackPlayer()
        {
            animator.Play("Attack");

            int totalDamage = 0;

            foreach (var buffs in BuffList)
            {
                if (buffs.BuffType == Buff.Type.Strength)
                {
                    totalDamage = turns[turnNumber].amount + buffs.BuffValue;
                }
            }

            foreach (var buffs in BattleManager.Instance.Player.BuffList)
            {
                if (buffs.BuffType == Buff.Type.Vulnerable && buffs.BuffValue > 0)
                {
                    totalDamage = (int)(totalDamage * 1.5f);
                }
            }

            yield return new WaitForSeconds(0.5f);
            BattleManager.Instance.Player.TakeDamage(totalDamage);
            yield return new WaitForSeconds(0.5f);
            WrapUpTurn();
        }

        private IEnumerator ApplyBuff()
        {
            yield return new WaitForSeconds(1f);
            WrapUpTurn();
        }

        private void WrapUpTurn()
        {
            turnNumber++;
            if (turnNumber == turns.Count)
                turnNumber = 0;

            EvaluateBuffsAtTurnEnd();
            midTurn = false;
        }

        private void ApplyBuffToSelf(Buff.Type t)
        {
            AddBuff(t, turns[turnNumber].amount);
        }

        private void ApplyDebuffToPlayer(Buff.Type t)
        {
            if (BattleManager.Instance.Player == null)
                LoadEnemy();

            BattleManager.Instance.Player.AddBuff(t, turns[turnNumber].debuffAmount);
        }

        private void PerformBlock()
        {
            AddBlock(turns[turnNumber].amount);
        }

        public void DisplayIntent()
        {
            if (turns.Count == 0)
                LoadEnemy();

            intentIcon.sprite = turns[turnNumber].icon;

            if (turns[turnNumber].intentType == EnemyAction.IntentType.Attack)
            {
                int totalDamage = 0;

                foreach (var buffs in BuffList)
                {
                    if (buffs.BuffType == Buff.Type.Strength)
                    {
                        totalDamage = turns[turnNumber].amount + buffs.BuffValue;
                    }
                }

                foreach (var buffs in BattleManager.Instance.Player.BuffList)
                {
                    if (buffs.BuffType == Buff.Type.Vulnerable && buffs.BuffValue > 0)
                    {
                        totalDamage = (int)(totalDamage * 1.5f);
                    }
                }

                intentAmount.text = totalDamage.ToString();
            }
            else
                intentAmount.text = turns[turnNumber].amount.ToString();

            intentUI.animator.Play("IntentSpawn");
        }

        /*public void CurlUp()
        {
            wigglerBuff.SetActive(false);
            thisEnemy.AddBlock(5);
        }*/
    }

    public enum EnemyType
    {
        Default,
        Elite,
        Boss
    }
}