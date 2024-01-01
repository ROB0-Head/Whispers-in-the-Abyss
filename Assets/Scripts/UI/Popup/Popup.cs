using System;
using UnityEngine;

namespace UI.Popups
{
    public class BasePopup : MonoBehaviour
    {
        private string _popupId = Guid.NewGuid().ToString();
        public string PopupId => _popupId;

        public virtual void Setup(PopupSettings popupSettings)
        {
            
        }
        
        public virtual void Close()
        {
            if(gameObject != null)
                Destroy(gameObject);
            
            /*if (AnalyticsManager.Instance != null)
            {
                if (this is HeartPurchasePopup heartPurchasePopup)
                {
                    AnalyticsManager.Instance.SendPopUpHeartsPurchaseCloseEvent();
                }
                else if (this is SubscribePopupView subscribePopupView)
                {
                    AnalyticsManager.Instance.SendPopUpOfferCloseEvent();
                }
            }*/

            PopupSystem.CloseThisPopup(PopupId);
        }
    }

    [Serializable]
    public class PopupSettings
    {
        
    }
}