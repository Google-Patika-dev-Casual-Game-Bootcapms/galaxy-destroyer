namespace SpaceShooterProject.AI.State
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using UnityEngine;

    public class RoadTrackerActionState : StateMachine
    {
        private IRoadTracker roadTracker;
        private RoadTrackerEventContainer roadTrackerEventContainer;

        public RoadTrackerActionState(IRoadTracker roadTracker, RoadTrackerEventContainer roadTrackerEventContainer)
        {
            this.roadTracker = roadTracker;
            this.roadTrackerEventContainer = roadTrackerEventContainer;
        }

        protected override void OnEnter()
        {
            Debug.Log("Action state");
            roadTrackerEventContainer.TriggerEnterActionState();
        }

        protected override void OnExit()
        {
            roadTrackerEventContainer.TriggerExitFromActionState();
        }

        protected override void OnUpdate()
        {
            if (roadTracker.IsDeath())
            {
                SendTrigger((int)RoadTrackerState.DEATH_STATE);
            }
            
        }
    }
}

