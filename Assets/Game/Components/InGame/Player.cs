namespace SpaceShooterProject.Component
{
    using UnityEngine;
    using Devkit.Base.Object;
    using Devkit.Base.Component;

    public class Player : MonoBehaviour, IUpdatable, IInitializable, IDestructible
    {
        public delegate void PlayerDownDelegate();
        public event PlayerDownDelegate OnPlayerDown;

        private InGameInputSystem inputSystemReferance;
        private Transform playerTransform;


        //[SerializeField] private ObjectPooler ObjectPooler;
        [SerializeField] private float shipSpeed = 20f;
        [SerializeField] private int HP = 100;
        [SerializeField] private SpriteRenderer shipSpriteRenderer;
        private ComponentContainer componentContainer;
        private CurrencyComponent currencyComponent;
        public float fireRate = 1.0f;
        public float fireNextSpawn = 2.0f;

        private GameCamera gameCamera;
        private Vector2 screenBounds;

        private float fireTime;
        private BulletCollector bulletCollector;

        public void Init()
        {
            HideShip();

            if (gameCamera == null) 
            {
                gameCamera = Camera.main.GetComponent<GameCamera>();
            }

            if (currencyComponent == null) 
            {
                currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
            }

            if (playerTransform == null) 
            {
                playerTransform = GetComponent<Transform>();
            }

            fireTime = 0;
        }

        public void InjectBulletCollector(BulletCollector bulletCollector) 
        {
            this.bulletCollector = bulletCollector;
        }

        public void PreInit()
        {
        }

        public void ShowShip()
        {
            shipSpriteRenderer.enabled = true;
        }

        public void HideShip()
        {
            shipSpriteRenderer.enabled = false;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Coin"))
            {
                currencyComponent.EarnGold(10);
                Destroy(collider.gameObject);
            }

            if (collider.gameObject.CompareTag("EnemyBullet"))
            {
                var bullet = collider.gameObject.GetComponent<Bullet>();
                HP -= bullet.GetDamage();
            }
        }

        public void CallUpdate()
        {
            fireTime += Time.deltaTime;
            playerTransform.Translate(Vector3.up * gameCamera.CameraSpeed * Time.deltaTime, Space.World);
            Shoot();

            if (Input.GetKeyDown(KeyCode.C)) 
            {
                GetHit(50);
            }
        }

        public void OnTouchUp()
        {
            Time.timeScale = 0.5f;
        }

        public void OnTouch()
        {
            Time.timeScale = 1f;
            var screenPos = gameCamera.ScreenToWorldPoint(Input.mousePosition);
            playerTransform.position = Vector2.Lerp(playerTransform.position, screenPos, shipSpeed * Time.deltaTime);

            // TODO min max ekran değerleri için fonksiyon yazılacak
        }

        public void InjectInputSystem(InGameInputSystem inputSystem)
        {
            if(inputSystemReferance == null)
            {
                inputSystemReferance = inputSystem;
            }
            inputSystemReferance.OnScreenTouch += OnScreenTouch;
            inputSystemReferance.OnScreenTouchEnter += OnTouch;
            inputSystemReferance.OnScreenTouchExit += OnTouchUp;
        }

        private void OnScreenTouch()
        {
        }

        public void OnDestruct()
        {
            inputSystemReferance.OnScreenTouch -= OnScreenTouch;
            inputSystemReferance.OnScreenTouchEnter -= OnTouch;
            inputSystemReferance.OnScreenTouchExit -= OnTouchUp;
        }

        private void Shoot()
        {
            if (fireTime > fireNextSpawn)
            {
                Bullet bullet = bulletCollector.GetBullet();
                bullet.transform.position = transform.position;
                fireNextSpawn += fireRate;
            }
        }

        public ComponentContainer ComponentContainer
        {
            get => componentContainer;
            set => componentContainer = value;
        }

        private void GetHit(int damage) 
        {
            HP -= damage;

            if (HP <= 0) 
            {
                if (OnPlayerDown != null) 
                {
                    OnPlayerDown();
                } 
            }
        }
    }
}