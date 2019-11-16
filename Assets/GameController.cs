using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public UIController uiController;

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
            uiController.Show();
        }
        else
        {
            GameState = GameState.Play;
            Time.timeScale = 1;
            uiController.Hide();
        }
    }

    public void EndGame()
    {
        GameState = GameState.Stop;
        uiController.Show(true);
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
            uiController.Hide();
        }
    }
}