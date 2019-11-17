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
    public float MaxDifficultyTime = 45f;
    public int Score = 0;
    public int FuelCollectCount = 0;
    private float lastLowEnergyWarnTime;
    private AudioSource cameraAudioSource;

    [HideInInspector]
    public Fuel Fuel;

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

    public void UpdateFuelCollectCount()
    {
        FuelCollectCount++;
    }

    public void UpdateFuel(float value)
    {
        if (value <= 0.2f && Time.time - lastLowEnergyWarnTime >= SoundsConfigure.LowEnergyWarnCoolDown)
        {
            PlayAudio(SoundsConfigure.LowEnergy);
            lastLowEnergyWarnTime = Time.time;
        }
        
        uiController.UpdateFuelValue(value);
        if (Math.Abs(value) < 0.001)
        {
            EndGame();
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        cameraAudioSource.PlayOneShot(clip);
    }
}