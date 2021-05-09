using Devkit.Base.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterProject.Component 
{
    public class GamePlayComponent : MonoBehaviour, IComponent
    {
        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>GamePlayComponent initialized!</color>");
        }
    }
}

