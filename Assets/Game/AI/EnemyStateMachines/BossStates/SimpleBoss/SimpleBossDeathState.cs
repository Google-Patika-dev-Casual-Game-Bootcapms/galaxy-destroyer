namespace SpaceShooterProject.AI.State
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;



    public class SimpleBossDeathState : StateMachine
    {
        private ISimpleBoss simpleBoss;
        private SimpleBossEventContainer simpleBossEventContainer;

        public SimpleBossDeathState(ISimpleBoss simpleBoss, SimpleBossEventContainer simpleBossEventContainer)
        {
            this.simpleBoss = simpleBoss;
            this.simpleBossEventContainer = simpleBossEventContainer;
        }

        protected override void OnEnter()
        {
            simpleBossEventContainer.TriggerEnterTheDeathState();
        }

        protected override void OnExit()
        {
            simpleBossEventContainer.TriggerExitFromDeathState();
        }

        protected override void OnUpdate()
        {

        }
    }
}
