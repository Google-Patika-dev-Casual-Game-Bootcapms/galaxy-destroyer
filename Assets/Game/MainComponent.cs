using SpaceShooterProject.Component.CoPilot;

namespace SpaceShooterProject 
{
    using Devkit.Base.Component;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.State;
    using System;
    using UnityEngine;
    public class MainComponent : MonoBehaviour
    {
        private ComponentContainer componentContainer;
        private AccountComponent accountComponent;
        private UIComponent uIComponent;
        private AchievementsComponent achievementsComponent;
        private AudioComponent audioComponent;
        private GamePlayComponent gamePlayComponent;
        private NotificationComponent notificationComponent;
        private TutorialComponent tutorialComponent;
        private IntroComponent introComponent;
        private InventoryComponent inventoryComponent;
        private MarketComponent marketComponent;
        private CoPilotComponent coPilotComponent;
        private SuperPowerComponent superPowerComponent;
        private GachaComponent gachaComponent;
        private CurrencyComponent currencyComponent;

        private AppState appState;

        private void Awake()
        {
            componentContainer = new ComponentContainer();
        }

        private void Start()
        {
            CreateAccountComponent();
            CreateGachaComponent();
            CreateUIComponent();
            CreateIntroComponent();
            CreateAchievementsComponent();
            CreateAudioComponent();
            CreateNotificationComponent();
            CreateGamePlayComponent();
            CreateTutorialComponent();
            CreateInventoryComponent();
            CreateMarketComponent();
            CreateCoPilotComponent();
            CreateSuperPowerComponent();
            CreateCurrencyComponent();

            InitializeComponents();
            CreateAppState();
            appState.Enter();
        }

        public void Update()
        {
            appState.Update();
        }

        private void CreateAccountComponent()
        {
            accountComponent = new AccountComponent();
            componentContainer.AddComponent("AccountComponent", accountComponent);
        }

        private void CreateGachaComponent()
        {
            gachaComponent = new GachaComponent();
            componentContainer.AddComponent("GachaComponent", gachaComponent);
        }

        private void CreateUIComponent()
        {
            uIComponent = FindObjectOfType<UIComponent>();
            //TODO: check is there any ui component object in the scene!!
            componentContainer.AddComponent("UIComponent", uIComponent);
        }

        private void CreateIntroComponent()
        {
            introComponent = FindObjectOfType<IntroComponent>();
            componentContainer.AddComponent("IntroComponent", introComponent);
        }

        private void CreateAchievementsComponent()
        {
            achievementsComponent = new AchievementsComponent();
            componentContainer.AddComponent("AchievementsComponent", achievementsComponent);
        }

        private void CreateAudioComponent()
        {
            audioComponent = FindObjectOfType<AudioComponent>();
            componentContainer.AddComponent("AudioComponent", audioComponent);
        }

        private void CreateNotificationComponent()
        {
            notificationComponent = new NotificationComponent();
            componentContainer.AddComponent("NotificationComponent", notificationComponent);
        }

        private void CreateGamePlayComponent()
        {
            gamePlayComponent = FindObjectOfType<GamePlayComponent>();
            componentContainer.AddComponent("GamePlayComponent", gamePlayComponent);
        }

        private void CreateTutorialComponent()
        {
            tutorialComponent = new TutorialComponent();
            componentContainer.AddComponent("TutorialComponent", tutorialComponent);
        }

        private void CreateInventoryComponent()
        {
            inventoryComponent = gameObject.AddComponent<InventoryComponent>();
            componentContainer.AddComponent("InventoryComponent", inventoryComponent);
        }
        
        private void CreateCoPilotComponent()
        {
            coPilotComponent = new CoPilotComponent();
            componentContainer.AddComponent("CoPilotComponent",coPilotComponent);
        }

        private void CreateSuperPowerComponent()
        {
            superPowerComponent = new SuperPowerComponent();
            componentContainer.AddComponent("SuperPowerComponent", superPowerComponent);
        }

        private void CreateMarketComponent()
        {
            marketComponent = FindObjectOfType<MarketComponent>();
            componentContainer.AddComponent("MarketComponent", marketComponent);
        }

        private void CreateCurrencyComponent()
        {
            currencyComponent = new CurrencyComponent();
            componentContainer.AddComponent("CurrencyComponent", currencyComponent);
        }

        private void InitializeComponents()
        {
            accountComponent.Initialize(componentContainer);
            currencyComponent.Initialize(componentContainer);
            gachaComponent.Initialize(componentContainer);
            uIComponent.Initialize(componentContainer);
            introComponent.Initialize(componentContainer);
            achievementsComponent.Initialize(componentContainer);
            audioComponent.Initialize(componentContainer);
            notificationComponent.Initialize(componentContainer);
            gamePlayComponent.Initialize(componentContainer);
            inventoryComponent.Initialize(componentContainer);
            marketComponent.Initialize(componentContainer);
            coPilotComponent.Initialize(componentContainer);
            superPowerComponent.Initialize(componentContainer);
        }

        private void CreateAppState()
        {
            appState = new AppState(componentContainer);
        }
    }
}

