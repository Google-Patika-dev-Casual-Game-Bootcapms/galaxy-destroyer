using Devkit.Base.Component;
using System.Collections;
using System.Collections.Generic;
using Devkit.Base.Object;
using Devkit.HSM;
using UnityEngine;


namespace SpaceShooterProject.Component 
{
    public class GamePlayComponent : MonoBehaviour, IComponent, IUpdatable
    {
        [SerializeField] private Player player;
        private InGameInputSystem inputSystem;

        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>GamePlayComponent initialized!</color>");
            inputSystem = componentContainer.GetComponent("InGameInputSystem") as InGameInputSystem;

            player.InjectInpuSystem(inputSystem);
        }

        public void CallUpdate()
        {
            Debug.Log("GamePlayComponent is on");
            inputSystem.CallUpdate();
            player.CallUpdate();
        }
    }
}

