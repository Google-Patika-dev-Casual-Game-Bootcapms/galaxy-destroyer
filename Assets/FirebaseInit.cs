using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
        void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task => {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available) {
    
              var app = Firebase.FirebaseApp.DefaultInstance;

    
             } else {
                UnityEngine.Debug.LogError(System.String.Format(
                "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
    
            }
        });
    }

    
}
