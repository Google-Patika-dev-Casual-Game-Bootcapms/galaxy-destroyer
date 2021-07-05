namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Component;
    using UserInterface;
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using System;

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
            endGameCanvas.OnMainMenuButtonClick += OnMainMenuButtonClick;
            endGameCanvas.OnSettingsButtonClick += OnSettingsButtonClick;
            
        }

        protected override void OnExit()
        {
            endGameCanvas.OnRestartButtonClick -= OnRestartButtonClick;
            endGameCanvas.OnMainMenuButtonClick -= OnMainMenuButtonClick;
            endGameCanvas.OnSettingsButtonClick -= OnSettingsButtonClick;
        }

        protected override void OnUpdate()
        {
            
        }
        private void OnRestartButtonClick()
        {
            gamePlayComponent.FinishGame();
            SendTrigger((int)StateTriggers.REPLAY_GAME_REQUEST);

        }
        private void OnMainMenuButtonClick()
        {
            gamePlayComponent.FinishGame();
            SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);

        }
        private void OnSettingsButtonClick()
        {
            gamePlayComponent.FinishGame();
            SendTrigger((int)StateTriggers.GO_TO_SETTINGS_REQUEST);
        }
    }
}

