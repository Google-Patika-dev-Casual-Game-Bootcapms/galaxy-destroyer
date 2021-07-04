namespace SpaceShooterProject.AI.State
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SimpleBossActionState : StateMachine
    {
        private SimpleBossAttackState simpleBossAttackState;
        private SimpleBossPatrolState simpleBossPatrolState;
        private ISimpleBoss simpleBoss;

        public SimpleBossActionState(ISimpleBoss simpleBoss, SimpleBossEventContainer simpleBossEventContainer)
        {
            this.simpleBoss = simpleBoss;

            simpleBossAttackState = new SimpleBossAttackState(simpleBoss, simpleBossEventContainer);
            simpleBossPatrolState = new SimpleBossPatrolState(simpleBoss, simpleBossEventContainer);

            AddSubState(simpleBossAttackState);
            AddSubState(simpleBossPatrolState);
            

            AddTransition(simpleBossPatrolState, simpleBossAttackState, (int)SimpleBossState.ATTACK_STATE);
            AddTransition(simpleBossAttackState, simpleBossPatrolState, (int)SimpleBossState.PATROL_STATE);
        }

        protected override void OnEnter()
        {

        }

        protected override void OnExit()
        {

        }

        protected override void OnUpdate()
        {
            if (simpleBoss.IsDeath())
            {
                SendTrigger((int)SimpleBossState.DEATH_STATE);
            }
        }
    }
}