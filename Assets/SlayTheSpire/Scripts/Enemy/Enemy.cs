using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Settings.BattleManager;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TJ
{
    public class Enemy : MonoBehaviour
    {
        public List<EnemyAction> enemyActions;
        public List<EnemyAction> turns = new List<EnemyAction>();
        public int turnNumber;
        public bool shuffleActions;
        public Fighter thisEnemy;

        [Header("UI")] public Image intentIcon;
        public TMP_Text intentAmount;
        public BuffUI intentUI;

        [Header("Specifics")] public int goldDrop;
        public bool bird;
        public bool nob;
        public bool wiggler;
        public GameObject wigglerBuff;
        public GameObject nobBuff;
        Fighter player;
        Animator animator;
        public bool midTurn;

        private void Start()
        {
            /*
            player = battleSceneManager.player;
            */
            thisEnemy = GetComponent<Fighter>();
            animator = GetComponent<Animator>();

            if (shuffleActions)
                GenerateTurns();
        }

        private void LoadEnemy()
        {
            /*
            player = battleSceneManager.player;
            */
            thisEnemy = GetComponent<Fighter>();

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

            turns.Shuffle();
        }

        private IEnumerator AttackPlayer()
        {
            animator.Play("Attack");
            /*if (bird)
                battleSceneManager.birdIcon.GetComponent<Animator>().Play("Attack");*/

            int totalDamage = 0;

            foreach (var buffs in thisEnemy.BuffList)
            {
                if (buffs.BuffType == Buff.Type.Strength)
                {
                    totalDamage = turns[turnNumber].amount + buffs.BuffValue;
                }
            }

            foreach (var buffs in player.BuffList)
            {
                if (buffs.BuffType == Buff.Type.Vulnerable && buffs.BuffValue > 0)
                {
                    totalDamage = (int)(totalDamage * 1.5f);
                }
            }

            yield return new WaitForSeconds(0.5f);
            player.TakeDamage(totalDamage);
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

            if (bird)
                turnNumber = 1;

            if (nob && turnNumber == 0)
                turnNumber = 1;

            thisEnemy.EvaluateBuffsAtTurnEnd();
            midTurn = false;
        }

        private void ApplyBuffToSelf(Buff.Type t)
        {
            thisEnemy.AddBuff(t, turns[turnNumber].amount);
        }

        private void ApplyDebuffToPlayer(Buff.Type t)
        {
            if (player == null)
                LoadEnemy();

            player.AddBuff(t, turns[turnNumber].debuffAmount);
        }

        private void PerformBlock()
        {
            thisEnemy.AddBlock(turns[turnNumber].amount);
        }

        public void DisplayIntent()
        {
            if (turns.Count == 0)
                LoadEnemy();

            intentIcon.sprite = turns[turnNumber].icon;

            if (turns[turnNumber].intentType == EnemyAction.IntentType.Attack)
            {
                int totalDamage = 0;

                foreach (var buffs in thisEnemy.BuffList)
                {
                    if (buffs.BuffType == Buff.Type.Strength)
                    {
                        totalDamage = turns[turnNumber].amount + buffs.BuffValue;
                    }
                }

                foreach (var buffs in player.BuffList)
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

        public void CurlUp()
        {
            wigglerBuff.SetActive(false);
            thisEnemy.AddBlock(5);
        }
    }

    public enum EnemyType
    {
        Default,
        Elite,
        Boss
    }
}