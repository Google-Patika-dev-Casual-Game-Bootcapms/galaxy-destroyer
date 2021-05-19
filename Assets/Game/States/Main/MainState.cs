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
        private MarketState marketState;
        private SpaceShipUpgradeState spaceShipUpgradeState;
        private SpaceShipSelectionState spaceShipSelectionState;
        private AchievementsState achievementsState;
        private SettingsState settingsState;
        private InventoryState inventoryState;
        private CoPilotSelectionState coPilotSelectionState;

        public MainState(ComponentContainer componentContainer)
        {
            mainMenuState = new MainMenuState(componentContainer);
            marketState = new MarketState(componentContainer);
            spaceShipSelectionState = new SpaceShipSelectionState(componentContainer);
            spaceShipUpgradeState = new SpaceShipUpgradeState(componentContainer);
            achievementsState = new AchievementsState(componentContainer);
            settingsState = new SettingsState(componentContainer);
            inventoryState = new InventoryState(componentContainer);
            coPilotSelectionState = new CoPilotSelectionState(componentContainer);

            this.AddSubState(mainMenuState);
            this.AddSubState(marketState);
            this.AddSubState(spaceShipSelectionState);
            this.AddSubState(spaceShipUpgradeState);
            this.AddSubState(achievementsState);
            this.AddSubState(settingsState);
            this.AddSubState(inventoryState);

            SetupMarketTransitions();
            SetupAchievementsTransitions();
            SetupSpaceShipSelectionTransitions();
            SetupSpaceShipUpgradeTransition();
            SetupSettingsTransition();
            SetupInventoryState();
            SetupCoPilotTransition();
        }

        private void SetupMarketTransitions()
        {
            this.AddTransition(mainMenuState, marketState, (int)StateTriggers.GO_TO_MARKET_REQUEST);
            this.AddTransition(marketState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        private void SetupAchievementsTransitions()
        {
            AddTransition(mainMenuState, achievementsState, (int)StateTriggers.GO_TO_ACHIEVEMENTS_REQUEST);
            AddTransition(achievementsState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        private void SetupSpaceShipSelectionTransitions()
        {
            this.AddTransition(mainMenuState, spaceShipSelectionState, (int)StateTriggers.GO_TO_SPACE_SHIP_SELECTION_REQUEST);
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
        }

        private void SetupCoPilotTransition()
        {
            this.AddTransition(mainMenuState, coPilotSelectionState, (int)StateTriggers.GO_TO_CO_PILOT_REQUEST);
            this.AddTransition(coPilotSelectionState, mainMenuState, (int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
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

