namespace SpaceShooterProject.AI.Projectiles
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.Base.Pattern.ObjectPool;
    using SpaceShooterProject.Component;
    using UnityEngine;

    public class EnemyGuidedMissile : EnemyMissile, IEnemyGuidedMissile
    {
        private Camera mainCamera;
        private IMissileCollector missileCollector;

        private Transform target;

        public override void Activate()
        {
            gameObject.SetActive(true);
        }

        public override void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public override void Initialize()
        {
            mainCamera = Camera.main;
        }

        public void InitializeTarget(Transform target)
        {
            this.target = target;
        }

        public override void InjectBulletCollector(BulletCollector bulletCollector)
        {
            
        }

        public override void Shoot()
        {
            StartCoroutine(Move());
        }
        

        private IEnumerator Move()
        {
            Vector2 translationVector;
            do
            {
                //Calculating in all frames
                translationVector = missileSpeed * (target.position - _transform.position).normalized;

                _transform.Translate(translationVector);
                yield return new WaitForEndOfFrame();

            } while (!IsOutOfScreen());

            //Out of screen
            missileCollector.AddMissileToPool(this);
            yield return null;
        }

        private bool IsOutOfScreen()
        {
            Vector2 normalizedPosition = mainCamera.WorldToViewportPoint(_transform.position);
            return (normalizedPosition.x < 0 || normalizedPosition.x > 1) ||
                (normalizedPosition.y < 0 || normalizedPosition.y > 1);
        }

        public override void InjectEnemyMissileCollector(IMissileCollector enemyMissileCollector)
        {
            this.missileCollector = enemyMissileCollector;
        }
    }
}

