namespace SpaceShooterProject.Component
{ 

    using UnityEngine;
    using System.Collections;

    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private float shipSpeed = 20f;
        [SerializeField] private SpriteRenderer shipSpriteRender;
        [SerializeField] private float frameRate = 0;
        [SerializeField] private float fireRate = 20;

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public abstract void Attack();

        public abstract void Patrol();

        public abstract void GetHit();

        public abstract void Death();

        public abstract void OutOfScreen();

        public void CallUpdate()
        {
            
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void Init()
        {
            
        }

        public void Initialize()
        {
            
        }

        public void InjectBulletCollector(BulletCollector bulletCollector)
        {
            
        }

        public void OnDestruct()
        {
            
        }

        public void PreInit()
        {
            
        }

    }


}