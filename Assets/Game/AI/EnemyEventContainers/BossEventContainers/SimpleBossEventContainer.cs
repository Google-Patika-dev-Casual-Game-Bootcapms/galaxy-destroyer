namespace SpaceShooterProject.AI
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class SimpleBossEventContainer
    {
        public delegate void MessageDelegate();

        public event MessageDelegate OnEnterSceneStateEnter;
        public event MessageDelegate OnEnterSceneStateExit;
        public event MessageDelegate OnAttackStateEnter;
        public event MessageDelegate OnAttackStateExit;
        public event MessageDelegate OnPatrolStateEnter;
        public event MessageDelegate OnPatrolStateExit;
        public event MessageDelegate OnDeathStateEnter;
        public event MessageDelegate OnDeathStateExit;

        public void TriggerEnterTheDeathState()
        {
            if (OnDeathStateEnter != null)
            {
                OnDeathStateEnter();
            }
        }

        public void TriggerExitFromDeathState()
        {
            if (OnDeathStateExit != null)
            {
                OnDeathStateExit();
            }
        }

        public void TriggerEnterTheScene()
        {

            if (OnEnterSceneStateEnter != null)
            {
                OnEnterSceneStateEnter();
            }
        }

        public void TriggerExitFromScene()
        {
            if (OnEnterSceneStateExit != null)
            {
                OnEnterSceneStateExit();
            }
        }

        public void TriggerEnterTheAttackState()
        {
            if (OnAttackStateEnter != null)
            {
                OnAttackStateEnter();
            }
        }

        public void TriggerExitFromAttackState()
        {
            if (OnAttackStateExit != null)
            {
                OnAttackStateExit();
            }
        }

        public void TriggerEnterThePatrolState()
        {
            if (OnPatrolStateEnter != null)
            {
                OnPatrolStateEnter();
            }
        }

        public void TriggerExitFromPatrolState()
        {
            if (OnPatrolStateExit != null)
            {
                OnPatrolStateExit();
            }
        }
    }
}