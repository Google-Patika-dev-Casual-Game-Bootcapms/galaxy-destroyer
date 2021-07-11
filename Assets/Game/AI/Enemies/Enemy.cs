namespace SpaceShooterProject.AI.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using SpaceShooterProject.AI.Movements;
    using Devkit.Base.Object;
    using SpaceShooterProject.Component;
    using UnityEngine;

    public abstract class Enemy : MonoBehaviour, IEnemy, IUpdatable
    {
        private List<Route> routes = new List<Route>();
        [SerializeField] private float speed;
        [SerializeField] private int health;
        //[SerializeField] private float frameRate = 0;

        protected const int defaultHP = 10;

        protected IGameCamera gameCamera;
        protected Transform enemyTransform;

        protected InGameMessageBroadcaster inGameMessageBroadcaster;
        protected EnemyType enemyType;

        protected IMovement movement;


        public abstract void OnInitialize();

        public abstract void RouteFinished();

        public abstract void PatrolRouteFinished();

        public abstract bool IsMovementInterrupted();

        public abstract bool IsMovementContinue();

        public abstract void OnOutOfScreen();

        public abstract void OnUpdate();

        public abstract void EnterMainState();

        public virtual bool IsOutOfScreen()
        {
            if(gameCamera == null)
            {
                Debug.Log("broken : " + enemyType);
                Debug.Log("Enemy game camera is null");
            }
            Vector2 normalizedPosition = gameCamera.WorldToViewportPoint(transform.position);
            return (normalizedPosition.x < 0 || normalizedPosition.x > 1) ||
                (normalizedPosition.y < 0 );
        }




        public void Movement()
        {
            movement.Move(this);
        }

        public void Patrol()
        {
            movement.Patrol(this);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("FriendlyBullet"))
            {
                var bullet = collider.gameObject.GetComponent<Bullet>();
                GetHit(bullet.GetDamage());
                bullet.OnHitEnemy();
            }
        }


        public float GetSpeed()
        {
            return speed;
        }

        public List<Route> GetRoutes()
        {
            return routes;
        }

        public Route GetRoute(int index)
        {
            return routes[index];
        }

        public void SetPosition(Vector2 newPosition)
        {
            this.transform.position = newPosition;
        }

        public Vector2 GetPosition()
        {
            return this.transform.position;
        }

        public void AddRoute(Route newRoute)
        {
            routes.Add(newRoute);
        }

        public void CallUpdate()
        {
            OnUpdate();
        }

        public void GetHit(int damage)
        {
            health -= damage;
        }

        public void Init()
        {
            enemyTransform = GetComponent<Transform>();
        }

        public void OnDestruct()
        {
            ResetHealth();
            gameObject.SetActive(false);
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public void PreInit()
        {
        }

        public void Initialize()
        {
            OnInitialize();
        }


        public void InjectBulletCollector(BulletCollector bulletCollector)
        {
            
        }

        public void SetType(EnemyType enemyType)
        {
            this.enemyType = enemyType;
        }

        public EnemyType GetEnemyType()
        {
            return enemyType;
        }

        public void ResetHealth()
        {
            health = defaultHP;
        }

        public void InjectMessageBroadcaster(InGameMessageBroadcaster inGameMessageBroadcaster)
        {
            this.inGameMessageBroadcaster = inGameMessageBroadcaster;
        }

        public void InjectGameCameraReference(IGameCamera gameCamera)
        {
            this.gameCamera = gameCamera;
        }

        internal void ResetEnemy()
        {
            ResetHealth();
            gameObject.SetActive(false);
        }
    }
}


