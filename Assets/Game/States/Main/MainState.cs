namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class MainState : StateMachine
    {
        private MainMenuState mainMenuState;
        private QuoteState quoteState;
        private MarketState marketState;
        private SpaceShipUpgradeState spaceShipUpgradeState;
        private GarageState spaceShipSelectionState;
        private AchievementsState achievementsState;
        private SettingsState settingsState;
        private InventoryState inventoryState;
        private SelectedCardState selectedCardState;
        private SelectedSpaceshipState selectedSpaceshipState;
        private CoPilotSelectionState coPilotSelectionState;
        private CreditsState creditsState;
        public MainState(ComponentContainer componentContainer)
        {
            mainMenuState = new MainMenuState(componentContainer);
            quoteState = new QuoteState(componentContainer);
            marketState = new MarketState(componentContainer);
            spaceShipSelectionState = new GarageState(componentContainer);
            spaceShipUpgradeState = new SpaceShipUpgradeState(componentContainer);
            achievementsState = new AchievementsState(componentContainer);
            settingsState = new SettingsState(componentContainer);
            inventoryState = new InventoryState(componentContainer);
            selectedCardState = new SelectedCardState(componentContainer);
            selectedSpaceshipState = new SelectedSpaceshipState(componentContainer);
            coPilotSelectionState = new CoPilotSelectionState(componentContainer);
            creditsState = new CreditsState(componentContainer);

            this.AddSubState(mainMenuState);            
            this.AddSubState(quoteState);
            this.AddSubState(marketState);
            this.AddSubState(spaceShipSelectionState);
            this.AddSubState(spaceShipUpgradeState);
            this.AddSubState(achievementsState);
            this.AddSubState(settingsState);
            this.AddSubState(inventoryState);
            this.AddSubState(selectedCardState);
            this.AddSubState(selectedSpaceshipState);
            this.AddSubState(coPilotSelectionState);
            this.AddSubState(creditsState);

            SetupMarketTransitions();
            SetupQuoteTransitions();
            SetupAchievementsTransitions();
            SetupSpaceShipSelectionTransitions();
            SetupSpaceShipUpgradeTransition();
            SetupSettingsTransition();
            SetupInventoryState();
            SetupCoPilotTransition();
            SetupCreditsTransition();
        }

        private void SetupMarketTransitions()
        {
            this.AddTransition(mainMenuState, marketState, (int)StateTriggers.GO_TO_MARKET_REQUEST);
            this.AddTransition(marketState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        private void SetupQuoteTransitions()
        {
            this.AddTransition(mainMenuState, quoteState, (int)StateTriggers.GO_TO_QUOTE_REQUEST);
        }

        private void SetupAchievementsTransitions()
        {
            AddTransition(mainMenuState, achievementsState, (int)StateTriggers.GO_TO_ACHIEVEMENTS_REQUEST);
            AddTransition(achievementsState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        private void SetupSpaceShipSelectionTransitions()
        {
            this.AddTransition(mainMenuState, spaceShipSelectionState, (int)StateTriggers.GO_TO_GARAGE_REQUEST);
            this.AddTransition(spaceShipSelectionState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        private void SetupSpaceShipUpgradeTransition()
        {
            this.AddTransition(mainMenuState, spaceShipUpgradeState, (int)StateTriggers.GO_TO_SPACE_SHIP_UPGRADE_REQUEST);
            this.AddTransition(spaceShipUpgradeState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        private void SetupSettingsTransition()
        {
            this.AddTransition(mainMenuState, settingsState, (int)StateTriggers.GO_TO_SETTINGS_REQUEST);
            this.AddTransition(settingsState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        private void SetupInventoryState()
        {
            this.AddTransition(mainMenuState, inventoryState, (int)StateTriggers.GO_TO_INVENTORY_REQUEST);
            this.AddTransition(inventoryState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
            this.AddTransition(inventoryState, selectedCardState, (int)StateTriggers.OPEN_CARD);
            this.AddTransition(selectedCardState, inventoryState, (int)StateTriggers.RETURN_TO_INVENTORY);
            this.AddTransition(inventoryState, selectedSpaceshipState, (int)StateTriggers.OPEN_SPACESHIP);
            this.AddTransition(selectedSpaceshipState, inventoryState, (int)StateTriggers.RETURN_TO_INVENTORY);
        }

        private void SetupCoPilotTransition()
        {
            this.AddTransition(mainMenuState, coPilotSelectionState, (int)StateTriggers.GO_TO_CO_PILOT_REQUEST);
            this.AddTransition(coPilotSelectionState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        private void SetupCreditsTransition()
        {
            this.AddTransition(mainMenuState, creditsState, (int)StateTriggers.GO_TO_CREDITS);
            this.AddTransition(creditsState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        protected override void OnEnter()
        {
            Debug.Log("MainState OnEnter");
        }

        protected override void OnExit()
        {
            Debug.Log("MainState OnExit");
        }

        protected override void OnUpdate()
        {
            Debug.Log("MainState OnUpdate");
        }
    }
}

