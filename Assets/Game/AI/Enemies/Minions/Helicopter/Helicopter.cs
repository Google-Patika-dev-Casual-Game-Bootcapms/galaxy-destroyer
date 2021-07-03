namespace SpaceShooterProject.AI.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using SpaceShooterProject.AI.Movements;
    using SpaceShooterProject.AI.Projectiles;
    using SpaceShooterProject.AI.State;
    using UnityEngine;

    public class Helicopter : Minion, IHelicopter
    {
        private HelicopterEventContainer helicopterEventContainer;
        public HelicopterMainState helicopterMainState;
        private IEnemyBulletCollector enemyBulletCollector;

        [SerializeField]
        private float initialDistance = 100f;

        [SerializeField]
        private float patrolRouteWidth = 100f;

        [SerializeField]
        private float patrolRouteHeight = 100f;

        [SerializeField]
        private float patrolTimeUntilAttack = 3f;

        [SerializeField]
        private float fireRate = 0.01f;

        [SerializeField]
        private int bulletPerAttack = 4;

        private bool isEnterTheSceneMovementFinish = false;
        private bool isPatrolTimeFinished = false;
        private bool isShootingSessionEnd = false;
        private bool isDeath = false;

        public override void OnUpdate()
        {
            helicopterMainState.Update();
        }

        public void Initialize()
        {
            mainCamera = Camera.main;
            movement = new PathMovement();
            enemyBulletCollector = new EnemyBulletCollector();
            helicopterEventContainer = new HelicopterEventContainer();
            helicopterMainState = new HelicopterMainState(this, helicopterEventContainer);
            InitializeEvents();
            helicopterMainState.Enter();
            
        }

        public void InitializeEvents()
        {
            helicopterEventContainer.OnEnterTheSceneStateEnter += OnEnterTheSceneStateEnter;
            helicopterEventContainer.OnEnterTheSceneStateExit += OnEnterTheSceneStateExit;
            helicopterEventContainer.OnPatrolStateEnter += OnPatrolStateEnter;
            helicopterEventContainer.OnPatrolStateExit += OnPatrolStateExit;
            helicopterEventContainer.OnAttackStateEnter += OnAttackStateEnter;
            helicopterEventContainer.OnAttackStateExit += OnAttackStateExit;
            helicopterEventContainer.OnDeathStateEnter += OnDeathStateEnter;
            helicopterEventContainer.OnDeathStateExit += OnDeathStateExit;

        }

        public void DestroyEvents()
        {
            // Call when helicopter dies
            helicopterEventContainer.OnEnterTheSceneStateEnter -= OnEnterTheSceneStateEnter;
            helicopterEventContainer.OnEnterTheSceneStateExit -= OnEnterTheSceneStateExit;
            helicopterEventContainer.OnPatrolStateEnter -= OnPatrolStateEnter;
            helicopterEventContainer.OnPatrolStateExit -= OnPatrolStateExit;
            helicopterEventContainer.OnAttackStateEnter -= OnAttackStateEnter;
            helicopterEventContainer.OnAttackStateExit -= OnAttackStateExit;
            helicopterEventContainer.OnDeathStateEnter -= OnDeathStateEnter;
            helicopterEventContainer.OnDeathStateExit -= OnDeathStateExit;
        }

        public void FireBullet()
        {
            Vector2 initialPosition;
            initialPosition.x = transform.position.x;
            initialPosition.y = transform.position.y;

            enemyBulletCollector.ShootEnemyBullet(initialPosition);

        }


        public void OnEnterTheSceneStateEnter()
        {

            Vector2 startPoint;
            startPoint.x = transform.position.x;
            startPoint.y = transform.position.y;

            Vector2 endPoint;
            endPoint.x = transform.position.x;
            endPoint.y = transform.position.y - initialDistance;


            LinearRoute initialRoute = new LinearRoute();

            initialRoute.AddControlPoint(startPoint);
            initialRoute.AddControlPoint(endPoint);

            AddRoute(initialRoute);
            Movement();
        }

        public void OnEnterTheSceneStateExit()
        {
            GetRoutes().Clear();
        }

        public void OnPatrolStateEnter()
        {
            Debug.Log("Patrol state enter");

            StartCoroutine(PatrolTimer());
            
            Vector2 startPoint;
            startPoint.x = transform.position.x;
            startPoint.y = transform.position.y;

            Vector2 middlePoint;
            middlePoint.x = transform.position.x - patrolRouteWidth;
            middlePoint.y = transform.position.y;

            Vector2 endPoint;
            endPoint.x = transform.position.x - patrolRouteWidth;
            endPoint.y = transform.position.y - patrolRouteHeight;

            QuadraticRoute patrolRoute = new QuadraticRoute();

            patrolRoute.AddControlPoint(startPoint);
            patrolRoute.AddControlPoint(middlePoint);
            patrolRoute.AddControlPoint(endPoint);

            AddRoute(patrolRoute);
            Patrol();
        }


        public void OnPatrolStateExit()
        {
            StopCoroutine(PatrolTimer());
        }

        public void OnAttackStateEnter()
        {
            StartCoroutine(Attack());
        }

        public void OnAttackStateExit()
        {
            StopCoroutine(Attack());
        }

        public void OnDeathStateEnter()
        {
            //TODO: Destory or put into pool
            Debug.Log("Heli dead");
        }

        public void OnDeathStateExit()
        {

        }

        private IEnumerator PatrolTimer()
        {
            float time = patrolTimeUntilAttack;

            float currentTime = 0f;

            while(currentTime < time)
            {
                currentTime += Time.deltaTime;
                yield return null;
            }

            OnPatrolTimeFinished();

        }

        private IEnumerator Attack()
        {
            int attackCount = bulletPerAttack;
            float totalTime = (1 / fireRate);
            
            int currentBulletCount = 0;

            while (currentBulletCount < attackCount)
            {
                float currentTime = 0;
                while (currentTime < totalTime)
                {
                    currentTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }
                
                FireBullet();
                currentBulletCount += 1;
            }

            OnShootingSessionEnd();

        }


        public override void RouteFinished()
        {
            Debug.Log("Route is finished");
            OnEnterTheSceneMovementFinish();
        }

        public void OnEnterTheSceneMovementFinish()
        {
            isEnterTheSceneMovementFinish = true;
        }

        public void OnPatrolTimeFinished()
        {
            isPatrolTimeFinished = true;
            isShootingSessionEnd = false;
        }

        public void OnShootingSessionEnd()
        {
            isPatrolTimeFinished = false;
            isShootingSessionEnd = true;
        }

        public void OnDeath()
        {
            isDeath = true;
        }


        public bool IsDeath()
        {
            return isDeath;
        }

        public bool IsEnterTheSceneAnimationFinish()
        {
            return isEnterTheSceneMovementFinish;
        }

        public bool IsPatrolTimeFinished()
        {
            return isPatrolTimeFinished;
        }

        public bool IsShootingSessionEnd()
        {
            return isShootingSessionEnd;
        }

        public override bool IsMovementInterrupted()
        {
            return IsPatrolTimeFinished();
        }

        public override bool IsMovementContinue()
        {
            return IsShootingSessionEnd();
        }

        public override void OnOutOfScreen()
        {
            OnDeath();
        }

        
    }

}


