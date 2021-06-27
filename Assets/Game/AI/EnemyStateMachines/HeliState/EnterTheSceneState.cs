namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EnterTheSceneState : StateMachine
    {
        private IHelicopter helicopter;
        private HelicopterEventContainer helicopterEventContainer;

        public EnterTheSceneState(IHelicopter helicopter, HelicopterEventContainer helicopterEventContainer) 
        {
            this.helicopter = helicopter;
            this.helicopterEventContainer = helicopterEventContainer;
        }

        protected override void OnEnter()
        {
            helicopterEventContainer.TriggerEnterTheScene();
        }

        protected override void OnExit()
        {
            helicopterEventContainer.TriggerExitFromScene();
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

