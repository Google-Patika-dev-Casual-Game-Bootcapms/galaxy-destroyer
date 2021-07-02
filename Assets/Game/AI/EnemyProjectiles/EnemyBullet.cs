namespace SpaceShooterProject.AI.Projectiles
{
    using UnityEngine;
    using Devkit.Base.Pattern.ObjectPool;
    using System.Collections;
    using SpaceShooterProject.Component;

    public class EnemyBullet : MonoBehaviour, IPoolable
    {
        [SerializeField] private float bulletSpeed = 3f;
        [SerializeField] private float directionAngle = 270f; //Angle is in anticlockwise so 270 is down
        [SerializeField] private RectTransform _transform;

        //public delegate void EnemyBulletTrigger();

        //public event EnemyBulletTrigger OnHitPlayer;

        private IEnemyBulletCollector enemyBulletCollector;
        private Camera mainCamera;

        public void Shoot()
        {
            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            bool isOutOfScreen;
            float translateX = Mathf.Cos(Mathf.Deg2Rad * directionAngle) * bulletSpeed;
            float translateY = Mathf.Sin(Mathf.Deg2Rad * directionAngle) * bulletSpeed;
            Vector2 translationVector;
            translationVector.x = translateX;
            translationVector.y = translateY;
            do
            {
                // Inside screen
                isOutOfScreen = IsOutOfScreen();
                
                _transform.Translate(translationVector);
                yield return new WaitForEndOfFrame();

            } while (!isOutOfScreen);

            //Out of screen
            enemyBulletCollector.AddBulletToPool(this);
            yield return null;
        }

        private bool IsOutOfScreen()
        {
            Vector2 normalizedPosition = mainCamera.WorldToViewportPoint(_transform.position);
            return (normalizedPosition.x < 0 || normalizedPosition.x > 1) ||
                (normalizedPosition.y < 0 || normalizedPosition.y > 1);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Initialize()
        {
            mainCamera = Camera.main;
        }

        public void SetPosition(Vector2 initialPosition)
        {
            _transform.position = initialPosition;
        }

        public void SetSpeed(float speed)
        {
            this.bulletSpeed = speed;
        }

        public void SetAngle(float angle)
        {
            this.directionAngle = angle;
        }

        public void InjectEnemyBulletCollector(IEnemyBulletCollector enemyBulletCollector)
        {
            if(this.enemyBulletCollector == null)
            {
                this.enemyBulletCollector = enemyBulletCollector;
            }
        }

        public void InjectBulletCollector(BulletCollector bulletCollector)
        {
            
        }
    }

}

