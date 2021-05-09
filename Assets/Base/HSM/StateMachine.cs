using System.Collections;
using System.Collections.Generic;


namespace Devkit.HSM 
{
    using System;
    using UnityEngine;

    public abstract class StateMachine
    {
        private Dictionary<Type, StateMachine> subStates = new Dictionary<Type, StateMachine>();
        private Dictionary<int, StateMachine> transitions = new Dictionary<int, StateMachine>();

        private StateMachine parent;
        private StateMachine defaultSubState;
        private StateMachine currentSubState;

        protected abstract void OnEnter();
        protected abstract void OnUpdate();
        protected abstract void OnExit();

        public void Enter() 
        {
            OnEnter();

            if (currentSubState == null && defaultSubState != null) 
            {
                currentSubState = defaultSubState;
            }

            if(currentSubState != null) 
            {
                currentSubState.Enter();
            }
        } 

        public void Update() 
        {
            OnUpdate();

            if (currentSubState != null) 
            {
                currentSubState.Update();
            }
        }

        public void Exit() 
        {
            if (currentSubState != null) 
            {
                currentSubState.Exit();
            }

            OnExit();
        }

        public void AddTransition(StateMachine sourceStateMachine, StateMachine targetStateMachine, int trigger) 
        {
            if (sourceStateMachine.transitions.ContainsKey(trigger)) 
            {
                Debug.LogWarning("Duplicated transition! : " + trigger);
                return;
            }

            sourceStateMachine.transitions.Add(trigger, targetStateMachine);
        }

        public void AddSubState(StateMachine subState)
        {
            if (subStates.Count == 0)
            {
                defaultSubState = subState;
            }

            subState.parent = this;

            if (subStates.ContainsKey(subState.GetType())) 
            {
                Debug.LogWarning("Duplicated sub state : " + subState.GetType());
                return;
            }

            subStates.Add(subState.GetType(), subState);

        }

        public void SendTrigger(int trigger)
        {
            var root = this;
            while (root?.parent != null)
            {
                root = root.parent;
            }

            while (root != null)
            {
                if (root.transitions.TryGetValue(trigger, out StateMachine toState))
                {
                    root.parent?.ChangeSubState(toState);
                    return;
                }

                root = root.currentSubState;
            }
        }

        private void ChangeSubState(StateMachine state)
        {
            if (currentSubState != null) 
            {
                currentSubState.Exit();
            }
            
            var nextState = subStates[state.GetType()];
            currentSubState = nextState;
            nextState.Enter();
        }

    }
}


