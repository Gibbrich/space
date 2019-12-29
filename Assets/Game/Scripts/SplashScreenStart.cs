using System.Collections;
using System.Collections.Generic;
using Firebase.Analytics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenStart : MonoBehaviour
{
    public void LoadGame()
    {
        FirebaseAnalytics.LogEvent(Actions.PLAY_GAME_BUTTON_CLICK);
        SceneManager.LoadScene(1);
    }
}
