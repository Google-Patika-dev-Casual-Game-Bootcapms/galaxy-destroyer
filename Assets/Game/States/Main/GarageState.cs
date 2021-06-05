namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class GarageState : StateMachine
    {
        private UIComponent uiComponent;
        private GarageCanvas garageCanvas;

        public GarageState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            garageCanvas = uiComponent.GetCanvas(UIComponent.MenuName.GARAGE) as GarageCanvas;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.GARAGE);
            garageCanvas.OnReturnToMainMenu += OnReturnToMainMenu;
        }

        private void OnReturnToMainMenu()
        {
            SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        protected override void OnExit()
        {
            garageCanvas.OnReturnToMainMenu -= OnReturnToMainMenu;
        }

        protected override void OnUpdate()
        {
            
        }
    }

}

