namespace SpaceShooterProject.AI.Enemies
{
    using SpaceShooterProject.AI.Movements;
    using System;
    public class WaveRoadTracker : RoadTracker
    {

        public override void OnInit()
        {
            movement = new WaveMovement();
        }
    }
}

