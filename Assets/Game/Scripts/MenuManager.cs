using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    enum Screen
    {
        None,
        Main,
        Setting
    }

    public CanvasGroup mainScreen;
    public CanvasGroup settingsScreen;

    private void Start()
    {
        SetCurrentScreen(Screen.Main);
    }

    private void SetCurrentScreen(Screen screen)
    {
        Utility.SetCanvasGroupEnabled(mainScreen, screen == Screen.Main);
        Utility.SetCanvasGroupEnabled(settingsScreen, screen == Screen.Setting);
    }

    public void StartNewGame()
    {
        SetCurrentScreen(Screen.None);
        //SceneManager.LoadScene("Game");
        LoadingScreen.instance.LoadScene("Game");
    }

    public void OpenSettings()
    {
        SetCurrentScreen(Screen.Setting);
    }

    public void CloseSettings()
    {
        SetCurrentScreen(Screen.Main);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
