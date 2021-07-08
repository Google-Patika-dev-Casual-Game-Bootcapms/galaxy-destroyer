namespace SpaceShooterProject.AI.Projectiles
{
    using UnityEngine;
    using System.Collections;

    public interface IEnemyBulletCollector 
    {
        EnemyBullet GetEnemyBullet();
        void AddBulletToPool(EnemyBullet bullet);
        public void ShootEnemyBullet(Vector2 initialPosition);
        public void ShootEnemyBullet(Vector2 initialPosition, float angle);
        public void ShootEnemyBullet(Vector2 initialPosition, float angle, float speed);
    }
}