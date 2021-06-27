namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PatrolState : StateMachine
    {
        private IHelicopter helicopter;

        public PatrolState(IHelicopter helicopter)
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
            if (helicopter.IsPatrolTimeFinished()) 
            {
                SendTrigger((int)HelicopterState.ATTACK_STATE);
            }
        }
    }
}


