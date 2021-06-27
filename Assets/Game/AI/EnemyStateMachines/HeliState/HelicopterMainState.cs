namespace SpaceShooterProject.AI.State 
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class HelicopterMainState : StateMachine
    {
        private EnterTheSceneState enterTheSceneState;
        private ActionState actionState;
        private DeathState deathState;

        public HelicopterMainState(IHelicopter helicopter, HelicopterEventContainer helicopterEventContainer) 
        {
            enterTheSceneState = new EnterTheSceneState(helicopter, helicopterEventContainer);
            actionState = new ActionState(helicopter, helicopterEventContainer);
            deathState = new DeathState(helicopter, helicopterEventContainer);

            AddSubState(enterTheSceneState);
            AddSubState(actionState);
            AddSubState(deathState);

            AddTransition(enterTheSceneState, actionState, (int)HelicopterState.ACTION_STATE);
            AddTransition(actionState, deathState, (int)HelicopterState.DEATH_STATE);
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

    public enum HelicopterState
    {
        ACTION_STATE,
        ATTACK_STATE,
        PATROL_STATE,
        DEATH_STATE
    }
}


