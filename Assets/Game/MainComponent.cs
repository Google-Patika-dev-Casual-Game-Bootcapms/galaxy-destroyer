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
        private EditorSceneBuilderComponent editorSceneBuilderComponent;
        private InventoryComponent inventoryComponent;
        private CardComponent cardComponent;
        private MarketComponent marketComponent;
        private CoPilotComponent coPilotComponent;
        private SuperPowerComponent superPowerComponent;
        private InGameInputSystem inGameInputSystem;
        private CurrencyComponent currencyComponent;
        private QuoteComponent quoteComponent;
        private UpgradeComponent upgradeComponent;

        private AppState appState;

        private void Awake()
        {
            componentContainer = new ComponentContainer();
        }

        private void Start()
        {
            CreateAccountComponent();
            CreateUIComponent();
            CreateIntroComponent();
            CreateAchievementsComponent();
            CreateAudioComponent();
            CreateNotificationComponent();
            CreateCurrencyComponent();
            CreateGamePlayComponent();
            CreateTutorialComponent();
            CreateEditorSceneBuilderComponent();
            CreateInventoryComponent();
            CreateCardComponent();
            CreateMarketComponent();
            CreateCoPilotComponent();
            CreateSuperPowerComponent();
            CreateInGameInputSystem();
            CreateQuoteComponent();
            CreateUpgradeComponent();

            InitializeComponents();
            CreateAppState();
            appState.Enter();
        }

        public void Update()
        {
            appState.Update();
        }

        public void OnDestroy()
        {
            accountComponent.OnDestruct();
        }

        private void CreateAccountComponent()
        {
            accountComponent = new AccountComponent();
            componentContainer.AddComponent("AccountComponent", accountComponent);
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
            achievementsComponent = FindObjectOfType<AchievementsComponent>();

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

        private void CreateCurrencyComponent()
        {
            currencyComponent = new CurrencyComponent();
            componentContainer.AddComponent("CurrencyComponent", currencyComponent);
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

        private void CreateEditorSceneBuilderComponent()
        {
            //TODO: Make prefab!!!
            editorSceneBuilderComponent = new GameObject().AddComponent<EditorSceneBuilderComponent>();
            componentContainer.AddComponent("LevelEditorSceneBuilderComponent", editorSceneBuilderComponent);
        }

        private void CreateInventoryComponent()
        {
            inventoryComponent = gameObject.AddComponent<InventoryComponent>();
            componentContainer.AddComponent("InventoryComponent", inventoryComponent);
        }

        private void CreateCardComponent()
        {
            cardComponent = FindObjectOfType<CardComponent>();
            componentContainer.AddComponent("CardComponent", cardComponent);
        }

        private void CreateCoPilotComponent()
        {
            coPilotComponent = new CoPilotComponent();
            componentContainer.AddComponent("CoPilotComponent", coPilotComponent);
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

        private void CreateInGameInputSystem()
        {
            inGameInputSystem = new InGameInputSystem();
            componentContainer.AddComponent("InGameInputSystem", inGameInputSystem);
        }

        private void CreateQuoteComponent() 
        {
            quoteComponent = FindObjectOfType<QuoteComponent>();
            componentContainer.AddComponent("QuoteComponent", quoteComponent);
        }

        private void CreateUpgradeComponent()
        {
            upgradeComponent = new UpgradeComponent();
            componentContainer.AddComponent("UpgradeComponent", upgradeComponent);
        }

        private void InitializeComponents()
        {
            accountComponent.Initialize(componentContainer);
            quoteComponent.Initialize(componentContainer);
            achievementsComponent.Initialize(componentContainer);
            uIComponent.Initialize(componentContainer);
            introComponent.Initialize(componentContainer);
            audioComponent.Initialize(componentContainer);
            notificationComponent.Initialize(componentContainer);
            currencyComponent.Initialize(componentContainer);
            gamePlayComponent.Initialize(componentContainer);
            editorSceneBuilderComponent.Initialize(componentContainer);
            inventoryComponent.Initialize(componentContainer);
            cardComponent.Initialize(componentContainer);
            marketComponent.Initialize(componentContainer);
            coPilotComponent.Initialize(componentContainer);
            superPowerComponent.Initialize(componentContainer);
            upgradeComponent.Initialize(componentContainer);
            currencyComponent.Initialize(componentContainer);
            inGameInputSystem.Initialize(componentContainer);
        }

        private void CreateAppState()
        {
            appState = new AppState(componentContainer);
        }
    }
}