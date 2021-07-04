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
        public delegate void GameOverDelegate();
        public event GameOverDelegate OnGameOver;

        [SerializeField] private Player player;
        [SerializeField] private GameCamera gameCamera;
        private InGameInputSystem inputSystem;
        private InGameWeaponUpgradeComponent weaponUpgradeComponent;
        private BulletCollector bulletCollector;
        private EnemyFactory enemyFactory;
        private InGameMessageBroadcaster inGameMessageBroadcaster;
        


        public void Initialize(ComponentContainer componentContainer)
        {
            inGameMessageBroadcaster = new InGameMessageBroadcaster();
            inGameMessageBroadcaster.Initialize(componentContainer);

            Debug.Log("<color=green>GamePlayComponent initialized!</color>");
            inputSystem = componentContainer.GetComponent("InGameInputSystem") as InGameInputSystem;

            InitializeWeaponUpgradeComponent(componentContainer);

            player.InjectInputSystem(inputSystem);
            player.ComponentContainer = componentContainer;
            player.Init();
            bulletCollector = new BulletCollector();
            player.InjectBulletCollector(bulletCollector);

            enemyFactory = new EnemyFactory(inGameMessageBroadcaster);
            enemyFactory.Init();

            player.OnPlayerDown += FinishGame;
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
            bulletCollector.UpdateBullets();
            enemyFactory.CallUpdate();

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
            inputSystem.Init();
            player.Init();
        }

        public void OnExit()
        {
            player.OnPlayerDown -= FinishGame;
        }

        public void FinishGame() 
        {
            player.OnDestruct();
            inputSystem.OnDestruct();
            bulletCollector.OnDestruct();
            enemyFactory.OnDestruct();
            inGameMessageBroadcaster.RemoveAllEvents();//TODO: research!!!

            if (OnGameOver != null) 
            {
                OnGameOver();
            }
        }

        public Player Player => player;

        public GameCamera GameCamera => gameCamera;
    }

    public interface IBulletCollector
    {
        void AddBulletToPool(Bullet bullet);
    }
}