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
        private CurrencyComponent currencyComponent;


        public GarageState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            garageCanvas = uiComponent.GetCanvas(UIComponent.MenuName.GARAGE) as GarageCanvas;
            upgradeComponent = componentContainer.GetComponent("UpgradeComponent") as UpgradeComponent;
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.GARAGE);
            garageCanvas.OnReturnToMainMenu += OnReturnToMainMenu;
            garageCanvas.OnPartUpgradeRequest += OnPartUpgradeRequest;
            upgradeComponent.OnUpgradeProcessCompleted += OnUpgradeProcessCompleted;
            garageCanvas.OnRequestSpaceShipChange += RequestShipChange;
        }
            

        private void OnPartUpgradeRequest(UpgradablePartType upgradablePartType)
        {
            upgradeComponent.UpgradePart(upgradablePartType);
        }

        private void OnUpgradeProcessCompleted(UpgradeProcessData upgradeProcessData)
        {
            garageCanvas.OnUpgradeProcessCompleted(upgradeProcessData,currencyComponent.GetOwnedGold());
        }
        private void RequestShipChange(bool isNextShip)
        {
            var maxSpaceshipCount = accountComponent.GetMaxSpaceshipCount();
            var currentSelectedShip = accountComponent.GetSelectedSpaceShipId();
            if (isNextShip)
            {
                if (maxSpaceshipCount < currentSelectedShip)
                {
                    accountComponent.SetSelectedSpaceShipId(0);
                }
                else
                {
                    currentSelectedShip++;
                    accountComponent.SetSelectedSpaceShipId(currentSelectedShip);
                }
            }

            // if user selected previous ship
            else
            {
                if (currentSelectedShip - 1 < 0)//TODO : Refactor if state
                {
                    maxSpaceshipCount--;
                    accountComponent.SetSelectedSpaceShipId(maxSpaceshipCount);
                }
                else
                {
                    currentSelectedShip--;
                    accountComponent.SetSelectedSpaceShipId(currentSelectedShip);
                }
            }

            //var accountshipID = accountComponent.GetSelectedSpaceShipId();
            garageCanvas.OnSpaceShipChangeSucces(accountComponent.GetSelectedSpaceShipId());
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
            garageCanvas.OnRequestSpaceShipChange -= RequestShipChange;
        }

        protected override void OnUpdate()
        {
            garageCanvas.UpdateUI(accountComponent.GetCurrentSpaceShipUpgradePartData(), currencyComponent.GetOwnedGold(),accountComponent.GetSelectedSpaceShipId());//TODO : Refactor
        }
    
    }

}

