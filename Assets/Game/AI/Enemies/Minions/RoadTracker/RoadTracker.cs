namespace SpaceShooterProject.AI.Enemies
{
    using SpaceShooterProject.AI.Movements;
    using SpaceShooterProject.AI.State;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public abstract class RoadTracker : Minion, IRoadTracker
    {
        private RoadTrackerEventContainer roadTrackerEventContainer;
        public RoadTrackerMainState roadTrackerMainState;

        private bool isDeath = false;

        public abstract void OnInit();

        public override void OnInitialize()
        {
            roadTrackerEventContainer = new RoadTrackerEventContainer();
            roadTrackerMainState = new RoadTrackerMainState(this, roadTrackerEventContainer);
            InitializeEvents();
            OnInit();
        }


        public override void EnterMainState()
        {
            // TODO: If you call destroy events call initialize events here
            roadTrackerMainState.SetDefaultState();
            roadTrackerMainState.Enter();

        }

        public override void OnUpdate()
        {
            roadTrackerMainState.Update();
        }

        private void InitializeEvents()
        {
            roadTrackerEventContainer.OnActionStateEnter += OnActionStateEnter;
            roadTrackerEventContainer.OnActionStateExit += OnActionStateExit;
            roadTrackerEventContainer.OnDeathStateEnter += OnDeathStateEnter;
            roadTrackerEventContainer.OnDeathStateExit += OnDeathStateExit;
        }

        private void DestroyEvents()
        {
            roadTrackerEventContainer.OnActionStateEnter -= OnActionStateEnter;
            roadTrackerEventContainer.OnActionStateExit -= OnActionStateExit;
            roadTrackerEventContainer.OnDeathStateEnter -= OnDeathStateEnter;
            roadTrackerEventContainer.OnDeathStateExit -= OnDeathStateExit;
        }

        public void OnActionStateEnter()
        {
            Movement();
        }

        public void OnActionStateExit()
        {

        }

        public void OnDeathStateEnter()
        {
            inGameMessageBroadcaster.TriggerEnemyDeath(this);
        }

        public void OnDeathStateExit()
        {

        }


        public bool IsDeath()
        {
            return isDeath;
        }

        public override bool IsMovementContinue()
        {
            return false;
        }

        public override bool IsMovementInterrupted()
        {
            return false;
        }

        public override void OnOutOfScreen()
        {
            isDeath = true;
            inGameMessageBroadcaster.TriggerEnemyOutOfScreen(this);
        }

        public override void RouteFinished()
        {
            
        }

        public override void PatrolRouteFinished()
        {

        }
    }


}
