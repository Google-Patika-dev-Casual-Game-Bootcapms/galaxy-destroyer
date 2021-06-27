namespace SpaceShooterProject.Component
{ 
    using UnityEngine;
    using System.Collections;

    public class FlameThrower : Enemy
    {
        public override void Attack()
        {
        }

        public override void Death()
        {
        }

        public override void GetHit(int damage)
        {
            HP -= damage;
        }

        public override void OutOfScreen()
        {
        }

        public override void Patrol()
        {
        }
    }
}
