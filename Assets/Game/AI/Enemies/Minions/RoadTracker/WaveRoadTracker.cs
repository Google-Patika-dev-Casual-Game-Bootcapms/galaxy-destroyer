namespace SpaceShooterProject.AI.Enemies
{
    using SpaceShooterProject.AI.Movements;
    using System;
    public class WaveRoadTracker : RoadTracker
    {

        public override void OnInitialize()
        {
            movement = new WaveMovement();
        }
    }
}

