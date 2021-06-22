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
        private InGameCanvas inGameCanvas;

        public InGameState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            inGameCanvas = uiComponent.GetCanvas(UIComponent.MenuName.IN_GAME) as InGameCanvas;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.IN_GAME);
        }

        protected override void OnExit()
        {
            
        }

        protected override void OnUpdate()
        {
            
        }
    }
}


