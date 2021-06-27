using System;

namespace SpaceShooterProject.Component
{
    using UnityEngine;
    using System.Collections;

    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private float shipSpeed = 20f;
        [SerializeField] private int hp = 100;
        [SerializeField] private SpriteRenderer shipSpriteRender;
        [SerializeField] private float frameRate = 0;
        [SerializeField] private float fireRate = 20;

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public abstract void Attack();

        public abstract void Patrol();

        public abstract void GetHit(int damage);

        public abstract void Death();

        public abstract void OutOfScreen();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("FriendlyBullet"))
            {
                var bullet = collider.gameObject.GetComponent<Bullet>();
                GetHit(bullet.GetDamage());
            }
        }

        public int HP
        {
            get => hp;
            set => hp = value;
        }

        public void CallUpdate()
        {
            if (HP<=0)
            {
                Destroy(gameObject);
            }
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