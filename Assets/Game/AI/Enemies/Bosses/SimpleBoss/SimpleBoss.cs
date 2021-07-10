namespace SpaceShooterProject.AI.Enemies
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using SpaceShooterProject.AI.Movements;
    using SpaceShooterProject.AI.Projectiles;
    using SpaceShooterProject.AI.State;
    using UnityEngine;

    public class SimpleBoss : Boss, ISimpleBoss
    {
        private SimpleBossEventContainer simpleBossEventContainer;
        public SimpleBossMainState simpleBossMainState;
        private IEnemyBulletCollector enemyBulletCollector;
        private IMissileCollector enemyMissileCollector;
        // TODO: Missile collector

        [SerializeField]
        private Vector2 firstPatrolPoint;

        [SerializeField]
        private Vector2 secondPatrolPoint;

        [SerializeField]
        private Vector2 thirdPatrolPoint;

        [SerializeField]
        private Vector2 fourthPatrolPoint;

        [SerializeField]
        private float fireRate = 6f;

        [SerializeField]
        private int firePerAttack = 4;

        [SerializeField]
        private float shootingAngle = 270f; // Down

        [SerializeField]
        private float scatteringBulletAngleDifference = 30f;

        [SerializeField]
        private Transform target;

        private bool isEnterTheSceneMovementFinish = false;
        private bool isPatrolTimeFinished = false;
        private bool isShootingSessionEnd = false;
        private bool isDeath = false;
        private bool isPatrolRouteInitialized = false;

        public override void OnUpdate()
        {
            simpleBossMainState.Update();
        }

        public void Initialize()
        {
            mainCamera = Camera.main;
            movement = new PathMovement();
            enemyBulletCollector = new EnemyBulletCollector();
            enemyMissileCollector = new MissileCollector();
            simpleBossEventContainer = new SimpleBossEventContainer();
            simpleBossMainState = new SimpleBossMainState(this, simpleBossEventContainer);
            InitializeEvents();
            simpleBossMainState.Enter();

        }

        public void InitializeEvents()
        {
            simpleBossEventContainer.OnEnterSceneStateEnter += OnEnterSceneStateEnter;
            simpleBossEventContainer.OnEnterSceneStateExit += OnEnterSceneStateExit;
            simpleBossEventContainer.OnPatrolStateEnter += OnPatrolStateEnter;
            simpleBossEventContainer.OnPatrolStateExit += OnPatrolStateExit;
            simpleBossEventContainer.OnAttackStateEnter += OnAttackStateEnter;
            simpleBossEventContainer.OnAttackStateExit += OnAttackStateExit;
            simpleBossEventContainer.OnDeathStateEnter += OnDeathStateEnter;
            simpleBossEventContainer.OnDeathStateExit += OnDeathStateExit;

        }

        

        public void DestroyEvents()
        {
            // Call when helicopter dies
            simpleBossEventContainer.OnEnterSceneStateEnter -= OnEnterSceneStateEnter;
            simpleBossEventContainer.OnEnterSceneStateExit -= OnEnterSceneStateExit;
            simpleBossEventContainer.OnPatrolStateEnter -= OnPatrolStateEnter;
            simpleBossEventContainer.OnPatrolStateExit -= OnPatrolStateExit;
            simpleBossEventContainer.OnAttackStateEnter -= OnAttackStateEnter;
            simpleBossEventContainer.OnAttackStateExit -= OnAttackStateExit;
            simpleBossEventContainer.OnDeathStateEnter -= OnDeathStateEnter;
            simpleBossEventContainer.OnDeathStateExit -= OnDeathStateExit;
        }

        private void OnDeathStateExit()
        {
            DestroyEvents();
        }

        private void OnAttackStateExit()
        {
            StopCoroutine(Attack());
        }

        private void OnDeathStateEnter()
        {
            GetRoutes().Clear();
        }

        private void OnAttackStateEnter()
        {
            StartCoroutine(Attack());
        }

        private IEnumerator Attack()
        {
            int attackCount = firePerAttack;
            float totalTime = (1 / fireRate);

            int currentFireCount = 0;

            //FireGuidedMissile();
            while (currentFireCount < attackCount)
            {
                float currentTime = 0;
                while (currentTime < totalTime)
                {

                    currentTime += Time.deltaTime;
                    yield return new WaitForEndOfFrame();
                }


                FireBullets();
                
                currentFireCount += 1;
            }
            


            OnShootingSessionEnd();

        }

        private void OnShootingSessionEnd()
        {
            isShootingSessionEnd = true;
            isPatrolTimeFinished = false;
        }

        public void FireBullets()
        {
            Vector2 initialPosition;
            initialPosition.x = transform.position.x;
            initialPosition.y = transform.position.y;

            enemyBulletCollector.ShootEnemyBullet(initialPosition, shootingAngle);
            enemyBulletCollector.ShootEnemyBullet(initialPosition, shootingAngle + scatteringBulletAngleDifference);
            enemyBulletCollector.ShootEnemyBullet(initialPosition, shootingAngle - scatteringBulletAngleDifference);

        }

        public void FireGuidedMissile()
        {
            Vector2 initialPosition;
            initialPosition.x = transform.position.x;
            initialPosition.y = transform.position.y;
            enemyMissileCollector.ShootEnemyGuidedMissile(initialPosition, target);
        }

        private void OnPatrolStateExit()
        {
            
        }

        private void OnPatrolStateEnter()
        {
            InitializePatrolRoute();
            BossPatrol();
        }

        private void InitializePatrolRoute()
        {
            if (!isPatrolRouteInitialized)
            {
                LinearRoute firstRoute = new LinearRoute();
                firstRoute.AddControlPoint(firstPatrolPoint);
                firstRoute.AddControlPoint(secondPatrolPoint);

                LinearRoute secondRoute = new LinearRoute();
                secondRoute.AddControlPoint(secondPatrolPoint);
                secondRoute.AddControlPoint(thirdPatrolPoint);

                LinearRoute thirdRoute = new LinearRoute();
                thirdRoute.AddControlPoint(thirdPatrolPoint);
                thirdRoute.AddControlPoint(fourthPatrolPoint);

                LinearRoute fourthRoute = new LinearRoute();
                fourthRoute.AddControlPoint(fourthPatrolPoint);
                fourthRoute.AddControlPoint(firstPatrolPoint);

                AddRoute(firstRoute);
                AddRoute(secondRoute);
                AddRoute(thirdRoute);
                AddRoute(fourthRoute);
                isPatrolRouteInitialized = true;
            }
        }

        

        private void OnEnterSceneStateExit()
        {
            GetRoutes().Clear();
        }

        private void OnEnterSceneStateEnter()
        {
            Vector2 startPoint;
            startPoint.x = transform.position.x;
            startPoint.y = transform.position.y;

            LinearRoute initialRoute = new LinearRoute();
            initialRoute.AddControlPoint(startPoint);
            initialRoute.AddControlPoint(firstPatrolPoint);

            AddRoute(initialRoute);
            Movement();
        }

        public bool IsDeath()
        {
            return isDeath;
        }

        public bool IsEnterTheSceneAnimationFinish()
        {
            return isEnterTheSceneMovementFinish;
        }

        public override bool IsMovementContinue()
        {
            return isShootingSessionEnd;
        }

        public override bool IsMovementInterrupted()
        {
            return false; // No need
        }

        public bool IsPatrolTimeFinished()
        {
            return isPatrolTimeFinished;
        }

        public bool IsShootingSessionEnd()
        {
            return isShootingSessionEnd;
        }

        public override void OnOutOfScreen()
        {
            isDeath = true;
        }

        public override void RouteFinished()
        {
            isEnterTheSceneMovementFinish = true;
        }

        public override void PatrolRouteFinished()
        {
            isPatrolTimeFinished = true;
            isShootingSessionEnd = false;
        }

    }
}

