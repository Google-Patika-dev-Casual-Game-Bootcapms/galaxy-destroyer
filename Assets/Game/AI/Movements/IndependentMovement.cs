namespace SpaceShooterProject.AI.Movements
{
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class IndependentMovement : PathMovement
    {
        public override void Initialize(Enemy minion)
        {
            currentRouteIndex = 0;
            tParam = 0f;
            couroutineAllowed = true;


        }

    }
}

