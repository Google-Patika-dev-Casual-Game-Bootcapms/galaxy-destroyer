namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class ActionState : StateMachine
    {
        private AttackState attackState;
        private PatrolState patrolState;

        public ActionState(IHelicopter helicopter, HelicopterEventContainer helicopterEventContainer) 
        {
            attackState = new AttackState(helicopter);
            patrolState = new PatrolState(helicopter);

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
           
        }
    }
}


