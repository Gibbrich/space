using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject ScorePanel;
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

    public void ShowMenu()
    {
        ScorePanel.gameObject.SetActive(false);
        ScoreMenuTitle.text = gameController.Score.ToString();
        Menu.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        ScorePanel.gameObject.SetActive(true);
        Menu.gameObject.SetActive(false);
    }

    public void OnRestartButtonClick()
    {
        if (gameController.SoundsConfigure.MenuButtonClick)
        {
            AudioSource.PlayClipAtPoint(gameController.SoundsConfigure.MenuButtonClick, transform.position);
        }

        gameController.StartGame(true);
    }

    public void OnExitButtonClick()
    {
        Application.Quit();
    }

    public void UpdateScore(int score)
    {
        ScoreTitle.text = $"Your Score: {score}";
    }
}