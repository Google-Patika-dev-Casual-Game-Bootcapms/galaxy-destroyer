using System;
using UnityEngine;

namespace SpaceShooterProject.AI 
{
    public class HelicopterEventContainer
    {
        public delegate void MessageDelegate();

        public event MessageDelegate OnEnterTheSceneStateEnter;
        public event MessageDelegate OnEnterTheSceneStateExit;
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

            Debug.Log("trigger enter the scene");
            if (OnEnterTheSceneStateEnter != null) 
            {
                OnEnterTheSceneStateEnter();
            }
        }

        public void TriggerExitFromScene() 
        {
            if (OnEnterTheSceneStateExit != null)
            {
                OnEnterTheSceneStateExit();
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


