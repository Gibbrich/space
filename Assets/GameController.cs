using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public UIController uiController;
    private int Score = 0;

    [HideInInspector]
    public Fuel Fuel;

    [HideInInspector] public GameState GameState = GameState.Play;

    private void Start()
    {
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameState != GameState.Stop)
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (GameState == GameState.Play)
        {
            GameState = GameState.Pause;
            Time.timeScale = 0;
            uiController.ShowMenu();
        }
        else
        {
            GameState = GameState.Play;
            Time.timeScale = 1;
            uiController.HideMenu();
        }
    }

    public void EndGame()
    {
        GameState = GameState.Stop;
        uiController.ShowMenu(true);
    }

    public void StartGame(bool shouldRestart = false)
    {
        if (shouldRestart)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            GameState = GameState.Play;
            uiController.HideMenu();
        }
    }

    public void UpdateScore(int score)
    {
        Score += score;
        uiController.UpdateScore(Score);
    }

    public void UpdateFuel(float value)
    {
        uiController.UpdateFuelValue(value);
        if (Math.Abs(value) < 0.001)
        {
            EndGame();
        }
    }
}