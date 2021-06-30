namespace SpaceShooterProject.AI.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using SpaceShooterProject.AI.Movements;
    using UnityEngine;

    public class Helicopter : Minion, IHelicopter
    {
        private HelicopterEventContainer helicopterEventContainer;

        [SerializeField]
        private float initialDistance = 10f;

        public void InitializeContainer(HelicopterEventContainer helicopterEventContainer)
        {
            this.helicopterEventContainer = helicopterEventContainer;
        }

        public void InitializeContainerMethods()
        {
            helicopterEventContainer.OnEnterTheSceneStateEnter += OnEnterTheSceneStateEnter;
        }


        public void OnEnterTheSceneStateEnter()
        {

            Debug.Log("Enter the scene event");

            Vector2 startPoint;
            startPoint.x = transform.position.x;
            startPoint.y = transform.position.y;

            Vector2 endPoint;
            endPoint.x = transform.position.x;
            endPoint.y = transform.position.y - initialDistance;

            Debug.Log(startPoint);
            Debug.Log(endPoint);

            LinearRoute initialRoute = new LinearRoute();

            initialRoute.AddControlPoint(startPoint);
            initialRoute.AddControlPoint(endPoint);

            AddRoute(initialRoute);
            movement = new PathMovement();
            Movement();


        }


        public bool IsDeath()
        {
            return false;
        }

        public bool IsEnterTheSceneAnimationFinish()
        {
            return false;
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


