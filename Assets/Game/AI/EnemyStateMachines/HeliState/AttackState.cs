namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AttackState : StateMachine
    {
        private IHelicopter helicopter;

        public AttackState(IHelicopter helicopter)
        {
            this.helicopter = helicopter;
        }

        protected override void OnEnter()
        {
            
        }

        protected override void OnExit()
        {
            
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


