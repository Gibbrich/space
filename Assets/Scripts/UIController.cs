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
    public Text FuelMenuTitle;
    public Slider FuelSlider;
    public FuelIndicator FuelIndicator;
    public float FuelIndicatorRenderDistanceThreshold = 10;

    private GameController gameController;
    private PlayerController playerController;

    private void Start()
    {
        gameController = FindObjectOfType<GameController>();
        playerController = FindObjectOfType<PlayerController>();
        UpdateScore(0);
    }

    private void Update()
    {
        FuelIndicator.gameObject.SetActive(IsFuelIndicatorVisible());
    }

    public void ShowMenu()
    {
        FuelSlider.gameObject.SetActive(false);
        ScorePanel.gameObject.SetActive(false);
        ScoreMenuTitle.text = gameController.Score.ToString();
        FuelMenuTitle.text = gameController.FuelCollectCount.ToString();
        Menu.gameObject.SetActive(true);
    }

    public void HideMenu()
    {
        FuelSlider.gameObject.SetActive(true);
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

    public void UpdateFuelValue(float value)
    {
        FuelSlider.value = value;
    }

    private bool IsFuelIndicatorVisible()
    {
        if (gameController.GameState != GameState.Play)
        {
            return false;
        }
        
        var fuel = gameController.Fuel;

        if (!fuel)
        {
            return false;
        }

        var distanceToFuel = (playerController.transform.position - fuel.transform.position).magnitude;

        return distanceToFuel > FuelIndicatorRenderDistanceThreshold;
    }
}