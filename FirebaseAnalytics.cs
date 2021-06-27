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
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task=> {
            var app = FirebaseApp.DefaultInstance;
        });
    }

    
}
