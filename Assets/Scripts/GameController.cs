using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameController : MonoBehaviour
{
    public UIController uiController;
    public SoundsConfigure SoundsConfigure;
    public float MaxDifficultyTime = 35f;
    public int Score = 0;
    private float lastLowEnergyWarnTime;
    private AudioSource cameraAudioSource;

    [HideInInspector] public GameState GameState = GameState.Play;

    private void Start()
    {
        cameraAudioSource = Camera.main.GetComponent<AudioSource>();
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
        uiController.ShowMenu();
    }

    public void StartGame(bool shouldRestart = false)
    {
        Time.timeScale = 1;
        
        if (shouldRestart)
        {
            SceneManager.LoadScene(1);
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

    public void PlayAudio(AudioClip clip)
    {
        cameraAudioSource.PlayOneShot(clip);
    }
}