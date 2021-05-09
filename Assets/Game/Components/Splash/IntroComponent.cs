namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class IntroComponent : MonoBehaviour, IComponent
    {
        public void Initialize(ComponentContainer componentContainer)
        {
            
        }

        public bool IsCompleted()
        {
            return true;
        }
    }
}


