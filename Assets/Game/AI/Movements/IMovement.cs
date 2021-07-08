namespace SpaceShooterProject.AI.Movements
{
    using SpaceShooterProject.AI.Enemies;
    using System;
    using UnityEngine;
    public interface IMovement
    {
        //public void Initialize(Enemy minion);
        public void Move(Enemy enemy);
        public void Patrol(Enemy enemy);
        public void BossPatrol(Enemy enemy);
    }
}
