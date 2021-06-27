namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ActionState : StateMachine
    {
        private AttackState attackState;
        private PatrolState patrolState;
        private IHelicopter helicopter;

        public ActionState(IHelicopter helicopter, HelicopterEventContainer helicopterEventContainer) 
        {
            attackState = new AttackState(helicopter, helicopterEventContainer);
            patrolState = new PatrolState(helicopter, helicopterEventContainer);

            AddSubState(patrolState);
            AddSubState(attackState);

            AddTransition(patrolState, attackState, (int)HelicopterState.ATTACK_STATE);
            AddTransition(attackState, patrolState, (int)HelicopterState.PATROL_STATE);
        }

        protected override void OnEnter()
        {
            
        }

        protected override void OnExit()
        {
            
        }

        protected override void OnUpdate()
        {
            if (helicopter.IsDeath())
            {
                SendTrigger((int)HelicopterState.DEATH_STATE);
            }
        }
    }
}


