namespace SpaceShooterProject.AI.Projectiles
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IEnemyGuidedMissile
    {
        public void Shoot();
        public void InitializeTarget(Transform target);
        
    }

}
