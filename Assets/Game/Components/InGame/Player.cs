namespace SpaceShooterProject.Component
{
    using UnityEngine;
    using Devkit.Base.Object;

    public class Player : MonoBehaviour, IUpdatable, IInitializable, IDestructible
    {
        private InGameInputSystem inputSystemReferance;
        [SerializeField] private ObjectPooler ObjectPooler;
        private Transform myTransform;
        private float shipSpeed = 100f;
        private float frameRate = 0;
        private float fireRate = 20;

        private float sceneSpeed = 1f;

        public void Init()
        {
        }

        public void PreInit()
        {
        }

        public void CallUpdate()
        {
            
        }

        public void CallFixedUpdate()
        {
        }

        public void CallLateUpdate()
        {
        }

        public void OnTouchUp()
        {
            Debug.Log("Parmak Kalkti");
            Time.timeScale = 0f;
        }

        public void OnTouch()
        {
            Time.timeScale = 1f;
            var screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = Vector2.MoveTowards(transform.position,
                screenPos,
                shipSpeed * Time.deltaTime);

            // var screenLimitX = Screen.width/Screen.currentResolution.width;
            // var screenLimitY = Screen.height/Screen.currentResolution.height;
            // TODO min max ekran değerleri için fonksiyon yazılacak

            // gameObject.transform.position = new Vector2(Mathf.Clamp(gameObject.transform.position.x,-2.5f,2.5f),
            //     Mathf.Clamp(gameObject.transform.position.y,-4.5f,4.5f));
            sceneSpeed = 3f;
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

        void Shoot()
        {
            GameObject bullet = ObjectPooler.GetPooledObject(0);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        }
    }
}