namespace SpaceShooterProject.UserInterface
{
    using System.Collections.Generic;
    using UnityEngine;
    using DG.Tweening;
    using TMPro;
    using UnityEngine.UI;
    using Devkit.Base.Component;
    using SpaceShooterProject.Component;
    using UnityEngine.EventSystems;

    public class InventoryCanvas : BaseCanvas, IComponent
    {
        //Events
        public delegate void InventoryRequestDelegate();
        public event InventoryRequestDelegate OnCardShowRequest;
        public event InventoryRequestDelegate OnSpaceshipShowRequest;
        
        //Fields
        //[SerializeField] private float animationDuration;
        [SerializeField] private int activeCardIndex = 0;
        [SerializeField] private int activeSpaceshipIndex = 0;
        [SerializeField] private RectTransform backgroundImage;
        [SerializeField] private Sprite ownedCardSprite;
        [SerializeField] private List<Button> buttons;

        private InventoryComponent inventoryComponent;

        protected override void Init()
        {
            inventoryComponent = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
            backgroundImage.sizeDelta = GetCanvasSize();
            //CalculateAllCardButtonPlaces(2, 3, 2);
        }

        public void CalculateAllCardButtonPlaces(int rowNumber, int columnNumber, int yOffset)
        {
            Vector2 canvasSize = GetCanvasSize();
            float xBase = canvasSize.x / (columnNumber + 1);
            float yBase = canvasSize.y / (rowNumber + 1 + yOffset + 1); // last +1 is for header

            int cardIndex = 0;
            for(int row = (rowNumber + yOffset); row > rowNumber; row--)
            {
                for(int column = 1; column <= columnNumber; column++)
                {
                    buttons[cardIndex].GetComponent<RectTransform>().anchoredPosition = new Vector3(xBase * column, yBase * row, buttons[cardIndex].transform.position.z);
                    print("New Position After Function: " + buttons[cardIndex].transform.position);
                    cardIndex++;
                }
            }
        }

        #region Transitions
        /*
        * Transition to CardCanvas
        */
        public void RequestCardShow()
        {
            if (OnCardShowRequest != null)
            {
                OnCardShowRequest();
            }
        }

        /*
         * Transition to SpaceshipCanvas
         */
        public void RequestSpaceshipShow()
        {
            if (OnSpaceshipShowRequest != null)
            {
                OnSpaceshipShowRequest();
            }
        }
        #endregion

        #region ButtonControllers
        //TODO: generate generic function for both permanent and temporary cards to control them easily
        public void ChangeButtonImage()
        {
            buttons[inventoryComponent.GetOwnedPermanentCards()[activeCardIndex]].GetComponent<Image>().sprite = ownedCardSprite;
        }

        public void ActivateButton()
        {
            buttons[inventoryComponent.GetOwnedPermanentCards()[activeCardIndex]].GetComponent<Button>().enabled = true;
        }

        public void DeactivateButton()
        {
            buttons[inventoryComponent.GetOwnedPermanentCards()[activeCardIndex]].GetComponent<Button>().enabled = false;
        }
        #endregion

        #region GetCanvasSize
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
        #endregion
    }
}
