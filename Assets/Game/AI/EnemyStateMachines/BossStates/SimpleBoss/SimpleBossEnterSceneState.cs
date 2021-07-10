namespace SpaceShooterProject.AI.State
{
    using Devkit.HSM;
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SimpleBossEnterSceneState : StateMachine
    {
        private ISimpleBoss simpleBoss;
        private SimpleBossEventContainer simpleBossEventContainer;

        public SimpleBossEnterSceneState(ISimpleBoss simpleBoss, SimpleBossEventContainer simpleBossEventContainer)
        {
            this.simpleBoss = simpleBoss;
            this.simpleBossEventContainer = simpleBossEventContainer;
        }


        protected override void OnEnter()
        {
            simpleBossEventContainer.TriggerEnterTheScene();
        }

        protected override void OnExit()
        {
            simpleBossEventContainer.TriggerExitFromScene();
        }

        protected override void OnUpdate()
        {
            if (simpleBoss.IsEnterTheSceneAnimationFinish())
            {
                SendTrigger((int)SimpleBossState.ACTION_STATE);
            }
        }
    }
}
