namespace SpaceShooterProject.Component
{
    using Devkit.Base.Component;
    using Devkit.Base.Pattern.ObjectPool;
    using Devkit.Base.Object;
    using UnityEngine;

    public class GamePlayComponent : MonoBehaviour, IComponent, IUpdatable
    {
        [SerializeField] private Player player;
        [SerializeField] private GameCamera gameCamera;
        private InGameInputSystem inputSystem;
        private InGameWeaponUpgradeComponent weaponUpgradeComponent;
        private BulletCollector bulletCollector;


        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>GamePlayComponent initialized!</color>");
            inputSystem = componentContainer.GetComponent("InGameInputSystem") as InGameInputSystem;

            InitializeWeaponUpgradeComponent(componentContainer);

            player.InjectInputSystem(inputSystem);
            player.ComponentContainer = componentContainer ;
            player.Init();
            bulletCollector = new BulletCollector();
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
            player.FrameRate++;
            if (player.FrameRate % player.FireRate == 0)
            {
                player.Shoot(bulletCollector.GetBullet());
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

    public class BulletCollector
    {
        private Pool<Bullet> pool;
        private const string SOURCE_OBJECT_PATH = "Prefabs/BulletForPooling";

        public BulletCollector()
        {
            pool = new Pool<Bullet>(SOURCE_OBJECT_PATH);
            pool.PopulatePool(20);
            foreach (var bullet in pool.GetPool)
            {
                bullet.OnBulletOutOfScreen += OnBulletOutOfScreen;
            }
        }

        private void OnBulletOutOfScreen(Bullet bullet)
        {
            pool.AddObjectToPool(bullet);
        }

        public Bullet GetBullet()
        {
            return pool.GetObjectFromPool();
        }

        /*private void SubscribeAllBullets()
        {
            foreach (var bullet in pool.GetPool.ToArray())
            {
                
            }
        }*/
    }
}