namespace SpaceShooterProject.AI.Projectiles
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.Base.Pattern.ObjectPool;
    using SpaceShooterProject.Component;
    using UnityEngine;

    public abstract class EnemyMissile : MonoBehaviour, IEnemyMissile, IPoolable
    {
        [SerializeField] protected float missileSpeed = 3f;
        [SerializeField] protected float directionAngle = 270f;
        [SerializeField] protected RectTransform _transform;

        public abstract void Activate();
        public abstract void Deactivate();
        public abstract void Initialize();
        public abstract void InjectBulletCollector(BulletCollector bulletCollector);

        public abstract void InjectEnemyMissileCollector(IMissileCollector enemyMissileCollector);

        public abstract void Shoot();

        public void SetPosition(Vector2 initialPosition)
        {
            _transform.position = initialPosition;
        }

        public void SetSpeed(float speed)
        {
            this.missileSpeed = speed;
        }

        public void SetAngle(float angle)
        {
            this.directionAngle = angle;
        }
    }
}
