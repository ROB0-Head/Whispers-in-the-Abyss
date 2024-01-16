using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Settings.BattleManager.Cards
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardTitleText;
        [SerializeField] private TMP_Text _cardDescriptionText;
        [SerializeField] private TMP_Text _cardStat;
        [SerializeField] private TMP_Text _cardEnergy;
        [SerializeField] private GameObject _discardEffect;
        [SerializeField] private Animator animator;

        public GameObject DiscardEffect => _discardEffect;
        public string CardTitle => _cardTitleText.ToString();
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            /*
            animator.Play("HoverOffCard");
        */
        }

        public void LoadCard(Card _card)
        {
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _cardTitleText.text = _card.CardTitle;
            /*cardDescriptionText.text = _card.GetCardDescriptionAmount();*/
            _cardEnergy.text = _card.GetCardCostAmount().ToString();
            _cardStat.text = _card.GetCardEffectAmount().ToString();
        }

        public void SelectCard()
        {
            //Debug.Log("card is selected");
            BattleSystem.BattleManager.Instance.SelectedCard = this;
        }

        public void DeselectCard()
        {
            //Debug.Log("card is deselected");
            BattleSystem.BattleManager.Instance.SelectedCard = null;
            animator.Play("HoverOffCard");
        }

        public void HoverCard()
        {
            if (BattleSystem.BattleManager.Instance.SelectedCard == null)
                animator.Play("HoverOnCard");
        }

        public void DropCard()
        {
            if (BattleSystem.BattleManager.Instance.SelectedCard == null)
                animator.Play("HoverOffCard");
        }

        public void HandleDrag()
        {
        }

        public void HandleEndDrag()
        {
            /*if ( BattleManager.Instance.Energy < card.GetCardCostAmount())
                return;

            if (card.cardType == Card.CardType.Attack)
            {
                BattleManager.Instance.PlayCard(this);
                animator.Play("HoverOffCard");
            }
            else if (card.cardType != Card.CardType.Attack)
            {
                animator.Play("HoverOffCard");
                BattleManager.Instance.PlayCard(this);
            }*/
        }
    }
}