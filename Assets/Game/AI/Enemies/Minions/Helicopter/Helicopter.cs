namespace SpaceShooterProject.AI.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using SpaceShooterProject.AI.Movements;
    using SpaceShooterProject.AI.State;
    using UnityEngine;

    public class Helicopter : Minion, IHelicopter
    {
        private HelicopterEventContainer helicopterEventContainer;
        public HelicopterMainState helicopterMainState;

        [SerializeField]
        private float initialDistance = 100f;

        [SerializeField]
        private float patrolRouteWidth = 100f;

        [SerializeField]
        private float patrolRouteHeight = 100f;

        private bool isEnterTheSceneMovementFinish = false;

        public void Initialize()
        {
            movement = new PathMovement();
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

        }

        public void DestroyEvents()
        {
            // Call when helicopter dies
            helicopterEventContainer.OnEnterTheSceneStateEnter -= OnEnterTheSceneStateEnter;
            helicopterEventContainer.OnEnterTheSceneStateExit -= OnEnterTheSceneStateExit;
            helicopterEventContainer.OnPatrolStateEnter -= OnPatrolStateEnter;
            helicopterEventContainer.OnPatrolStateExit -= OnPatrolStateExit;
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


        public bool IsDeath()
        {
            return false;
        }

        public bool IsEnterTheSceneAnimationFinish()
        {
            return isEnterTheSceneMovementFinish;
        }

        public bool IsPatrolTimeFinished()
        {
            return false;
        }

        public bool IsShootingSessionEnd()
        {
            return false;
        }


    }

}


