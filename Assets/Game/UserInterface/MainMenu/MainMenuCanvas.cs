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

        protected override void Init()
        {
            backgroundImage.sizeDelta = GetCanvasSize();
            planetUIController.Init();
        }

        private Vector2 GetCanvasSize()
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
            var m_ScreenMatchMode = canvasScaler.screenMatchMode;
            var m_ReferenceResolution = canvasScaler.referenceResolution;
            var m_MatchWidthOrHeight = canvasScaler.matchWidthOrHeight;

            float scaleFactor = 0;
            float logWidth = Mathf.Log(screenSize.x / m_ReferenceResolution.x, 2);
            float logHeight = Mathf.Log(screenSize.y / m_ReferenceResolution.y, 2);
            float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, m_MatchWidthOrHeight);
            scaleFactor = Mathf.Pow(2, logWeightedAverage);

            return new Vector2(screenSize.x / scaleFactor, screenSize.y / scaleFactor);
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
                OnInventoryMenuRequest();
            }
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

        public PlanetUIController PlanetUIController => planetUIController;
    }
}