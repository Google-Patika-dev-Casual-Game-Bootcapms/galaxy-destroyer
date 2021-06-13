using Devkit.Base.Component;
using System.Collections;
using System.Collections.Generic;
using Devkit.Base.Object;
using Devkit.HSM;
using UnityEngine;
using System;

namespace SpaceShooterProject.Component 
{
    public class GamePlayComponent : MonoBehaviour, IComponent, IUpdatable
    {
        [SerializeField] private Player player;
        private InGameInputSystem inputSystem;
        private InGameWeaponUpgradeComponent weaponUpgradeComponent;

        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>GamePlayComponent initialized!</color>");
            inputSystem = componentContainer.GetComponent("InGameInputSystem") as InGameInputSystem;

            InitializeWeaponUpgradeComponent(componentContainer);

            player.InjectInpuSystem(inputSystem);
        }

        private void InitializeWeaponUpgradeComponent(ComponentContainer componentContainer)
        {
            weaponUpgradeComponent = new InGameWeaponUpgradeComponent();
            weaponUpgradeComponent.Initialize(componentContainer);
        }

        public void CallUpdate()
        {
            Debug.Log("GamePlayComponent is on");
            inputSystem.CallUpdate();
            player.CallUpdate();
        }

        public void OnEnter()
        {
            //LOAD Level!
        }

        public void OnExit()
        {
            
        }
    }
}
