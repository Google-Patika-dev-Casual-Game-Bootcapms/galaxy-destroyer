namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using UnityEngine;

    public class SplashState : StateMachine
    {
        private LoadingState loadingState;
        private IntroState introState;

        public SplashState(ComponentContainer componentContainer) 
        {
            loadingState = new LoadingState();
            introState = new IntroState(componentContainer);

            this.AddSubState(introState);
            this.AddSubState(loadingState);

            this.AddTransition(introState, loadingState, (int)StateTriggers.SPLASH_LOADING);
        }

        protected override void OnEnter()
        {
            Debug.Log("Splash State OnEnter");
        }

        protected override void OnExit()
        {
            Debug.Log("Splash State OnExit");
        }

        protected override void OnUpdate()
        {
            Debug.Log("Splash State OnUpdate");
        }
    }
}


