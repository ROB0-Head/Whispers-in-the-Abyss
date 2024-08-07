using System;
using System.Collections;
using System.Collections.Generic;
using BattleSystem.Cards;
using BattleSystem.Characters.Reward;
using DefaultNamespace;
using SaveSystem;
using Settings;
using Settings.Battle;
using Settings.Battle.Cards;
using UI;
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
            var deck = new List<Card>();

            int attackCardCount = 0;
            int defenseCardCount = 0;
            int skillCardCount = 0;

            while (attackCardCount < 4 || defenseCardCount < 4 || skillCardCount < 2)
            {
                var randomCard = SettingsProvider.Get<BattlePrefabSet>().DeckLibrary.GetRandomCard();
                bool isUpgraded = UnityEngine.Random.value <= 0.2f;

                switch (randomCard.CardType)
                {
                    case CardType.Defense:
                    case CardType.Attack:
                        if (randomCard is DefenseCardSettings defenseCard && defenseCardCount < 4)
                        {
                            deck.Add(new DefenseCard(defenseCard.CardTitle, isUpgraded, defenseCard.CardDescription,
                                defenseCard.CardCost, defenseCard.CardEffect, defenseCard.BuffAmount,
                                defenseCard.CardType, defenseCard.DefenseType));

                            defenseCardCount++;
                        }

                        if (randomCard is AttackCardSettings attackCard && attackCardCount < 4)
                        {
                            deck.Add(new AttackCard(attackCard.CardTitle, isUpgraded, attackCard.CardDescription,
                                attackCard.CardCost, attackCard.CardEffect, attackCard.BuffAmount,
                                attackCard.CardType, attackCard.AttackType));

                            attackCardCount++;
                        }

                        break;

                    case CardType.Skill:
                        if (randomCard is SkillCardSettings skillCard && skillCardCount < 2)
                        {
                            deck.Add(new SkillCard(skillCard.CardTitle, isUpgraded, skillCard.CardDescription,
                                skillCard.CardCost, skillCard.CardEffect, skillCard.BuffAmount, skillCard.CardType,
                                skillCard.SkillType));

                            skillCardCount++;
                        }

                        break;
                }
            } 
            
            var characterData = SaveManager.LoadCharacterData();
            characterData.startingRelic = SettingsProvider.Get<BattlePrefabSet>().RelicLibrary.GetRandomRelic().RelicType;
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