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

    public void UpdateFuelValue(float value)
    {
        FuelSlider.value = value;
    }

    private bool IsFuelIndicatorVisible()
    {
        var fuel = gameController.Fuel;

        if (!fuel)
        {
            return false;
        }
        
        var distanceToFuel = (playerController.transform.position - fuel.transform.position).magnitude;

        return distanceToFuel > FuelIndicatorRenderDistanceThreshold;
    }
}
