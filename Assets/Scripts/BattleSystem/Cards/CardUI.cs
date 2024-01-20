using System.Linq;
using Settings;
using TMPro;
using UI.Screens;
using UnityEngine;
using UnityEngine.UI;
using SettingsProvider = Settings.SettingsProvider;

namespace BattleSystem.Cards
{
    public class CardUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text _cardTitleText;
        [SerializeField] private GameObject _cardDescription;
        [SerializeField] private TMP_Text _cardDefense;
        [SerializeField] private TMP_Text _cardAttack;
        [SerializeField] private TMP_Text _cardSkill;
        [SerializeField] private TMP_Text _cardEnergy;
        [SerializeField] private CardFly _cardFly;
        [SerializeField] private Image _cardSprite;


        private Animator _animator;
        private Card _card;

        public Card Card => _card;
        public CardFly DiscardEffect => _cardFly;
        public string CardTitle => _cardTitleText.text;
        public string CardStat => _cardDefense.text;
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
            
            _cardSprite.sprite = SettingsProvider.Get<BattlePrefabSet>().DeckLibrary.Deck
                .FirstOrDefault(x => x.CardTitle == card.CardTitle)?.CardSprite;
            
            _cardTitleText.text = card.CardTitle;
            _cardDescription.GetComponentInChildren<TMP_Text>().text = _card.GetCardDescriptionAmount();
            _cardDescription.SetActive(false);
            _cardEnergy.text = card.GetCardEnergyAmount().ToString();
            if (card is AttackCard attackCard)
            {
                if (attackCard.AttackType != AttackCardType.Bodyslam)
                {
                    _cardAttack.text = attackCard.GetCardStatAmount().ToString();
                }
                else
                {
                    _cardAttack.text = "-/-";
                }

                _cardAttack.gameObject.SetActive(true);
                _cardDefense.gameObject.SetActive(false);
                _cardSkill.gameObject.SetActive(false);
            }

            if (card is DefenseCard defenseCard)
            {
                if (defenseCard.DefenseType != DefenceCardType.Entrench)
                {
                    _cardDefense.text = defenseCard.GetCardStatAmount().ToString();
                }
                else
                {
                    _cardDefense.text = "-/-";
                }

                _cardDefense.gameObject.SetActive(true);
                _cardAttack.gameObject.SetActive(false);
                _cardSkill.gameObject.SetActive(false);
            }

            if (card is SkillCard skillCard)
            {
                if (skillCard.SkillType != SkillCardType.Inflame)
                {
                    _cardSkill.text = skillCard.GetCardStatAmount().ToString();
                }
                else
                {
                    _cardSkill.text = "-/-";
                }

                _cardSkill.gameObject.SetActive(true);
                _cardAttack.gameObject.SetActive(false);
                _cardDefense.gameObject.SetActive(false);
            }
        }

        public void SelectCard()
        {
            Debug.Log("card is selected");
            BattleManager.Instance.SelectedCard = this;
        }

        public void DeselectCard()
        {
            Debug.Log("card is deselected");
            _cardDescription.SetActive(false);
            BattleManager.Instance.SelectedCard = null;
            _animator.Play("HoverOffCard");
        }

        public void HoverCard()
        {
            if (BattleManager.Instance.SelectedCard == null)
            {
                _cardDescription.SetActive(true);
                _animator.Play("HoverOnCard");
            }
        }

        public void DropCard()
        {
            if (BattleManager.Instance.SelectedCard == null)
            {
                _cardDescription.SetActive(false);
                _animator.Play("HoverOffCard");
            }
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
                Instantiate(DiscardEffect, BattleScreen.Instance.DiscardParent);
                _animator.Play("HoverOffCard");
            }
            else
            {
                _animator.Play("HoverOffCard");
                Instantiate(DiscardEffect, BattleScreen.Instance.DiscardParent);
                BattleManager.Instance.PlayCard(this);
            }
        }
    }
}