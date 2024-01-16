using TMPro;
using UnityEngine;

namespace BattleSystem.Characters.BattleScreen
{
    public class DamageIndicator : MonoBehaviour
    {
        public Animator animator;
        public TMP_Text text;

        public void DisplayDamage(int amount)
        {
            text.text = amount.ToString();
            animator.Play("DisplayDamage");
        }
    }
}