namespace SpaceShooterProject.AI.Enemies
{
    using System;
    using SpaceShooterProject.AI.Movements;
    public class StraightRoadTracker : RoadTracker
    {
        public override void OnInitialize()
        {
            movement = new StraightDownMovement();
        }
    }
}

