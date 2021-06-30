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

        [SerializeField]
        Sprite enemySprite;

        protected IMovement movement;


        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void Movement()
        {
            movement.Move(this);
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


