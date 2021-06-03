using Devkit.Base.Component;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooterProject.Component 
{
    public class GamePlayComponent : MonoBehaviour, IComponent
    {
        [SerializeField]
        private Player player;
        private InGameInputSystem inputSystem;

        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>GamePlayComponent initialized!</color>");
            inputSystem = componentContainer.GetComponent("InGameInputSystem") as InGameInputSystem;
            player.InjectInpuSystem(inputSystem);

        }
    }
}

