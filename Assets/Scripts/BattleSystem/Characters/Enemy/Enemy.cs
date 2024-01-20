using System.Collections;
using System.Collections.Generic;
using Settings.Battle;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem.Characters.Enemy
{
    public class Enemy : Fighter
    {
        [SerializeField] private Image _enemyCard;
        [SerializeField] private Image _intentIcon;
        [SerializeField] private TMP_Text _intentAmount;
        [SerializeField] private BuffUI _intentUI;

        private List<EnemyAction> _turns = new List<EnemyAction>();
        private Animator _animator;
        private int _turnNumber;
        private List<EnemyAction> _enemyActions;
        public bool MidTurn { get; set; }

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public override void Init(EnemySetting setting, Sprite enemyCard)
        {
            _enemyCard.sprite = enemyCard;
            BuffList = new List<Buff>();
            _enemyActions = setting.enemyActions;
            MaxHealth = setting.MaxHeath;
            SetupCurrentHealth(setting.MaxHeath);
            HealthBar.DisplayHealth(CurrentHealth);
        }

        public void SetupCurrentHealth(int value)
        {
            CurrentHealth = value;
        }

        public void TakeTurn()
        {
            _intentUI.animator.Play("IntentFade");

            switch (_turns[_turnNumber].IntentType)
            {
                case IntentType.Attack:
                    StartCoroutine(AttackPlayer());
                    break;
                case IntentType.Block:
                    PerformBlock();
                    StartCoroutine(ApplyBuff());
                    break;
                case IntentType.StrategicBuff:
                    ApplyBuffToSelf(_turns[_turnNumber].BuffType);
                    StartCoroutine(ApplyBuff());
                    break;
                case IntentType.StrategicDebuff:
                    ApplyDebuffToPlayer(_turns[_turnNumber].BuffType);
                    StartCoroutine(ApplyBuff());
                    break;
                case IntentType.AttackDebuff:
                    ApplyDebuffToPlayer(_turns[_turnNumber].BuffType);
                    StartCoroutine(AttackPlayer());
                    break;
                default:
                    Debug.Log("lmao how did you fuck this up");
                    break;
            }
        }

        public void GenerateTurns()
        {
            foreach (EnemyAction enemyAction in _enemyActions)
            {
                for (int i = 0; i < enemyAction.Chance; i++)
                {
                    _turns.Add(enemyAction);
                }
            }

            _turns.Shuffle();
        }

        private IEnumerator AttackPlayer()
        {
            _animator.Play("Attack");

            int totalDamage = _turns[_turnNumber].IntentAmount;

            foreach (var buffs in BuffList)
            {
                if (buffs.BuffsType == BuffType.Strength)
                {
                    totalDamage += buffs.BuffValue;
                }
            }

            foreach (var buffs in BattleManager.Instance.Player.BuffList)
            {
                if (buffs.BuffsType == BuffType.Vulnerable && buffs.BuffValue > 0)
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
            _turnNumber++;
            if (_turnNumber == _turns.Count)
                _turnNumber = 0;

            EvaluateBuffsAtTurnEnd();
            MidTurn = false;
        }

        private void ApplyBuffToSelf(BuffType type)
        {
            AddBuff(type, _turns[_turnNumber].IntentAmount);
        }

        private void ApplyDebuffToPlayer(BuffType type)
        {
            if (BattleManager.Instance.Player == null)
                GenerateTurns();

            BattleManager.Instance.Player.AddBuff(type, _turns[_turnNumber].DebuffAmount);
        }

        private void PerformBlock()
        {
            AddBlock(_turns[_turnNumber].IntentAmount);
        }

        public void DisplayIntent()
        {
            if (_turns.Count == 0)
                GenerateTurns();

            _intentIcon.sprite = _turns[_turnNumber].Icon;

            if (_turns[_turnNumber].IntentType == IntentType.Attack)
            {
                int totalDamage = _turns[_turnNumber].IntentAmount;

                foreach (var buffs in BuffList)
                {
                    if (buffs.BuffsType == BuffType.Strength)
                    {
                        totalDamage += buffs.BuffValue;
                    }
                }

                foreach (var buffs in BattleManager.Instance.Player.BuffList)
                {
                    if (buffs.BuffsType == BuffType.Vulnerable && buffs.BuffValue > 0)
                    {
                        totalDamage = (int)(totalDamage * 1.5f);
                    }
                }

                _intentAmount.text = totalDamage.ToString();
            }
            else
                _intentAmount.text = _turns[_turnNumber].IntentAmount.ToString();

            _intentUI.animator.Play("IntentSpawn");
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