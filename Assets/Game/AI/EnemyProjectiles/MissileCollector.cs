namespace SpaceShooterProject.AI.Projectiles
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.Base.Pattern.ObjectPool;
    using UnityEngine;

    public class MissileCollector : IMissileCollector
    {
        private const int INITIAL_MISSILE_IN_POOL = 10;
        private Pool<EnemyMissile> pool;
        private const string SOURCE_OBJECT_PATH = "Prefabs/EnemyGuidedMissile";

        public MissileCollector()
        {
            pool = new Pool<EnemyMissile>(SOURCE_OBJECT_PATH);
            pool.PopulatePool(INITIAL_MISSILE_IN_POOL);
        }

        public void AddMissileToPool(EnemyMissile missile)
        {
            pool.AddObjectToPool(missile);
        }

        public EnemyMissile GetEnemyMissile()
        {
            var missile = pool.GetObjectFromPool();
            missile.InjectEnemyMissileCollector(this);
            return missile;
        }

        public void ShootEnemyMissile(Vector2 initialPosition)
        {
            var missile = GetEnemyMissile();
            missile.SetPosition(initialPosition);
            missile.Initialize();
            missile.Shoot();
        }

        public void ShootEnemyMissile(Vector2 initialPosition, float angle)
        {
            var missile = GetEnemyMissile();
            missile.SetPosition(initialPosition);
            missile.SetAngle(angle);
            missile.Initialize();
            missile.Shoot();
        }

        public void ShootEnemyMissile(Vector2 initialPosition, float angle, float speed)
        {
            var missile = GetEnemyMissile();
            missile.SetPosition(initialPosition);
            missile.SetAngle(angle);
            missile.SetSpeed(speed);
            missile.Initialize();
            missile.Shoot();
        }

        public void ShootEnemyGuidedMissile(Vector2 initialPosition, Transform target)
        {
            var guidedMissile = GetEnemyMissile() as EnemyGuidedMissile;
            guidedMissile.SetPosition(initialPosition);
            guidedMissile.Initialize();
            guidedMissile.InitializeTarget(target);
            guidedMissile.Shoot();
        }
    }
}

