namespace SpaceShooterProject.AI.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using SpaceShooterProject.AI.Movements;
    using UnityEngine;

    public abstract class Enemy : MonoBehaviour, IEnemy
    {
        private List<Route> routes = new List<Route>();
        [SerializeField] private float speed;
        [SerializeField] private int health;

        [SerializeField]
        Sprite enemySprite;

        [SerializeField]
        protected Camera mainCamera;

        protected IMovement movement;

        public abstract void RouteFinished();

        public abstract void PatrolRouteFinished();

        public abstract bool IsMovementInterrupted();

        public abstract bool IsMovementContinue();

        public abstract void OnOutOfScreen();

        public abstract void OnUpdate();

        public virtual bool IsOutOfScreen()
        {
            Vector2 normalizedPosition = mainCamera.WorldToViewportPoint(transform.position);
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

    }
}


