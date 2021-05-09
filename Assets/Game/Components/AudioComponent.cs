namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    public class AudioComponent : MonoBehaviour, IComponent
    {
        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("Audio Component initialized!");
        }
    }
}


