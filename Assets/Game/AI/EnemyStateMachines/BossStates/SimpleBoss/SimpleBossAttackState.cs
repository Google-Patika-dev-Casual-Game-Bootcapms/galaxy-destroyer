namespace SpaceShooterProject.AI.State
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SimpleBossAttackState : StateMachine
    {
        private ISimpleBoss simpleBoss;
        private SimpleBossEventContainer simpleBossEventContainer;

        public SimpleBossAttackState(ISimpleBoss simpleBoss, SimpleBossEventContainer simpleBossEventContainer)
        {
            this.simpleBoss = simpleBoss;
            this.simpleBossEventContainer = simpleBossEventContainer;
        }

        protected override void OnEnter()
        {
            Debug.Log("In Attack State");
            simpleBossEventContainer.TriggerEnterTheAttackState();
        }

        protected override void OnExit()
        {
            simpleBossEventContainer.TriggerExitFromAttackState();
        }

        protected override void OnUpdate()
        {
            if (simpleBoss.IsShootingSessionEnd())
            {
                SendTrigger((int)SimpleBossState.PATROL_STATE);
            }
        }
    }
}
