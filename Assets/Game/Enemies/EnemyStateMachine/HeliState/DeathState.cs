namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class DeathState : StateMachine
    {
        private IHelicopter helicopter;
        private HelicopterEventContainer helicopterEventContainer;

        public DeathState(IHelicopter helicopter, HelicopterEventContainer helicopterEventContainer)
        {
            this.helicopter = helicopter;
            this.helicopterEventContainer = helicopterEventContainer;
        }

        protected override void OnEnter()
        {
            helicopterEventContainer.TriggerEnterTheDeathState();
        }

        protected override void OnExit()
        {
            helicopterEventContainer.TriggerExitFromDeathState();
        }

        protected override void OnUpdate()
        {
            
        }
    }

}
