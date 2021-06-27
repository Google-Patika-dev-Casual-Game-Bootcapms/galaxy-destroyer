namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.Base.Object;
    using Devkit.HSM;
    using UnityEngine;
    using System;

    public class GamePlayComponent : MonoBehaviour, IComponent, IUpdatable
    {
        /*[SerializeField] 
        private Player player;
        [SerializeField]
        private GameObject playerPrefab;
        private InGameInputSystem inputSystem;
        private InGameWeaponUpgradeComponent weaponUpgradeComponent;*/

        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>GamePlayComponent initialized!</color>");
            //inputSystem = componentContainer.GetComponent("InGameInputSystem") as InGameInputSystem;

            InitializeWeaponUpgradeComponent(componentContainer);

            /*if (player == null) 
            {
                //TODO create player from prefab!!!
                CreatePlayer();
            }*/

            //player.InjectInpuSystem(inputSystem);
        }

        private void CreatePlayer()
        {
            //player = Instantiate(playerPrefab).GetComponent<Player>();
        }

        private void InitializeWeaponUpgradeComponent(ComponentContainer componentContainer)
        {
            //weaponUpgradeComponent = new InGameWeaponUpgradeComponent();
            //weaponUpgradeComponent.Initialize(componentContainer);
        }

        public void CallUpdate()
        {
            Debug.Log("GamePlayComponent is on");
            //inputSystem.CallUpdate();
            //player.CallUpdate();
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

