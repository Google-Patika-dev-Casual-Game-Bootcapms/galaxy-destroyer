namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnterTheSceneState : StateMachine
    {
        private IHelicopter helicopter;

        public EnterTheSceneState(IHelicopter helicopter, HelicopterEventContainer helicopterEventContainer) 
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
            if (helicopter.IsEnterTheSceneAnimationFinish()) 
            {
                SendTrigger((int)HelicopterState.ACTION_STATE);
            }
        }
    }
}

