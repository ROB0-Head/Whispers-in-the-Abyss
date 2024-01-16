using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using BattleSystem;
using BattleSystem.Cards;
using DefaultNamespace;
using SaveSystem;
using Settings;
using Settings.BattleManager.Cards;
using TJ;
using UI.Popups;
using UnityEngine;

public class GameManager : MonoBehaviour
{
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

    public static SceneFader SceneFader { get; private set; }


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
        SceneFader = GetComponent<SceneFader>();
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
            var deck = SaveManager.LoadDeck();
            for (int i = 0; i < 10; i++)
            {
                var randomCard = SettingsProvider.Get<BattlePrefabSet>().DeckLibrary.GetRandomCard();
                bool isUpgraded = UnityEngine.Random.value <= 0.2f;
                switch (randomCard.CardType)
                {
                    case CardType.Defense:
                    case CardType.Attack:
                        if (randomCard is DefenseCardSettings defenceCard)
                        {
                            deck.Add(new DefenseCard(defenceCard.CardTitle, isUpgraded, defenceCard.CardDescription,
                                defenceCard.CardCost, defenceCard.CardEffect, defenceCard.BuffAmount,
                                defenceCard.CardType, defenceCard.DefenseType));
                        }

                        if (randomCard is AttackCardSettings attackCard)
                        {
                            deck.Add(new AttackCard(attackCard.CardTitle, isUpgraded, attackCard.CardDescription,
                                attackCard.CardCost, attackCard.CardEffect, attackCard.BuffAmount,
                                attackCard.CardType,attackCard.AttackType));
                        }

                        break;
                    case CardType.Skill:
                        if (randomCard is SkillCardSettings skillCard)
                        {
                            deck.Add(new SkillCard(skillCard.CardTitle, isUpgraded, skillCard.CardDescription,
                                skillCard.CardCost, skillCard.CardEffect, skillCard.BuffAmount, skillCard.CardType,skillCard.SkillType));
                        }

                        break;
                }
            }

            var characterData = SaveManager.LoadCharacterData();
            characterData.startingRelic = SettingsProvider.Get<BattlePrefabSet>().RelicLibrary.GetRandomRelic();
            IsFirstSessionForAnalytics = true;
            IsFirstLaunch = false;
            DailyRewardSystem.ShowRewardPopup();
            characterData.FirstLaunchDateTime = DateTime.UtcNow;
            SaveManager.SaveCharacterData(characterData);
            SaveManager.SaveDeck(deck);
        }
        else
        {
            if (DailyRewardSystem.CheckRewardAccess())
            {
                DailyRewardSystem.ShowRewardPopup();
            }
        }
    }
}