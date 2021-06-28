namespace SpaceShooterProject.Component
{
    using Devkit.Base.Component;
    using Devkit.Base.Pattern.ObjectPool;
    using Devkit.Base.Object;
    using UnityEngine;
    using System.Collections.Generic;
    using System;

    public class GamePlayComponent : MonoBehaviour, IComponent, IUpdatable
    {
        [SerializeField] private Player player;
        [SerializeField] private GameCamera gameCamera;
        private InGameInputSystem inputSystem;
        private InGameWeaponUpgradeComponent weaponUpgradeComponent;
        private BulletCollector bulletCollector;
        private EnemyFactory enemyFactory;


        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>GamePlayComponent initialized!</color>");
            inputSystem = componentContainer.GetComponent("InGameInputSystem") as InGameInputSystem;

            InitializeWeaponUpgradeComponent(componentContainer);

            player.InjectInputSystem(inputSystem);
            player.ComponentContainer = componentContainer;
            player.Init();
            bulletCollector = new BulletCollector();

            enemyFactory = new EnemyFactory();
            enemyFactory.Init();
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
            player.Shoot(bulletCollector);
            bulletCollector.UpdateBullets();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                enemyFactory.SpawnEnemies();
            }
        }

        private void LateUpdate()
        {
            if (gameCamera.IsAvailable)
                gameCamera.CallLateUpdate();
        }

        public void OnEnter()
        {
            //LOAD Level!
        }

        public void OnExit()
        {
        }

        public Player Player => player;

        public GameCamera GameCamera => gameCamera;
    }

    public interface IBulletCollector
    {
        void AddBulletToPool(Bullet bullet);
    }
}