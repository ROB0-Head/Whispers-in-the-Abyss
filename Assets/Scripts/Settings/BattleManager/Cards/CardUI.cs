using TMPro;
using UnityEngine;

namespace Settings.BattleManager.Cards
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardTitleText;
        [SerializeField] private TMP_Text _cardDescriptionText;
        [SerializeField] private TMP_Text _cardStat;
        [SerializeField] private TMP_Text _cardEnergy;
        [SerializeField] private GameObject _discardEffect;


        private Animator _animator;
        private Card _card;

        public Card Card => _card;
        public GameObject DiscardEffect => _discardEffect;
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
            if (BattleSystem.BattleManager.Instance.Energy < _card.GetCardEnergyAmount())
                return;

            if (_card.CardType == CardType.Attack)
            {
                BattleSystem.BattleManager.Instance.PlayCard(this);
                _animator.Play("HoverOffCard");
            }
            else if (_card.CardType != CardType.Attack)
            {
                _animator.Play("HoverOffCard");
                BattleSystem.BattleManager.Instance.PlayCard(this);
            }
        }
    }
}