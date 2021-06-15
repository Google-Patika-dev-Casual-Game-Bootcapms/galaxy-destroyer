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
        private UpgradeComponent upgradeComponent;
        private AccountComponent accountComponent;


        public GarageState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            garageCanvas = uiComponent.GetCanvas(UIComponent.MenuName.GARAGE) as GarageCanvas;
            upgradeComponent = componentContainer.GetComponent("UpgradeComponent") as UpgradeComponent;
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.GARAGE);
            garageCanvas.OnReturnToMainMenu += OnReturnToMainMenu;
            garageCanvas.OnPartUpgradeRequest += OnPartUpgradeRequest;
            upgradeComponent.OnUpgradeProcessCompleted += OnUpgradeProcessCompleted;

            garageCanvas.UpdateUI(accountComponent.GetCurrentSpaceShipUpgradePartData());
        }

        private void OnPartUpgradeRequest(UpgradablePartType upgradablePartType)
        {
            upgradeComponent.UpgradePart(upgradablePartType);
        }

        private void OnUpgradeProcessCompleted(UpgradeProcessData upgradeProcessData)
        {
            garageCanvas.OnUpgradeProcessCompleted(upgradeProcessData);
        }

        private void OnReturnToMainMenu()
        {
            SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        protected override void OnExit()
        {
            garageCanvas.OnReturnToMainMenu -= OnReturnToMainMenu;
            garageCanvas.OnPartUpgradeRequest -= OnPartUpgradeRequest;
            upgradeComponent.OnUpgradeProcessCompleted -= OnUpgradeProcessCompleted;
        }

        protected override void OnUpdate()
        {
            
        }
    }

}

