using System.Collections;
using System.Collections.Generic;
using BattleSystem;
using Settings.BattleManager;
using Settings.BattleManager.Cards;
using UnityEngine;

namespace TJ
{
    public class CardActions : MonoBehaviour
    {
        Card card;
        public Fighter target;
        public Fighter player;

        public void PerformAction(Card _card, Fighter _fighter)
        {
            card = _card;
            target = _fighter;

            if (_card.CardType == CardType.Attack && _card is AttackCard attackCard)
            {
                switch (attackCard.AttackType)
                {
                    case AttackCardType.Strike:
                        AttackEnemy();
                        break;
                    case AttackCardType.Bash:
                        AttackEnemy();
                        ApplyBuff(Buff.Type.Vulnerable);
                        break;
                    
                    case AttackCardType.Clothesline:
                        AttackEnemy();
                        ApplyBuff(Buff.Type.Weak);
                        break;
                    
                    case AttackCardType.Bodyslam:
                        BodySlam();
                        break;
                    default:
                        Debug.Log("Invalid Attack Type");
                        break;
                }
            }

            if (_card.CardType == CardType.Defense && _card is DefenseCard defenceCard)
            {
                switch (defenceCard.DefenseType)
                {
                    case DefenceCardType.Defense:
                        PerformBlock();
                        break;
                    case DefenceCardType.Entrench:
                        Entrench();
                        break;
                    case DefenceCardType.ShrugItOff:
                        Entrench();
                        break;
                    case DefenceCardType.IronWave:
                        AttackEnemy();
                        PerformBlock();
                        break;
                }
            }

            if (_card.CardType == CardType.Skill && _card is SkillCard skillCard)
            {
                switch (skillCard.SkillType)
                {
                    case SkillCardType.Bloodletting:
                        AttackSelf();
                        /*
                        BattleManager.Instance.Energy += 2;
                        */
                        break;
                    case SkillCardType.Inflame:
                        ApplyBuffToSelf(Buff.Type.Strength);
                        break;
                }
            }
        }


        private void AttackEnemy()
        {
            int totalDamage = 0;
            foreach (var buffs in player.BuffList)
            {
                if (buffs.BuffType == Buff.Type.Strength)
                {
                    totalDamage = card.GetCardEffectAmount() + buffs.BuffValue;
                }

                foreach (var currentBuff in target.BuffList)
                {
                    if (currentBuff.BuffType == Buff.Type.Vulnerable && currentBuff.BuffValue > 0)
                    {
                        float a = totalDamage * 1.5f;
                        Debug.Log("incrased damage from " + totalDamage + " to " + (int)a);
                        totalDamage = (int)a;
                    }
                }
            }

            target.TakeDamage(totalDamage);
        }

        private void AttackStrength()
        {
            int totalDamage = 0;
            foreach (var buffs in player.BuffList)
            {
                if (buffs.BuffType == Buff.Type.Strength && buffs.BuffValue > 0)
                {
                    totalDamage = card.GetCardEffectAmount() + (buffs.BuffValue * 3);
                }

                foreach (var currentBuff in target.BuffList)
                {
                    if (currentBuff.BuffType == Buff.Type.Vulnerable && currentBuff.BuffValue > 0)
                    {
                        float a = totalDamage * 1.5f;
                        Debug.Log("incrased damage from " + totalDamage + " to " + (int)a);
                        totalDamage = (int)a;
                    }
                }
            }

            target.TakeDamage(totalDamage);
        }

        private void BodySlam()
        {
            int totalDamage = player.currentBlock;

            foreach (var currentBuff in target.BuffList)
            {
                if (currentBuff.BuffType == Buff.Type.Vulnerable && currentBuff.BuffValue > 0)
                {
                    float a = totalDamage * 1.5f;
                    Debug.Log("incrased damage from " + totalDamage + " to " + (int)a);
                    totalDamage = (int)a;
                }
            }

            target.TakeDamage(totalDamage);
        }

        private void Entrench()
        {
            player.AddBlock(player.currentBlock);
        }

        private void ApplyBuff(Buff.Type t)
        {
            target.AddBuff(t, card.GetBuffAmount());
        }

        private void ApplyBuffToSelf(Buff.Type t)
        {
            player.AddBuff(t, card.GetBuffAmount());
        }

        private void AttackSelf()
        {
            player.TakeDamage(2);
        }

        private void PerformBlock()
        {
            player.AddBlock(card.GetCardEffectAmount());
        }
    }
}