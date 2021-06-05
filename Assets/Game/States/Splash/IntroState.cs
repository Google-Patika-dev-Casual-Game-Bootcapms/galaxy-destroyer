namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using UnityEngine;

    public class IntroState : StateMachine
    {
        private IntroComponent introComponent;

        public IntroState(ComponentContainer componentContainer)
        {
            this.introComponent = componentContainer.GetComponent("IntroComponent") as IntroComponent;
        }

        protected override void OnEnter()
        {
            Debug.Log("Intro state On Enter");
            introComponent.StartIntro();
        }

        protected override void OnExit()
        {
            Debug.Log("Intro state On Exit");
        }

        protected override void OnUpdate()
        {
            Debug.Log("Intro state On Update");
            
            if (introComponent.IsIntroAnimationCompleted()) 
            {
                SendTrigger((int)StateTriggers.SPLASH_LOADING);
            }
            
        }
    }

}

