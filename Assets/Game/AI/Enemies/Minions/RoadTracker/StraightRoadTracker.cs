namespace SpaceShooterProject.AI.Enemies
{
    using System;
    using SpaceShooterProject.AI.Movements;
    public class StraightRoadTracker : RoadTracker
    {
        public override void OnInit()
        {
            movement = new StraightDownMovement();
        }
    }
}

