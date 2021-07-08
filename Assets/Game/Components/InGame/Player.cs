namespace SpaceShooterProject.Component
{
    using UnityEngine;
    using Devkit.Base.Object;
    using Devkit.Base.Component;

    public class Player : MonoBehaviour, IUpdatable, IInitializable, IDestructible
    {
        private InGameInputSystem inputSystemReferance;


        //[SerializeField] private ObjectPooler ObjectPooler;

        private Transform myTransform;
        [SerializeField] private float shipSpeed = 20f;
        [SerializeField] private SpriteRenderer shipSpriteRenderer;
        private ComponentContainer componentContainer;
        private CurrencyComponent currencyComponent;
        private float frameRate = 0;
        private float fireRate = 20;

        private GameCamera gameCamera;
        private bool a = true;

        public void Init()
        {
            HideShip();
            gameCamera = Camera.main.GetComponent<GameCamera>();
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
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
            //TODO: currencyComponent MainComponent'in içinde Initialize edilecek.
            if (collider.gameObject.CompareTag("Coin"))
            {
                currencyComponent.EarnGold(10);
                Destroy(collider.gameObject);
            }

        }
        public void CallUpdate()
        {
            transform.Translate(Vector3.up * gameCamera.CameraSpeed * Time.deltaTime, Space.World);
        }

        public void CallFixedUpdate()
        {
        }

        public void CallLateUpdate()
        {
        }
        public void OnTouchUp()
        {
            Time.timeScale = 0.5f;
        }

        public float FrameRate
        {
            get => frameRate;
            set => frameRate = value;
        }

        public float FireRate => fireRate;

        public void OnTouch()
        {
            Time.timeScale = 1f;
            var screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = Vector2.Lerp(transform.position, screenPos, shipSpeed * Time.deltaTime);

            // var screenLimitX = Screen.width/Screen.currentResolution.width;
            // var screenLimitY = Screen.height/Screen.currentResolution.height;
            // TODO min max ekran değerleri için fonksiyon yazılacak

            // gameObject.transform.position = new Vector2(Mathf.Clamp(gameObject.transform.position.x,-2.5f,2.5f),
            //     Mathf.Clamp(gameObject.transform.position.y,-4.5f,4.5f));

        }

        public void InjectInputSystem(InGameInputSystem inputSystem)
        {
            inputSystemReferance = inputSystem;
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

        public void Shoot(Bullet bullet)
        {
            bullet.transform.position = transform.position;
        }

        public ComponentContainer ComponentContainer
        {
            get => componentContainer;
            set => componentContainer = value;
        }

    }
}