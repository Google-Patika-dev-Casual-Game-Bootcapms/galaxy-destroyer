namespace SpaceShooterProject.AI.State
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SimpleBossPatrolState : StateMachine
    {
        private ISimpleBoss simpleBoss;
        private SimpleBossEventContainer simpleBossEventContainer;

        public SimpleBossPatrolState(ISimpleBoss simpleBoss, SimpleBossEventContainer simpleBossEventContainer)
        {
            this.simpleBoss = simpleBoss;
            this.simpleBossEventContainer = simpleBossEventContainer;
        }

        protected override void OnEnter()
        {
            simpleBossEventContainer.TriggerEnterThePatrolState();
        }

        protected override void OnExit()
        {
            simpleBossEventContainer.TriggerExitFromPatrolState();
        }

        protected override void OnUpdate()
        {
            if (simpleBoss.IsPatrolTimeFinished())
            {
                SendTrigger((int)SimpleBossState.ATTACK_STATE);
            }
        }
    }
}
