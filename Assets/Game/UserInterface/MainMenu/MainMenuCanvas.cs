using System;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooterProject.UserInterface
{
    public class MainMenuCanvas : BaseCanvas
    {
        public delegate void MenuRequestDelegate();

        public delegate void MenuPlanetSelectionDelegate();

        public event MenuRequestDelegate OnInGameMenuRequest;
        public event MenuRequestDelegate OnSettingsMenuRequest;
        public event MenuRequestDelegate OnAchievementsMenuRequest;
        public event MenuRequestDelegate OnMarketMenuRequest;
        public event MenuRequestDelegate OnInventoryMenuRequest;
        public event MenuRequestDelegate OnGarageMenuRequest;
        public event MenuRequestDelegate OnCoPilotMenuRequest;
        public event MenuRequestDelegate OnCreditsMenuRequest;
        public event MenuRequestDelegate OnQuoteMenuRequest;
        public event MenuPlanetSelectionDelegate OnNextPlanetButtonRequest;
        public event MenuPlanetSelectionDelegate OnPreviousPlanetButtonRequest;

        [SerializeField] private RectTransform backgroundImage;

        [SerializeField] private PlanetUIController planetUIController;

        // Created for Adjusting Inventory Canvas before entering the canvas
        private InventoryCanvas inventoryCanvas;

        protected override void Init()
        {
            inventoryCanvas = FindObjectOfType<InventoryCanvas>();

            backgroundImage.sizeDelta = GetCanvasSize();
            planetUIController.Init();
        }

        public void RequestQuoteMenu()
        {
            if (OnQuoteMenuRequest != null)
            {
                OnQuoteMenuRequest();
            }
        }

        public void RequestSettingsMenu()
        {
            if (OnSettingsMenuRequest != null)
            {
                OnSettingsMenuRequest();
            }
        }

        public void RequestAchievementsMenu()
        {
            if (OnAchievementsMenuRequest != null)
            {
                OnAchievementsMenuRequest();
            }
        }

        public void RegisterUIEvents()
        {
            planetUIController.SubscribeAllPlanetAnimationCompletionEvents();
        }

        public void RequestMarketMenu()
        {
            if (OnMarketMenuRequest != null)
            {
                OnMarketMenuRequest();
            }
        }

        public void RequestInventoryMenu()
        {
            if (OnInventoryMenuRequest != null)
            {
                inventoryCanvas.AdjustTheInventoryCanvas();
                OnInventoryMenuRequest();
            }
        }

        public void ShowPreviosPlanet()
        {
            planetUIController.PreviousPlanet();
        }

        public void ShowNextPlanet()
        {
            planetUIController.NextPlanet();
        }

        public void RequestGarageMenu()
        {
            if (OnGarageMenuRequest != null)
            {
                OnGarageMenuRequest();
            }
        }

        public void RequestCoPilotMenu()
        {
            if (OnCoPilotMenuRequest != null)
            {
                OnCoPilotMenuRequest();
            }
        }

        public void RequestCreditsMenu()
        {
            if (OnCreditsMenuRequest != null)
            {
                OnCreditsMenuRequest();
            }
        }

        public void OnNextPlanetButtonClick()
        {
            if (OnNextPlanetButtonRequest != null)
                OnNextPlanetButtonRequest();
        }

        public void OnPreviousPlanetButtonClick()
        {
            if (OnPreviousPlanetButtonRequest != null)
                OnPreviousPlanetButtonRequest();
        }

        public int GetCurrentSelectedPlanetId() 
        {
            return planetUIController.GetSelectedPlanetIndex();
        }

        public void UpdateCurrentSelectedPlanetUI(int selectedPlanetId) 
        {
            //TODO: refactor UI side
            planetUIController.UpdateUI(0);//TODO: delete work-around
            //planetUIController.UpdateUI(selectedPlanetId);
        }

        public void UnRegisterFromUIEvents()
        {
            planetUIController.UnSubscribeAllPlanetAnimationCompletionEvents();
        }

        public void CompleteAllUIAnimations()
        {
            planetUIController.CompleteAllPlanetAnimations();
        }
    }
}