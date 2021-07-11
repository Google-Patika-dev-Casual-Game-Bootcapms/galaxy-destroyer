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
        public delegate void LevelCompletedDelegate();
        public event GameOverDelegate OnGameOver;
        public event LevelCompletedDelegate OnLevelCompleted;

        [SerializeField] private Player player;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameCamera gameCamera;
        private InGameInputSystem inputSystem;
        private InGameWeaponUpgradeComponent weaponUpgradeComponent;
        private BulletCollector bulletCollector;
        private EnemyFactory enemyFactory;
        private InGameMessageBroadcaster inGameMessageBroadcaster;
        private ComponentContainer componentContainer;
        private bool isGameOver;

        public void Initialize(ComponentContainer componentContainer)
        {
			this.componentContainer = componentContainer;
			
            inGameMessageBroadcaster = new InGameMessageBroadcaster();
            inGameMessageBroadcaster.Initialize(componentContainer);
			
			 Debug.Log("<color=green>GamePlayComponent initialized!</color>");
            inputSystem = componentContainer.GetComponent("InGameInputSystem") as InGameInputSystem;

            InitializeWeaponUpgradeComponent(componentContainer);

            CreatePlayer();
			
		}

        private void CreatePlayer()
        {
            player = Instantiate(playerPrefab).GetComponent<Player>();
            player.InjectInputSystem(inputSystem);
            player.ComponentContainer = componentContainer;
            player.Init();
            bulletCollector = new BulletCollector();
            player.InjectBulletCollector(bulletCollector);

            enemyFactory = new EnemyFactory(inGameMessageBroadcaster);
            enemyFactory.Init(componentContainer);

            player.OnPlayerDown += FinishGame;
        }

        public void SendGameIsStartedMessage()
        {
            isGameOver = false;
            enemyFactory.ResetSpawnLogic();
        }

        public void TriggerSpawnEnemies()
        {
            enemyFactory.SpawnNextEnemyWave();
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
        }

        public bool GetLastGameResult()
        {
            //TODO: configure
            return true;
        }

        public bool IsGameOver()
        {
            return isGameOver;
        }

        private void LateUpdate()
        {
            if (gameCamera == null) 
            {
                return;
            }

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
            //player.OnDestruct();
            inputSystem.OnDestruct();
            bulletCollector.OnDestruct();
            enemyFactory.OnDestruct();
            isGameOver = true;
            //inGameMessageBroadcaster.RemoveAllEvents();//TODO: research!!!

            if (OnGameOver != null) 
            {
                OnGameOver();
            }
        }

        public void TriggerLevelCompleted() 
        {
            if (OnLevelCompleted != null) 
            {
                OnLevelCompleted();
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