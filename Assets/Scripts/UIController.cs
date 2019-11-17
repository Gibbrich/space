using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public GameObject Menu;
    public GameObject GameOverText;
    public Text ScoreTitle;

    private GameController gameController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        UpdateScore(0);
    }

    public void ShowMenu(bool shouldShowEndGameLabel = false)
    {
        GameOverText.SetActive(shouldShowEndGameLabel);
        Menu.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        Menu.gameObject.SetActive(false);
    }

    public void OnRestartButtonClick()
    {
        gameController.StartGame(true);
    }

    public void UpdateScore(int score)
    {
        ScoreTitle.text = $"Your Score: {score}";
    }
}
