using System;
using System.Collections.Generic;
using Settings;
using UI.Popups;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace
{
    public static class DailyRewardSystem
    {
        private static DateTime _nextReward;

        public static void Setup()
        {
            /*var userData = SaveDataManager.LoadUserData();
            _nextReward = userData.NextRewardDateTime;
            if (_nextReward == DateTime.MinValue)
            {
                _nextReward = DateTime.UtcNow;
                userData.NextRewardDateTime = _nextReward;
                SaveDataManager.SaveUserData(userData);
            }*/
        }

        public static bool CheckRewardAccess()
        {
            return DateTime.UtcNow >= _nextReward;
        }

        public static void ShowRewardPopup()
        {
            /*List<RewardContainer.RewardContainerSetting> rewardContainerSettings =
                new List<RewardContainer.RewardContainerSetting>();

            var rewardsCount = SaveDataManager.LoadUserData().ReceivedAwards;

            var dailyHearts = SettingsProvider.Get<ServerDataSettingsFromServer>().DailyRewards;

            int index = 0;
            foreach (var dailyHeart in dailyHearts)
            {
                rewardContainerSettings.Add(new RewardContainer.RewardContainerSetting
                {
                    FreeReward = dailyHeart.Free,
                    PremiumReward = dailyHeart.Premium,
                    CurrentReward = ((rewardsCount - index) % dailyHearts.Count) % dailyHearts.Count == 0,
                    Unlocked = CheckRewardAccess(),
                    ClaimAction = () =>
                    {
                        GetReward();
                        PopupSystem.CloseAllPopups();
                    }
                });
                index++;
            }

            PopupSystem.ShowPopup<DailyRewardPopup>(new DailyRewardPopupSettings()
            {
                RewardContainerSettings = rewardContainerSettings
            });*/
        }

        public static void GetReward()
        {
            /*var rewardSettings = SettingsProvider.Get<ServerDataSettingsFromServer>();
            var reward =
                rewardSettings.DailyRewards[
                    SaveDataManager.LoadUserData().ReceivedAwards % rewardSettings.DailyRewards.Count];

            if (CheckRewardAccess())
            {
                var userData = SaveDataManager.LoadUserData();
                _nextReward = TimeToNextReward();
                userData.NextRewardDateTime = _nextReward;
                userData.ReceivedAwards++;
                if (PurchaseManager.Instance.IsSubscriptionActive())
                {
                    userData.AddHearts(reward.Premium);
                }
                userData.AddHearts(reward.Free);
            }*/
        }

        public static DateTime TimeToNextReward()
        {
            var utcNow = DateTime.UtcNow;
            return utcNow.AddHours(23 - utcNow.Hour).AddMinutes(59 - utcNow.Minute).AddSeconds(60 - utcNow.Second);
        }
    }
}