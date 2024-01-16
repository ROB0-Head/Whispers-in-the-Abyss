using BattleSystem.Cards;
using Settings.BattleManager.Cards;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace TJ
{
    public class RelicRewardUI : MonoBehaviour
    {
        public Image relicImage;
        public TMP_Text relicName;
        public TMP_Text relicDescription;

        public void DisplayRelic(Relic r)
        {
            relicImage.sprite = r.relicIcon;
            relicName.text = r.RelicType.ToString();
            relicDescription.text = r.relicDescription;
        }

        public void DisplayCard(Card r)
        {
            /*relicImage.sprite = r.;
            relicName.text = r.cardTitle;
            relicDescription.text = r.GetCardDescriptionAmount();*/
        }
    }
}