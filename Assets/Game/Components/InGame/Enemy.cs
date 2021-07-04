using System;

namespace SpaceShooterProject.Component
{
    using UnityEngine;
    using System.Collections;
    using Devkit.Base.Object;

    public abstract class Enemy : MonoBehaviour, IEnemy, IUpdatable
    {
        [SerializeField] private float shipSpeed = 20f;
        [SerializeField] private int hp = 100;
        [SerializeField] private SpriteRenderer shipSpriteRender;
        [SerializeField] private float frameRate = 0;
        [SerializeField] private float fireRate = 20;

        const int defaultHP = 10;

        protected InGameMessageBroadcaster inGameMessageBroadcaster;
        protected EnemyType enemyType;

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void SetType(EnemyType enemyType)
        {
            this.enemyType = enemyType;
        }

        public abstract void Attack();

        public abstract void Patrol();

        public void GetHit(int damage) 
        {
            HP -= damage;
        }

        public abstract void Death();

        public abstract void OutOfScreen();

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("FriendlyBullet"))
            {
                var bullet = collider.gameObject.GetComponent<Bullet>();
                GetHit(bullet.GetDamage());
                bullet.OnHitEnemy();
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
                Death();
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

        public void InjectMessageBroadcaster(InGameMessageBroadcaster inGameMessageBroadcaster)
        {
            this.inGameMessageBroadcaster = inGameMessageBroadcaster;
        }

        public EnemyType GetEnemyType() 
        {
            return enemyType;
        }

        public void ResetHealth() 
        {
            HP = defaultHP;
        }
    }
}