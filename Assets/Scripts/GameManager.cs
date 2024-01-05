using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using BattleSystem;
using DefaultNamespace;
using SaveSystem;
using Settings;
using UI.Popups;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool NeedShowGiftTutor;
    public static bool NeedShowDateTutor;
    private static DateTime _startDateTime;

    private static bool _timeAfterStart5Send;
    private static bool _timeAfterStart10Send;
    private static bool _timeAfterStart15Send;
    private static bool _timeAfterStart20Send;
    private static bool _timeAfterStart25Send;
    private static bool _timeAfterStart30Send;
    private static bool _timeAfterStart40Send;
    private static bool _timeAfterStart50Send;
    private static bool _timeAfterStart60Send;

    public static int SubscribePopupCloseCounter;
    public static bool IsFirstSessionForAnalytics { get; private set; }

    public static bool IsFirstLaunch
    {
        get { return PlayerPrefs.GetInt("NotFirstLaunch", 0) == 0; }
        set { PlayerPrefs.SetInt("NotFirstLaunch", value ? 0 : 1); }
    }

    public static bool SendFirstMessage;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        DailyRewardSystem.Setup();
        _startDateTime = DateTime.Now;


        StartCoroutine(PlayTimer());
    }

    private IEnumerator PlayTimer()
    {
        var timeAfterStart = _startDateTime.Subtract(DateTime.Now).TotalMinutes;
        /*if (timeAfterStart > 5 && !_timeAfterStart5Send)
        {
            AnalyticsManager.Instance.SendPlaytimeEvent(5);
            _timeAfterStart5Send = true;
        }
        else if (timeAfterStart > 10 && !_timeAfterStart10Send)
        {
            AnalyticsManager.Instance.SendPlaytimeEvent(10);
            _timeAfterStart10Send = true;
        }
        else if (timeAfterStart > 15 && !_timeAfterStart15Send)
        {
            AnalyticsManager.Instance.SendPlaytimeEvent(15);
            _timeAfterStart15Send = true;
        }
        else if (timeAfterStart > 20 && !_timeAfterStart20Send)
        {
            AnalyticsManager.Instance.SendPlaytimeEvent(20);
            _timeAfterStart20Send = true;
        }
        else if (timeAfterStart > 25 && !_timeAfterStart25Send)
        {
            AnalyticsManager.Instance.SendPlaytimeEvent(25);
            _timeAfterStart25Send = true;
        }
        else if (timeAfterStart > 30 && !_timeAfterStart30Send)
        {
            AnalyticsManager.Instance.SendPlaytimeEvent(30);
            _timeAfterStart30Send = true;
        }
        else if (timeAfterStart > 40 && !_timeAfterStart40Send)
        {
            AnalyticsManager.Instance.SendPlaytimeEvent(40);
            _timeAfterStart40Send = true;
        }
        else if (timeAfterStart > 50 && !_timeAfterStart50Send)
        {
            AnalyticsManager.Instance.SendPlaytimeEvent(50);
            _timeAfterStart50Send = true;
        }
        else if (timeAfterStart > 60 && !_timeAfterStart60Send)
        {
            AnalyticsManager.Instance.SendPlaytimeEvent(60);
            _timeAfterStart60Send = true;
        }*/

        yield return new WaitForSecondsRealtime(5f);
    }

    private void Start()
    {
        if (IsFirstLaunch)
        {
            var userData = SaveManager.LoadUserData();
            userData.Cards = new List<Card>();

            for (int i = 0; i < 7; i++)
            {
                var cards = SettingsProvider.Get<Deck>();
                userData.Cards.Add(cards.CardDeck[0]);
            }

            SaveManager.SaveUserData(userData);
            IsFirstSessionForAnalytics = true;
            IsFirstLaunch = false;
            DailyRewardSystem.ShowRewardPopup();
            NeedShowGiftTutor = true;
            NeedShowDateTutor = true;
            /*var userData = SaveDataManager.LoadUserData();
            userData.TotalHearts = SettingsProvider.Get<ServerDataSettingsFromServer>().StartingInternalCurrency;
            userData.FirstLaunchDateTime = DateTime.UtcNow;
            SaveDataManager.SaveUserData(userData);*/
        }
        else
        {
            if (DailyRewardSystem.CheckRewardAccess())
            {
                DailyRewardSystem.ShowRewardPopup();
            }

            /*var blurType = SettingsProvider.Get<ServerDataSettingsFromServer>().BlurType;
            if (blurType == BlurType.Limit)
            {
                foreach (var chatInfo in TestSystem.Instance.TestChats)
                {
                    var userData = SaveDataManager.LoadUserData();
                    var characterInfo = userData.GetActionLimits(chatInfo.ChatId);

                    characterInfo.SendedPhotos = 0;
                    SaveDataManager.SaveUserData(userData);
                }
            }*/
        }
    }
}