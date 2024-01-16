using Settings.BattleManager;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem
{
    public class BuffUI : MonoBehaviour
    {
        public Image buffImage;
        public TMP_Text buffAmountText;
        public Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void DisplayBuff(Buff b)
        {
            animator.Play("IntentSpawn");

            buffImage.sprite = b.BuffIcon;
            buffAmountText.text = b.BuffValue.ToString();
        }
    }
}