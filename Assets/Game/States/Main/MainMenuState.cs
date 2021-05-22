namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainMenuState : StateMachine
    {
        private ComponentContainer componentContainer;

        public MainMenuState(ComponentContainer componentContainer)
        {
            this.componentContainer = componentContainer;
        }

        protected override void OnEnter()
        {
            Debug.Log("MainMenuState OnEnter");
        }

        protected override void OnExit()
        {
            Debug.Log("MainMenuState OnExit");
        }

        protected override void OnUpdate()
        {
            Debug.Log("MainMenuState OnUpdate");
        }
    }
}



