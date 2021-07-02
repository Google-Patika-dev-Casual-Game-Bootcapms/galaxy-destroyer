namespace SpaceShooterProject.AI.Projectiles
{
    using UnityEngine;
    using Devkit.Base.Pattern.ObjectPool;
    using System.Collections;

    //public delegate void EnemyBulletTrigger();

    public class EnemyBulletCollector : IEnemyBulletCollector
    {
        private const int INITIAL_BULLET_IN_POOL = 10;
        private Pool<EnemyBullet> pool;
        private const string SOURCE_OBJECT_PATH = "Prefabs/EnemyBullet";

        
        //private event EnemyBulletTrigger playerHitFunction;

        public EnemyBulletCollector()
        {
            pool = new Pool<EnemyBullet>(SOURCE_OBJECT_PATH);
            pool.PopulatePool(INITIAL_BULLET_IN_POOL);
        }

        public EnemyBullet GetEnemyBullet()
        {
            var bullet = pool.GetObjectFromPool();
            bullet.InjectEnemyBulletCollector(this);
            return bullet;

        }

        public void AddBulletToPool(EnemyBullet bullet)
        {
            pool.AddObjectToPool(bullet);
        }

        public void ShootEnemyBullet(Vector2 initialPosition)
        {
            var bullet = GetEnemyBullet();
            bullet.SetPosition(initialPosition);
            bullet.Initialize();
            bullet.Shoot();
        }

        public void ShootEnemyBullet(Vector2 initialPosition, float angle)
        {
            var bullet = GetEnemyBullet();
            bullet.SetPosition(initialPosition);
            bullet.SetAngle(angle);
            bullet.Initialize();
            bullet.Shoot();
        }

        public void ShootEnemyBullet(Vector2 initialPosition, float angle, float speed)
        {
            var bullet = GetEnemyBullet();
            bullet.SetPosition(initialPosition);
            bullet.SetAngle(angle);
            bullet.SetSpeed(speed);
            bullet.Initialize();
            bullet.Shoot();
        }



    }
}
