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
        //[SerializeField] private int activeCardIndex = 0;
        //[SerializeField] private int activeSpaceshipIndex = 0;
        //[SerializeField] private Sprite ownedCardSprite;
        [SerializeField] private List<Button> buttons;
        [SerializeField] private RectTransform backgroundImage;

        private InventoryComponent inventoryComponent;
        private CardComponent cardComponent;

        private CardCanvas cardCanvas;

        protected override void Init()
        {
            inventoryComponent = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
            cardComponent = componentContainer.GetComponent("CardComponent") as CardComponent;

            cardCanvas = FindObjectOfType<CardCanvas>();

            backgroundImage.sizeDelta = GetCanvasSize();
            //CalculateAllCardButtonPlaces(2, 3, 2);
        }

        /*
        * Transition to CardCanvas
        */
        public void RequestCardShow()
        {
            if (OnCardShowRequest != null)
            {
                AdjustTheCardCanvas();
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

        public void CalculateAllCardButtonPlaces(int rowNumber, int columnNumber, int yOffset)
        {
            Vector2 canvasSize = GetCanvasSize();
            float xBase = canvasSize.x / (columnNumber + 1);
            float yBase = canvasSize.y / (rowNumber + 1 + yOffset + 1); // last +1 is for header

            int cardIndex = 0;
            for (int row = (rowNumber + yOffset); row > rowNumber; row--)
            {
                for (int column = 1; column <= columnNumber; column++)
                {
                    buttons[cardIndex].GetComponent<RectTransform>().anchoredPosition = new Vector3(xBase * column, yBase * row, buttons[cardIndex].transform.position.z);
                    print("New Position After Function: " + buttons[cardIndex].transform.position);
                    cardIndex++;
                }
            }
        }

        // Change owned card's sprites and set the corresponding button active
        public void AdjustTheInventoryCanvas()
        {
            List<int> ownedPermanentCards = inventoryComponent.GetOwnedPermanentCards();
            List<int> ownedTemporalCards = inventoryComponent.GetOwnedTemporalCards();

            int permanentCardCount = cardComponent.GetPermanentCardCount();

            foreach (int index in ownedPermanentCards)
            {
                Sprite sprite = cardComponent.GetPermanentCardSprite(index);

                ChangeButtonImage(index, sprite);
                ActivateButton(index);
            }

            foreach (int index in ownedTemporalCards)
            {
                Sprite sprite = cardComponent.GetTemporalCardSprite(index);

                ChangeButtonImage(index + permanentCardCount, sprite);
                ActivateButton(index + permanentCardCount);
            }
        }

        // With clicked card index adjust the selected card's canvas
        private void AdjustTheCardCanvas()
        {
            Button clickedCard = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            int clickedCardIndex = buttons.IndexOf(clickedCard);

            cardCanvas.AdjustTheCanvas(clickedCardIndex);
        }

        //TODO: generate generic function for both permanent and temporary cards to control them easily
        public void ChangeButtonImage(int index, Sprite cardArtwork)
        {
            buttons[index].GetComponent<Image>().sprite = cardArtwork;
        }

        public void ActivateButton(int index)
        {
            buttons[index].GetComponent<Button>().enabled = true;
        }

        public void DeactivateButton(int index)
        {
            buttons[index].GetComponent<Button>().enabled = false;
        }
    }
}
