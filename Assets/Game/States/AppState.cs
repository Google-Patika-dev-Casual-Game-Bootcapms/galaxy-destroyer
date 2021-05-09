
namespace SpaceShooterProject.State 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Devkit.HSM;
    using Devkit.Base.Component;

    public class AppState : StateMachine
    {
        private SplashState splashState;

        public AppState(ComponentContainer componentContainer) 
        {
            splashState = new SplashState(componentContainer);
            this.AddSubState(splashState);
            splashState.Enter();
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


