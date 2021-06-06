using SpaceShooterProject.Component;

namespace SpaceShooterProject.State
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.UserInterface;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PrepareGameState : StateMachine
    {
        private SuperPowerComponent superPowerComponent;
        private UIComponent uiComponent;
        private ProvisionCanvas provisionCanvas;
        public PrepareGameState(ComponentContainer componentContainer)
        {
            superPowerComponent = componentContainer.GetComponent("SuperPowerComponent") as SuperPowerComponent;
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            provisionCanvas = uiComponent.GetCanvas(UIComponent.MenuName.PROVISION) as ProvisionCanvas;
        }

        protected override void OnEnter()
        {
            provisionCanvas.OnSuperPowerLaserRequest += superPowerComponent.UpgradeSuperPower;
        }

        protected override void OnExit()
        {
            provisionCanvas.OnSuperPowerLaserRequest -= superPowerComponent.UpgradeSuperPower;
        }

        protected override void OnUpdate()
        {

        }
    }
}