namespace SpaceShooterProject.State
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainMenuState : StateMachine
    {
        private UIComponent uiComponent;
        private MainMenuCanvas mainMenuCanvas;

        public MainMenuState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            mainMenuCanvas = uiComponent.GetCanvas(UIComponent.MenuName.MAIN_MENU) as MainMenuCanvas;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.MAIN_MENU);
            mainMenuCanvas.OnInGameMenuRequest += RequestInGameMenu;
            mainMenuCanvas.OnSettingsMenuRequest += OnSettingsMenuRequest;
            mainMenuCanvas.OnAchievementsMenuRequest += OnAchievementsMenuRequest;
            mainMenuCanvas.OnMarketMenuRequest += OnMarketMenuRequest;
            mainMenuCanvas.OnInventoryMenuRequest += OnInventoryMenuRequest;
            mainMenuCanvas.OnGarageMenuRequest += OnGarageMenuRequest;
            mainMenuCanvas.OnCoPilotMenuRequest += OnCoPilotMenuRequest;
            mainMenuCanvas.OnCreditsMenuRequest += OnCreditsMenuRequest;

            mainMenuCanvas.OnNextPlanetButtonRequest += mainMenuCanvas.PlanetUIController.NextPlanet;
            mainMenuCanvas.OnPreviousPlanetButtonRequest += mainMenuCanvas.PlanetUIController.PreviousPlanet;
            mainMenuCanvas.PlanetUIController.SubscribeAllPlanetAnimationCompletionEvents();
            Debug.Log("MainMenuState OnEnter");
        }

        private void OnCreditsMenuRequest()
        {
            SendTrigger((int) StateTriggers.GO_TO_CREDITS);
        }

        private void OnCoPilotMenuRequest()
        {
            SendTrigger((int) StateTriggers.GO_TO_CO_PILOT_REQUEST);
        }

        private void OnGarageMenuRequest()
        {
            SendTrigger((int) StateTriggers.GO_TO_GARAGE_REQUEST);
        }

        private void OnInventoryMenuRequest()
        {
            SendTrigger((int) StateTriggers.GO_TO_INVENTORY_REQUEST);
        }

        private void OnMarketMenuRequest()
        {
            SendTrigger((int) StateTriggers.GO_TO_MARKET_REQUEST);
        }

        private void OnAchievementsMenuRequest()
        {
            SendTrigger((int) StateTriggers.GO_TO_ACHIEVEMENTS_REQUEST);
        }

        private void OnSettingsMenuRequest()
        {
            SendTrigger((int) StateTriggers.GO_TO_SETTINGS_REQUEST);
        }

        private void RequestInGameMenu()
        {
            SendTrigger((int) StateTriggers.START_GAME_REQUEST);
        }

        protected override void OnExit()
        {
            mainMenuCanvas.OnInGameMenuRequest -= RequestInGameMenu;
            mainMenuCanvas.OnSettingsMenuRequest -= OnSettingsMenuRequest;
            mainMenuCanvas.OnAchievementsMenuRequest -= OnAchievementsMenuRequest;
            mainMenuCanvas.OnMarketMenuRequest -= OnMarketMenuRequest;
            mainMenuCanvas.OnInventoryMenuRequest -= OnInventoryMenuRequest;
            mainMenuCanvas.OnCoPilotMenuRequest -= OnCoPilotMenuRequest;
            mainMenuCanvas.OnCreditsMenuRequest -= OnCreditsMenuRequest;

            mainMenuCanvas.OnNextPlanetButtonRequest -= mainMenuCanvas.PlanetUIController.NextPlanet;
            mainMenuCanvas.OnPreviousPlanetButtonRequest -= mainMenuCanvas.PlanetUIController.PreviousPlanet;
            mainMenuCanvas.PlanetUIController.UnSubscribeAllPlanetAnimationCompletionEvents();

            Debug.Log("MainMenuState OnExit");
        }

        protected override void OnUpdate()
        {
            Debug.Log("MainMenuState OnUpdate");
        }
    }
}