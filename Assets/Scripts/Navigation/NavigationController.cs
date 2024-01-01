using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Settings;
using UI.Screens;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Sequence = DG.Tweening.Sequence;
using SettingsProvider = Settings.SettingsProvider;

public class NavigationController : MonoBehaviour
{
    public GameObject Canvas;

    private static NavigationController _instance;

    public static NavigationController Instance => _instance;

    private DefaultScreen _currentScreen;

    public DefaultScreen CurrentScreen => _currentScreen;
    
    public void Awake()
    {
        _instance = this;
    }

    void Start()
    {
        ScreenTransition<MainScreen>(new MainScreenSettings()
        {
            TabType = MainScreen.TabType.Chats
        });
    }

    public void UpdateScreen()
    {
        _currentScreen.UpdateScreen();
    }

    public void ScreenTransition<T>(ScreenSettings settings = null) where T : DefaultScreen
    {
        if (_currentScreen != null)
        {
            var startPos = _currentScreen is MainScreen ? 1000 : -1000;
            var endPos = _currentScreen is MainScreen ? -1000 : 1000;
            
            Sequence mySequence = DOTween.Sequence();
            var screen = _currentScreen;

            _currentScreen = Instantiate(SettingsProvider.Get<PrefabSet>().GetScreen<T>(), Canvas.transform);
            _currentScreen.gameObject.transform.localPosition = new Vector3(startPos, 0, 0);
            screen.Deactivate();
            _currentScreen.Setup(settings);
            mySequence.AppendCallback(() => _currentScreen.gameObject.transform.DOLocalMoveX(0, 0.2f));
            mySequence.Append(screen.gameObject.transform.DOLocalMoveX(endPos, 0.2f));
            mySequence.AppendCallback(() => Destroy(screen.gameObject));
            mySequence.Play();
        }
        else
        {
            _currentScreen = Instantiate(SettingsProvider.Get<PrefabSet>().GetScreen<T>(), Canvas.transform);
            _currentScreen.Setup(settings);
        }
    }

    public enum ScreenType
    {
        MainScreen,
        ChatScreen,
        ProfileScreen
    }
}
