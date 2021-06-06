namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class CreditsState : StateMachine
    {
        private UIComponent uiComponent;
        private CreditsCanvas creditsCanvas;

        public CreditsState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            creditsCanvas = uiComponent.GetCanvas(UIComponent.MenuName.CREDITS) as CreditsCanvas;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.CREDITS);
            creditsCanvas.OnReturnToMainMenu += OnReturnToMainMenu;
        }

        private void OnReturnToMainMenu()
        {
            SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        protected override void OnExit()
        {
            creditsCanvas.OnReturnToMainMenu -= OnReturnToMainMenu;
        }

        protected override void OnUpdate()
        {

        }
    }

}

