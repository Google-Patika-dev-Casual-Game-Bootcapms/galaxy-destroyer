namespace SpaceShooterProject.Component
{ 

    using UnityEngine;
    using System.Collections;

    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private float shipSpeed = 20f;
        [SerializeField] private SpriteRenderer shipSpriteRender;
        private float frameRate = 0;
        private float fireRate = 20;

        public void Activate()
        {
            
        }

        public abstract void Attack();

        public abstract void Patrol();

        public void CallUpdate()
        {
            
        }

        public void Deactivate()
        {
            
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