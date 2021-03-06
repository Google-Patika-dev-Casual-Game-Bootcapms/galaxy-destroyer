namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using SpaceShooterProject.UserInterface;
    using UnityEngine;

    public class UIComponent : MonoBehaviour, IComponent
    {
        public enum MenuName 
        { 
            SPLASH, MAIN_MENU,QUOTE, IN_GAME, 
            SETTINGS, ACHIEVEMENTS, MARKET, 
            CO_PILOT, CREDITS, PROVISION,
            INVENTORY, CARD, SPACESHIP, GARAGE, END_GAME,
        }

        [SerializeField]
        private BaseCanvas splashCanvas = null;
        [SerializeField]
        private BaseCanvas mainMenuCanvas = null;
        [SerializeField]
        private BaseCanvas quoteCanvas = null;        
        [SerializeField]
        private BaseCanvas inGameCanvas = null;
        [SerializeField]
        private BaseCanvas settingsCanvas = null;
        [SerializeField]
        private BaseCanvas achievementsCanvas = null;
        [SerializeField]
        private BaseCanvas marketCanvas = null;
        [SerializeField]
        private BaseCanvas inventoryCanvas = null;
        [SerializeField]
        private BaseCanvas cardCanvas = null;
        [SerializeField]
        private BaseCanvas spaceshipCanvas = null;
        [SerializeField]
        private BaseCanvas garageCanvas = null;
        [SerializeField]
        private BaseCanvas coPilotCanvas = null;
        [SerializeField]
        private BaseCanvas creditsCanvas = null;
        [SerializeField]
        private BaseCanvas provisionCanvas = null;
        [SerializeField]
        private BaseCanvas endGameCanvas = null;

        private BaseCanvas activeCanvas = null;
        

        public void Initialize(ComponentContainer componentContainer)
        {
            splashCanvas.Initialize(componentContainer);
            mainMenuCanvas.Initialize(componentContainer);
            quoteCanvas.Initialize(componentContainer);
            inGameCanvas.Initialize(componentContainer);
            settingsCanvas.Initialize(componentContainer);
            achievementsCanvas.Initialize(componentContainer);
            marketCanvas.Initialize(componentContainer);
            inventoryCanvas.Initialize(componentContainer);
            cardCanvas.Initialize(componentContainer);
            spaceshipCanvas.Initialize(componentContainer);
            garageCanvas.Initialize(componentContainer);
            coPilotCanvas.Initialize(componentContainer);
            creditsCanvas.Initialize(componentContainer);
            provisionCanvas.Initialize(componentContainer);
            endGameCanvas.Initialize(componentContainer);

            DeactivateCanvas(splashCanvas);
            DeactivateCanvas(mainMenuCanvas);
            DeactivateCanvas(quoteCanvas);
            DeactivateCanvas(inGameCanvas);
            DeactivateCanvas(settingsCanvas);
            DeactivateCanvas(achievementsCanvas);
            DeactivateCanvas(marketCanvas);
            DeactivateCanvas(inventoryCanvas);
            DeactivateCanvas(cardCanvas);
            DeactivateCanvas(spaceshipCanvas);
            DeactivateCanvas(garageCanvas);
            DeactivateCanvas(coPilotCanvas);
            DeactivateCanvas(creditsCanvas);
            DeactivateCanvas(provisionCanvas);
            DeactivateCanvas(endGameCanvas);
        }

        public BaseCanvas GetCanvas(MenuName canvas)
        {
            switch (canvas)
            {
                case MenuName.SPLASH:
                    return splashCanvas;
                case MenuName.MAIN_MENU:
                    return mainMenuCanvas;
                case MenuName.QUOTE:
                    return quoteCanvas;
                case MenuName.IN_GAME:
                    return inGameCanvas;
                case MenuName.SETTINGS:
                    return settingsCanvas;
                case MenuName.ACHIEVEMENTS:
                    return achievementsCanvas;
                case MenuName.MARKET:
                    return marketCanvas;
                case MenuName.INVENTORY:
                    return inventoryCanvas;
                case MenuName.CARD:
                    return cardCanvas;
                case MenuName.SPACESHIP:
                    return spaceshipCanvas;
                case MenuName.GARAGE:
                    return garageCanvas;
                case MenuName.CO_PILOT:
                    return coPilotCanvas;
                case MenuName.CREDITS:
                    return creditsCanvas;
                case MenuName.PROVISION:
                    return provisionCanvas;
                case MenuName.END_GAME:
                    return endGameCanvas;    
                default:
                    return null;
            }
        }

        public void EnableCanvas(MenuName menuName)
        {
            DeactivateCanvas(activeCanvas);
            ActivateCanvas(menuName);
        }

        private void DeactivateCanvas(BaseCanvas canvas)
        {
            if (canvas)
            {
                canvas.Deactivate();
            }
        }

        private void ActivateCanvas(MenuName menuName)
        {
            switch (menuName)
            {
                case MenuName.SPLASH:
                    activeCanvas = splashCanvas;
                    break;
                case MenuName.MAIN_MENU:
                    activeCanvas = mainMenuCanvas;
                    break;
                case MenuName.QUOTE:
                    activeCanvas = quoteCanvas;
                    break;
                case MenuName.IN_GAME:
                    activeCanvas = inGameCanvas;
                    break;
                case MenuName.SETTINGS:
                    activeCanvas = settingsCanvas;
                    break;
                case MenuName.ACHIEVEMENTS:
                    activeCanvas = achievementsCanvas;
                    break;
                case MenuName.MARKET:
                    activeCanvas = marketCanvas;
                    break;
                case MenuName.INVENTORY:
                    activeCanvas = inventoryCanvas;
                    break;
                case MenuName.CARD:
                    activeCanvas = cardCanvas;
                    break;
                case MenuName.SPACESHIP:
                    activeCanvas = spaceshipCanvas;
                    break;
                case MenuName.GARAGE:
                    activeCanvas = garageCanvas;
                    break;
                case MenuName.CO_PILOT:
                    activeCanvas = coPilotCanvas;
                    break;
                case MenuName.CREDITS:
                    activeCanvas = creditsCanvas;
                    break;
                case MenuName.PROVISION:
                    activeCanvas = provisionCanvas;
                    break;
                case MenuName.END_GAME:
                    activeCanvas = endGameCanvas;
                    break;
            }

            if (activeCanvas)
            {
                activeCanvas.Activate();
            }
        }
    }
}


