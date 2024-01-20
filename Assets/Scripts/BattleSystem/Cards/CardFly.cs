using UI.Screens;
using UnityEngine;

namespace BattleSystem.Cards
{
    public class CardFly : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<Animator>().Play("Disappear");
        }

        public void Update()
        {
            transform.position =
                Vector3.Lerp(transform.position, BattleScreen.Instance.DiscardPileCount.position, Time.deltaTime * 10);
            if (Vector3.Distance(transform.position, BattleScreen.Instance.DiscardPileCount.position) < 10f)
                Destroy(gameObject);
        }
    }
}