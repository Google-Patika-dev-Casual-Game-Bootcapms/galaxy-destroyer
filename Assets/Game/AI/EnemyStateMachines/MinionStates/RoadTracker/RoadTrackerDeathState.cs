namespace SpaceShooterProject.AI.State
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;

    public class RoadTrackerDeathState : StateMachine
    {
        private IRoadTracker roadTracker;
        private RoadTrackerEventContainer roadTrackerEventContainer;

        public RoadTrackerDeathState(IRoadTracker roadTracker, RoadTrackerEventContainer roadTrackerEventContainer)
        {
            this.roadTracker = roadTracker;
            this.roadTrackerEventContainer = roadTrackerEventContainer;
        }

        protected override void OnEnter()
        {
            Debug.Log("Enter death state");
            roadTrackerEventContainer.TriggerEnterTheDeathState();
        }

        protected override void OnExit()
        {
            roadTrackerEventContainer.TriggerExitFromDeathState();
        }

        protected override void OnUpdate()
        {
            
        }
    }
}

