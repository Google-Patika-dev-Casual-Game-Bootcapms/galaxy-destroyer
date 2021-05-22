
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
        private MainState mainState;
        private GameState gameState;

        public AppState(ComponentContainer componentContainer) 
        {
            splashState = new SplashState(componentContainer);
            mainState = new MainState(componentContainer);
            gameState = new GameState(componentContainer);

            this.AddSubState(splashState);
            this.AddSubState(mainState);
            this.AddSubState(gameState);

            this.AddTransition(splashState, mainState, (int)StateTriggers.SPLASH_COMPLETED);
            this.AddTransition(mainState, gameState, (int)StateTriggers.START_GAME_REQUEST);
            this.AddTransition(gameState, mainState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        protected override void OnEnter()
        {
            Debug.Log("AppState OnEnter");
        }

        protected override void OnExit()
        {
            Debug.Log("AppState OnExit");
        }

        protected override void OnUpdate()
        {
            Debug.Log("AppState Update");
        }
    }

}


