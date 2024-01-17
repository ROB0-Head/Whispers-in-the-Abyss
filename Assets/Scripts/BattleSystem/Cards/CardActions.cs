using BattleSystem.Characters;
using UnityEngine;

namespace BattleSystem.Cards
{
    public class CardActions : MonoBehaviour
    {
        [SerializeField] private Fighter _player;

        private Fighter _target;

        public void PerformAction(Card card, Fighter _fighter)
        {
            _target = _fighter;

            if (card.CardType == CardType.Attack && card is AttackCard attackCard)
            {
                switch (attackCard.AttackType)
                {
                    case AttackCardType.Strike:
                        AttackEnemy(card);
                        break;
                    case AttackCardType.Bash:
                        AttackEnemy(card);
                        ApplyBuff(BuffType.Vulnerable, card);
                        break;

                    case AttackCardType.Clothesline:
                        AttackEnemy(card);
                        ApplyBuff(BuffType.Weak, card);
                        break;

                    case AttackCardType.Bodyslam:
                        BodySlam();
                        break;
                    case AttackCardType.IronWave:
                        AttackEnemy(card);
                        PerformBlock(card);
                        break;
                    default:
                        Debug.Log("Invalid Attack Type");
                        break;
                }
            }

            if (card.CardType == CardType.Defense && card is DefenseCard defenceCard)
            {
                switch (defenceCard.DefenseType)
                {
                    case DefenceCardType.Defense:
                        PerformBlock(card);
                        break;
                    case DefenceCardType.Entrench:
                        Entrench();
                        break;
                    case DefenceCardType.ShrugItOff:
                        ShrugItOff(card);
                        break;
                }
            }

            if (card.CardType == CardType.Skill && card is SkillCard skillCard)
            {
                switch (skillCard.SkillType)
                {
                    case SkillCardType.Bloodletting:
                        AttackSelf(card);
                        BattleManager.Instance.UpdateEnergy(2);
                        break;
                    case SkillCardType.Inflame:
                        ApplyBuffToSelf(BuffType.Strength, card);
                        break;
                }
            }
        }


        private void AttackEnemy(Card card)
        {
            int totalDamage = card.GetCardStatAmount();
            foreach (var buffs in _player.BuffList)
            {
                if (buffs.BuffsType == BuffType.Strength)
                {
                    totalDamage += buffs.BuffValue;
                }

                foreach (var currentBuff in _target.BuffList)
                {
                    if (currentBuff.BuffsType == BuffType.Vulnerable && currentBuff.BuffValue > 0)
                    {
                        float a = totalDamage * 1.5f;
                        Debug.Log("incrased damage from " + totalDamage + " to " + (int)a);
                        totalDamage = (int)a;
                    }
                }
            }

            _target.TakeDamage(totalDamage);
        }

        private void AttackStrength(Card card)
        {
            int totalDamage = card.GetCardStatAmount();
            foreach (var buffs in _player.BuffList)
            {
                if (buffs.BuffsType == BuffType.Strength && buffs.BuffValue > 0)
                {
                    totalDamage += (buffs.BuffValue * 3);
                }

                foreach (var currentBuff in _target.BuffList)
                {
                    if (currentBuff.BuffsType == BuffType.Vulnerable && currentBuff.BuffValue > 0)
                    {
                        float a = totalDamage * 1.5f;
                        Debug.Log("incrased damage from " + totalDamage + " to " + (int)a);
                        totalDamage = (int)a;
                    }
                }
            }

            _target.TakeDamage(totalDamage);
        }

        private void BodySlam()
        {
            int totalDamage = _player.CurrentBlock;

            foreach (var currentBuff in _target.BuffList)
            {
                if (currentBuff.BuffsType == BuffType.Vulnerable && currentBuff.BuffValue > 0)
                {
                    float a = totalDamage * 1.5f;
                    Debug.Log("incrased damage from " + totalDamage + " to " + (int)a);
                    totalDamage = (int)a;
                }
            }

            _target.TakeDamage(totalDamage);
        }

        private void Entrench()
        {
            _player.AddBlock(_player.CurrentBlock);
        }

        private void ShrugItOff(Card card)
        {
            PerformBlock(card);
            if (card.IsUpgraded)
            {
                BattleManager.Instance.DrawCards(card.BuffAmount.baseAmount);
            }
            else
            {
                BattleManager.Instance.DrawCards(card.BuffAmount.upgradedAmount);
            }
        }

        private void ApplyBuff(BuffType t, Card card)
        {
            _target.AddBuff(t, card.GetBuffAmount());
        }

        private void ApplyBuffToSelf(BuffType t, Card card)
        {
            _player.AddBuff(t, card.GetBuffAmount());
        }

        private void AttackSelf(Card card)
        {
            _player.TakeDamage(card.GetCardStatAmount());
        }

        private void PerformBlock(Card card)
        {
            _player.AddBlock(card.GetCardStatAmount());
        }
    }
}