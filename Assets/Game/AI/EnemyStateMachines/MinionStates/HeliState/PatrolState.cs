namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PatrolState : StateMachine
    {
        private IHelicopter helicopter;
        private HelicopterEventContainer helicopterEventContainer;

        public PatrolState(IHelicopter helicopter, HelicopterEventContainer helicopterEventContainer)
        {
            this.helicopter = helicopter;
            this.helicopterEventContainer = helicopterEventContainer;
        }

        protected override void OnEnter()
        {
            Debug.Log("In Patrol State");
            helicopterEventContainer.TriggerEnterThePatrolState();
        }

        protected override void OnExit()
        {
            helicopterEventContainer.TriggerExitFromPatrolState();
        }

        protected override void OnUpdate()
        {
            if (helicopter.IsPatrolTimeFinished()) 
            {
                SendTrigger((int)HelicopterState.ATTACK_STATE);
            }
        }
    }
}


