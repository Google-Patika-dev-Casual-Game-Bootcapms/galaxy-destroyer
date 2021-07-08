namespace SpaceShooterProject.AI.Projectiles
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IMissileCollector
    {
        EnemyMissile GetEnemyMissile();
        void AddMissileToPool(EnemyMissile missile);
        public void ShootEnemyMissile(Vector2 initialPosition);
        public void ShootEnemyMissile(Vector2 initialPosition, float angle);
        public void ShootEnemyMissile(Vector2 initialPosition, float angle, float speed);
        public void ShootEnemyGuidedMissile(Vector2 initialPosition, Transform target);
    }
}

