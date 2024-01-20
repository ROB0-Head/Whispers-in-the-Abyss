using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace BattleSystem.Characters
{
    public class FighterHealthBar : MonoBehaviour
    {
        [SerializeField] private GameObject _block;
        [SerializeField] private TMP_Text _healthText;

        public void DisplayBlock(int blockAmount)
        {
            if (blockAmount > 0)
            {
                _block.SetActive(true);
                _block.GetComponentInChildren<TMP_Text>().text = blockAmount.ToString();
            }
            else
            {
                _block.SetActive(false);
            }
        }

        public void DisplayHealth(int healthAmount)
        {
            _healthText.text = healthAmount.ToString();
        }
    }
}