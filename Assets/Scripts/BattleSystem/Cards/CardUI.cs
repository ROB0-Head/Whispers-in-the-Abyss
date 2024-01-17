using TMPro;
using UI.Screens;
using UnityEngine;

namespace BattleSystem.Cards
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardTitleText;
        [SerializeField] private TMP_Text _cardDescriptionText;
        [SerializeField] private TMP_Text _cardStat;
        [SerializeField] private TMP_Text _cardEnergy;
        [SerializeField] private CardFly _cardFly;


        private Animator _animator;
        private Card _card;

        public Card Card => _card;
        public CardFly DiscardEffect => _cardFly;
        public string CardTitle => _cardTitleText.text;
        public string CardStat => _cardStat.text;
        public string CardEnergy => _cardEnergy.text;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void OnEnable()
        {
            _animator.Play("HoverOffCard");
        }

        public void LoadCard(Card card)
        {
            _card = card;
            gameObject.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            _cardTitleText.text = card.CardTitle;
            /*cardDescriptionText.text = _card.GetCardDescriptionAmount();*/
            _cardEnergy.text = card.GetCardEnergyAmount().ToString();
            _cardStat.text = card.GetCardStatAmount().ToString();
        }

        public void SelectCard()
        {
            Debug.Log("card is selected");
            BattleSystem.BattleManager.Instance.SelectedCard = this;
        }

        public void DeselectCard()
        {
            Debug.Log("card is deselected");
            BattleSystem.BattleManager.Instance.SelectedCard = null;
            _animator.Play("HoverOffCard");
        }

        public void HoverCard()
        {
            if (BattleSystem.BattleManager.Instance.SelectedCard == null)
                _animator.Play("HoverOnCard");
        }

        public void DropCard()
        {
            if (BattleSystem.BattleManager.Instance.SelectedCard == null)
                _animator.Play("HoverOffCard");
        }

        public void HandleDrag()
        {
        }

        public void HandleEndDrag()
        {
            if (BattleManager.Instance.Energy < _card.GetCardEnergyAmount())
                return;

            if (_card.CardType == CardType.Attack)
            {
                BattleManager.Instance.PlayCard(this);
                Instantiate(DiscardEffect, BattleScreen.Instance.TopParent);
                _animator.Play("HoverOffCard");
            }
            else
            {
                _animator.Play("HoverOffCard");
                Instantiate(DiscardEffect, BattleScreen.Instance.TopParent);
                BattleManager.Instance.PlayCard(this);
            }
        }
    }
}