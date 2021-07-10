namespace SpaceShooterProject.AI.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.Base.Object;
    using Devkit.Base.Pattern.ObjectPool;
    using UnityEngine;

    public interface IEnemy : IUpdatable, IInitializable, IDestructible, IPoolable
    {
        public void OnUpdate();
        void GetHit(int damage);
    }
}


