using System;
using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static readonly string CONTINUE_TITLE = "Continue";
    public static readonly string RESTART_TITLE = "Restart";
    
    public GameObject Menu;
    public GameObject ScorePanel;
    public GameObject MenuButton;
    public Text MenuRightButtonTitle;
    public Text ScoreTitle;
    public Text ScoreMenuTitle;
    public float FuelIndicatorRenderDistanceThreshold = 10;

    private GameController gameController;
    private PlayerController playerController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        playerController = FindObjectOfType<PlayerController>();
        UpdateScore(0);
    }

    public void ShowMenu(bool isEndGame = false)
    {
        MenuRightButtonTitle.text = isEndGame ? RESTART_TITLE : CONTINUE_TITLE;
        MenuButton.gameObject.SetActive(false);
        ScorePanel.gameObject.SetActive(false);
        ScoreMenuTitle.text = gameController.Score.ToString();
        Menu.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        MenuButton.gameObject.SetActive(true);
        ScorePanel.gameObject.SetActive(true);
        Menu.gameObject.SetActive(false);
    }

    public void OnMenuRightButtonClick()
    {
        if (gameController.GameState == GameState.Stop)
        {
            FirebaseAnalytics.LogEvent(Actions.RESTART_GAME_BUTTON_CLICK);
            RestartGame();
        }
        else
        {
            FirebaseAnalytics.LogEvent(Actions.CONTINUE_GAME_BUTTON_CLICK);
            ContinueGame();
        }
    }

    private void RestartGame()
    {
        if (gameController.SoundsConfigure.MenuButtonClick)
        {
            AudioSource.PlayClipAtPoint(gameController.SoundsConfigure.MenuButtonClick, transform.position);
        }

        gameController.StartGame(true);
    }

    public void OnRestartButtonClick() => RestartGame();

    public void OnExitButtonClick()
    {
        FirebaseAnalytics.LogEvent(Actions.EXIT_GAME_BUTTON_CLICK);
        Application.Quit();
    }

    public void OnMenuButtonClick()
    {
        FirebaseAnalytics.LogEvent(Actions.SETTINGS_BUTTON_CLICK);
        gameController.TogglePause();
    }

    public void OnContinueButtonClick() => ContinueGame();

    private void ContinueGame() => gameController.TogglePause();

    public void UpdateScore(int score)
    {
        ScoreTitle.text = $"Your Score: {score}";
    }
}