namespace SpaceShooterProject.State
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class InGameState : StateMachine
    {
        private UIComponent uiComponent;
        private GamePlayComponent gamePlayComponent;
        private InGameCanvas inGameCanvas;

        public InGameState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            gamePlayComponent = componentContainer.GetComponent("GamePlayComponent") as GamePlayComponent;
            inGameCanvas = uiComponent.GetCanvas(UIComponent.MenuName.IN_GAME) as InGameCanvas;
        }

        protected override void OnEnter()
        {
            Debug.Log("IngameState on Enter");
            uiComponent.EnableCanvas(UIComponent.MenuName.IN_GAME);
            gamePlayComponent.Player.ShowShip();
            gamePlayComponent.GameCamera.IsAvailable = true;
        }

        protected override void OnExit()
        {
            Debug.Log("IngameState on Exit");
            gamePlayComponent.Player.HideShip();
            gamePlayComponent.GameCamera.IsAvailable = false;
        }

        protected override void OnUpdate()
        {
            gamePlayComponent.CallUpdate();
        }
    }
}