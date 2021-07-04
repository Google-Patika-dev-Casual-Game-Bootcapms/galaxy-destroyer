namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Component;
    using UserInterface;
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class EndGameState : StateMachine
    {
        private UIComponent uiComponent;
        private EndGameCanvas endGameCanvas; 
        private GamePlayComponent gamePlayComponent;

        public EndGameState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            gamePlayComponent = componentContainer.GetComponent("GamePlayComponent") as GamePlayComponent;
            endGameCanvas = uiComponent.GetCanvas(UIComponent.MenuName.END_GAME) as EndGameCanvas;

        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.END_GAME);
            endGameCanvas.OnRestartButtonClick += OnRestartButtonClick;
            
        }

        protected override void OnExit()
        {
            endGameCanvas.OnRestartButtonClick -= OnRestartButtonClick;
        }

        protected override void OnUpdate()
        {
            
        }
        private void OnRestartButtonClick()
        {
            gamePlayComponent.FinishGame();
            SendTrigger((int)StateTriggers.REPLAY_GAME_REQUEST);

        }
    }
}

