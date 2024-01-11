using BattleSystem;
using TMPro;
using UnityEngine;

namespace UI.Battle
{
    public class CardUI : MonoBehaviour
    {
        public static CardUI Instance { get; private set; }

        public TextMeshProUGUI statsText;
        public TextMeshProUGUI energyCostText;
        Animator animator;
        public GameObject discardEffect;

        private void Awake()
        {
            Instance = this;
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            animator.Play("HoverOffCard");
        }

        public void DisplayCardInfo(Card card)
        {
            switch (card.cardType)
            {
                case Card.CardType.Attack:
                    statsText.text = card.damage.ToString();
                    break;

                case Card.CardType.Defense:
                    statsText.text = card.defense.ToString();
                    break;

                case Card.CardType.Special:
                    break;
            }

            energyCostText.text = card.energyCost.ToString();
        }
    }
}