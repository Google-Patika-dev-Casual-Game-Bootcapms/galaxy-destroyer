namespace SpaceShooterProject.AI.State
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SimpleBossMainState : StateMachine
    {
        private SimpleBossEnterSceneState simpleBossEnterSceneState;
        private SimpleBossActionState simpleBossActionState;
        private SimpleBossDeathState simpleBossDeathState;
        private ISimpleBoss simpleBoss;
        private SimpleBossEventContainer simpleBossEventContainer;

        public SimpleBossMainState(ISimpleBoss simpleBoss, SimpleBossEventContainer simpleBossEventContainer) 
        {
            Debug.Log("Main state");
            this.simpleBoss = simpleBoss;
            this.simpleBossEventContainer = simpleBossEventContainer;


            simpleBossEnterSceneState = new SimpleBossEnterSceneState(simpleBoss, simpleBossEventContainer);
            simpleBossActionState = new SimpleBossActionState(simpleBoss, simpleBossEventContainer);
            simpleBossDeathState = new SimpleBossDeathState(simpleBoss, simpleBossEventContainer);

            AddSubState(simpleBossEnterSceneState);
            AddSubState(simpleBossActionState);
            AddSubState(simpleBossDeathState);

            AddTransition(simpleBossEnterSceneState, simpleBossActionState, (int)SimpleBossState.ACTION_STATE);
            AddTransition(simpleBossActionState, simpleBossDeathState, (int)SimpleBossState.DEATH_STATE);
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

    public enum SimpleBossState
    {
        ACTION_STATE,
        ATTACK_STATE,
        PATROL_STATE,
        DEATH_STATE
    }
}

