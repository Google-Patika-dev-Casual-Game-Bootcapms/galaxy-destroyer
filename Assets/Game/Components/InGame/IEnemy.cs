namespace SpaceShooterProject.Component
{ 
    using UnityEngine;
    using System.Collections;
    using Devkit.Base.Object;
    using Devkit.Base.Pattern.ObjectPool;

    public interface IEnemy: IUpdatable, IInitializable, IDestructible, IPoolable
    {
        void Patrol();
        void Attack();
        void GetHit(int damage);
        void Death();
        void OutOfScreen();
    }


}