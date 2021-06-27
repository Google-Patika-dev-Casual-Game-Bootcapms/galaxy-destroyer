namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AttackState : StateMachine
    {
        private IHelicopter helicopter;
        private HelicopterEventContainer helicopterEventContainer;

        public AttackState(IHelicopter helicopter, HelicopterEventContainer helicopterEventContainer)
        {
            this.helicopter = helicopter;
            this.helicopterEventContainer = helicopterEventContainer;
        }

        protected override void OnEnter()
        {
            helicopterEventContainer.TriggerEnterTheAttackState();
        }

        protected override void OnExit()
        {
            helicopterEventContainer.TriggerExitFromAttackState();
        }

        protected override void OnUpdate()
        {
            if (helicopter.IsShootingSessionEnd()) 
            {
                SendTrigger((int)HelicopterState.PATROL_STATE);
            }
        }
    }
}


