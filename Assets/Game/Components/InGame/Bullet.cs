namespace SpaceShooterProject.Component
{
    using Devkit.Base.Object;
    using Devkit.Base.Pattern.ObjectPool;
    using UnityEngine;
    using Random = UnityEngine.Random;

    public class Bullet : MonoBehaviour, IPoolable, IUpdatable
    {
        [SerializeField] private float speed = 3f;
        [SerializeField] private int damage = 10;
        [SerializeField] private RectTransform _transform;


        public delegate void BulletPoolDelegate(Bullet bullet);

        public BulletPoolDelegate OnBulletOutOfScreen;

        private IBulletCollector bulletCollectorReference;
        private GameCamera gameCamera;

        public void Initialize()
        {
            gameCamera = Camera.main.GetComponent<GameCamera>();
        }

        public void CallUpdate()
        {
            _transform.Translate(Vector3.up * (speed + gameCamera.CameraSpeed) * Time.deltaTime,
                Space.World);
            if (_transform.position.y > Camera.main.ViewportToWorldPoint(new Vector2(Random.value, 1)).y)
            {
                bulletCollectorReference.AddBulletToPool(this);
            }
        }

        public void InjectBulletCollector(BulletCollector bulletCollector)
        {
            if (bulletCollectorReference != null)
            {
                return;
            }

            bulletCollectorReference = bulletCollector;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public int GetDamage()
        {
            return damage;
        }
        public void OnHitEnemy()
        {
            bulletCollectorReference.AddBulletToPool(this);
        }
    }
}