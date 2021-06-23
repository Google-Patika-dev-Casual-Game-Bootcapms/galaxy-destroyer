namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GameState : StateMachine
    {
        private InGameState inGameState;
        private PrepareGameState prepareGameState;
        private PauseGameState pauseGameState;
        private EndGameState endGameState;

        private UIComponent uiComponent;
        private ProvisionCanvas provisionCanvas;
        private InGameCanvas inGameCanvas;
        private GamePlayComponent gamePlayComponent;

        public GameState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            inGameCanvas = uiComponent.GetCanvas(UIComponent.MenuName.IN_GAME) as InGameCanvas; 
            provisionCanvas = uiComponent.GetCanvas(UIComponent.MenuName.PROVISION) as ProvisionCanvas; 
             gamePlayComponent=componentContainer.GetComponent("GamePlayComponent") as GamePlayComponent;
             
            prepareGameState = new PrepareGameState(componentContainer);
            inGameState = new InGameState(componentContainer);
            pauseGameState = new PauseGameState(componentContainer);
            endGameState = new EndGameState(componentContainer);

            AddSubState(prepareGameState);
            AddSubState(inGameState);
            AddSubState(pauseGameState);
            AddSubState(endGameState);
            
            AddTransition(prepareGameState, inGameState, (int)StateTriggers.PLAY_GAME_REQUEST);
            AddTransition(inGameState, pauseGameState, (int)StateTriggers.PAUSE_GAME_REQUEST);
            AddTransition(pauseGameState, inGameState, (int)StateTriggers.RESUME_GAME_REQUEST);
            AddTransition(inGameState, endGameState, (int)StateTriggers.GAME_OVER);
            AddTransition(endGameState, prepareGameState, (int)StateTriggers.REPLAY_GAME_REQUEST);
            AddTransition(prepareGameState, pauseGameState, (int)StateTriggers.PAUSE_GAME_REQUEST);

        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.PROVISION);
        }

        private void ReturnToMainMenu()
        {
            SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        protected override void OnExit()
        {
            provisionCanvas.OnReturnToMainMenu -= ReturnToMainMenu;
        }

        protected override void OnUpdate()
        {
            Debug.Log("GameState in on ");
            gamePlayComponent.CallUpdate();
        }
    }
}
