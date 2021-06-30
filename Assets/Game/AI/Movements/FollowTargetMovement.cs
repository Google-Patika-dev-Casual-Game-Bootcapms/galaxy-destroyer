namespace SpaceShooterProject.AI.Movements
{
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class FollowTargetMovement : IMovement
    {

        [SerializeField] Transform targetObject;

        public void Initialize(Enemy minion)
        {
            throw new System.NotImplementedException();
        }

        public void Move(Enemy minion)
        {
            throw new System.NotImplementedException();
        }

        public void Patrol(Enemy enemy)
        {
            throw new System.NotImplementedException();
        }
    }

}