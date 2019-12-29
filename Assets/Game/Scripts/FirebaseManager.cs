using System;
using Firebase.Analytics;
using UnityEngine;
using Firebase;
using Firebase.Crashlytics;

public class FirebaseManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        
        // Initialize Firebase
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                // Create and hold a reference to your FirebaseApp,
                // where app is a Firebase.FirebaseApp property of your application class.
                // Crashlytics will use the DefaultInstance, as well;
                // this ensures that Crashlytics is initialized.
                FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
                Crashlytics.IsCrashlyticsCollectionEnabled = true;
        
                // this ensures that Crashlytics and Analytics is initialized.
                var firebaseApp = FirebaseApp.DefaultInstance;

                // Set a flag here for indicating that your project is ready to use Firebase.
                FirebaseAnalytics.LogEvent(Actions.APP_OPEN);

                // Set a flag here for indicating that your project is ready to use Firebase.
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                // Firebase Unity SDK is not safe to use here.
            }
        });
    }

    private void OnDestroy()
    {
        FirebaseAnalytics.LogEvent(Actions.APP_CLOSE);
    }
}