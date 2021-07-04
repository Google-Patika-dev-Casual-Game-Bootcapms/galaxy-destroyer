namespace SpaceShooterProject.AI.State
{
    using System.Collections;
    using System.Collections.Generic;
    using SpaceShooterProject.AI.Enemies;
    using UnityEngine;
    using Devkit.HSM;

    public class RoadTrackerMainState : StateMachine
    {

        private RoadTrackerActionState roadTrackerActionState;
        private RoadTrackerDeathState roadTrackerDeathState;
        private IRoadTracker roadTracker;
        private RoadTrackerEventContainer roadTrackerEventContainer;

        public RoadTrackerMainState(IRoadTracker roadTracker, RoadTrackerEventContainer roadTrackerEventContainer)
        {
            Debug.Log("Main state");
            this.roadTracker = roadTracker;
            this.roadTrackerEventContainer = roadTrackerEventContainer;


            roadTrackerActionState = new RoadTrackerActionState(roadTracker, roadTrackerEventContainer);
            roadTrackerDeathState = new RoadTrackerDeathState(roadTracker, roadTrackerEventContainer);

            AddSubState(roadTrackerActionState);
            AddSubState(roadTrackerDeathState);

            AddTransition(roadTrackerActionState, roadTrackerDeathState, (int)RoadTrackerState.DEATH_STATE);
        }

        protected override void OnEnter()
        {

        }

        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {

        }
        

        
    }

    public enum RoadTrackerState
    {
        DEATH_STATE
    }

}

