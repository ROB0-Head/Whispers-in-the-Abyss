using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardContainer : MonoBehaviour
{
    public GameObject SelectedBackground;
    
    //Левый блок
    public GameObject Clock;
    public TMP_Text DayText;
    
    //Free блок
    public TMP_Text FreeHeartsText;
    public Button FreeHeartsButton;
    public GameObject FreeLockedText;
    //Premium блок
    public TMP_Text PremiumHeartsText;
    public Button PremiumHeartsButton;
    public GameObject PremiumHeartsLock; 
    public GameObject PremiumLockedText;

    public void Setup(RewardContainerSetting rewardContainerSetting)
    {
        SelectedBackground.SetActive(rewardContainerSetting.CurrentReward);
        
        Clock.SetActive(!rewardContainerSetting.CurrentReward);
        DayText.gameObject.SetActive(rewardContainerSetting.CurrentReward);

        FreeHeartsText.text = rewardContainerSetting.FreeReward.ToString();
        PremiumHeartsText.text = rewardContainerSetting.PremiumReward.ToString();

        FreeHeartsButton.onClick.AddListener(rewardContainerSetting.ClaimAction.Invoke);
        FreeHeartsButton.gameObject.SetActive(rewardContainerSetting.Unlocked && rewardContainerSetting.CurrentReward);
        /*if (rewardContainerSetting.Unlocked && PurchaseManager.Instance.IsSubscriptionActive())
        {
            PremiumHeartsButton.gameObject.SetActive(rewardContainerSetting.CurrentReward);
            FreeLockedText.gameObject.SetActive(!rewardContainerSetting.CurrentReward);
        }
        else
        {
            PremiumHeartsButton.gameObject.SetActive(false);
            FreeLockedText.gameObject.SetActive(true); 
        }
        
        PremiumHeartsButton.onClick.AddListener(rewardContainerSetting.ClaimAction.Invoke);
        if (!rewardContainerSetting.Unlocked || !PurchaseManager.Instance.IsSubscriptionActive())
        {
            PremiumHeartsLock.gameObject.SetActive(true);
            PremiumLockedText.gameObject.SetActive(false);
        }
        else
        {
            PremiumHeartsLock.gameObject.SetActive(false);
            PremiumLockedText.gameObject.SetActive(true);
        }*/
    }
    
    public class RewardContainerSetting
    {
        public int FreeReward;
        public int PremiumReward;
        public bool CurrentReward;
        public bool Unlocked;
        public Action ClaimAction;
    }
}
