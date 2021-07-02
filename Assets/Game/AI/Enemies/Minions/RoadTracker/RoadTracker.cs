namespace SpaceShooterProject.AI.Enemies
{
    using SpaceShooterProject.AI.Movements;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class RoadTracker : Minion
    {
        [SerializeField]
        private float pathLength;

        public float GetPathLength()
        {
            return pathLength;
        }

        public override bool IsMovementContinue()
        {
            return false;
        }

        public override bool IsMovementInterrupted()
        {
            return false;
        }

        public override void RouteFinished()
        {
            
        }
    }

}
