using BattleSystem.Cards;
using UnityEngine;

namespace BattleSystem.Characters.BattleScreen
{
    public class CardTarget : MonoBehaviour
    {
        private Fighter _enemyFighter;

        private void Awake()
        {
            _enemyFighter = GetComponent<Fighter>();
        }

        public void PointerEnter()
        {
            if (_enemyFighter == null)
            {
                Debug.Log("fighta is null");
                _enemyFighter = GetComponent<Fighter>();
            }

            if (BattleManager.Instance.SelectedCard != null &&
                BattleManager.Instance.SelectedCard.Card.CardType == CardType.Attack)
            {
                BattleManager.Instance.SetTarget(_enemyFighter);
            }
        }

        public void PointerExit()
        {
            BattleManager.Instance.SetTarget(null);
        }
    }
}