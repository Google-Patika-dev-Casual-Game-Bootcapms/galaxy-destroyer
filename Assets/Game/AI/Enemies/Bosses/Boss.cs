namespace SpaceShooterProject.AI.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class Boss : Enemy
    {
        protected void BossPatrol()
        {
            movement.BossPatrol(this);
        }
    }
}