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
        private CurrencyComponent currencyComponent;
        private UIComponent uiComponent;
        private ProvisionCanvas provisionCanvas;
        private AccountComponent accountComponent;

        public PrepareGameState(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;
            superPowerComponent = componentContainer.GetComponent("SuperPowerComponent") as SuperPowerComponent;
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            provisionCanvas = uiComponent.GetCanvas(UIComponent.MenuName.PROVISION) as ProvisionCanvas;
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.PROVISION);
            provisionCanvas.OnSuperPowerPurchaseRequest += OnSuperPowerPurchaseRequest;
            provisionCanvas.OnNextShipSelectionRequest += RequestNextShip;
            provisionCanvas.OnPreviousShipSelectionRequest += RequestPreviousShip;
            provisionCanvas.OnPauseRequest += RequestPause;
            provisionCanvas.OnStartRequest += RequestInGame;
            superPowerComponent.OnSuperPowerProcessCompleted += OnSuperPowerProcessCompleted;
            provisionCanvas.UpdateUI(accountComponent.GetSuperPowerData(),currencyComponent.GetOwnedGold());
        }

        protected override void OnExit()
        {
            provisionCanvas.OnSuperPowerPurchaseRequest -= OnSuperPowerPurchaseRequest;
            provisionCanvas.OnNextShipSelectionRequest -= RequestNextShip;
            provisionCanvas.OnPreviousShipSelectionRequest -= RequestPreviousShip;
            provisionCanvas.OnPauseRequest -= RequestPause;
            provisionCanvas.OnStartRequest -= RequestInGame;
            superPowerComponent.OnSuperPowerProcessCompleted -= OnSuperPowerProcessCompleted;
        }

        protected override void OnUpdate()
        {

        }

        private void RequestInGame()
        {
            SendTrigger((int)StateTriggers.PLAY_GAME_REQUEST);
        }

        private void RequestPause()
        {
            SendTrigger((int)StateTriggers.PAUSE_GAME_REQUEST);
        }

        private void RequestNextShip()
        {
            
        }

        private void RequestPreviousShip()
        {

        }

        private void OnSuperPowerPurchaseRequest(SuperPowerType superPowerType)
        {
            superPowerComponent.PurchaseSuperPower(superPowerType);
        }
        

        private void OnSuperPowerProcessCompleted(SuperPowerPurchaseProcessData superPowerPurchaseProcessData)
        {
            provisionCanvas.OnSuperPowerPurchaseCompleted(superPowerPurchaseProcessData,currencyComponent.GetOwnedGold());
        }
    }
}