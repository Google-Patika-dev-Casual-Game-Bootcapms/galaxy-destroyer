using UnityEngine.PlayerLoop;

namespace SpaceShooterProject.UserInterface
{
    using System.Collections.Generic;
    using UnityEngine;
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
        [SerializeField] private List<Button> buttons;//TODO: rename
        [SerializeField] private RectTransform backgroundImage;

        [Header("Card Design Tool Fields \t Never Give 0!")]
        [SerializeField] private int rowNumber = 2;
        [SerializeField] private int columnNumber = 3;
        [SerializeField] private float topOffsetMultiplier = 0.223f;
        [SerializeField] private float bottomOffsetMultiplier = 0.2f;
        [SerializeField] private float verticalSpaceAreaMultiplier = 0.04f;
        [SerializeField] private float rightOffsetMultiplier = 0.1f;
        [SerializeField] private float leftOffsetMultiplier = 0.1f;
        [SerializeField] private float horizontalSpaceAreaMultiplier = 0.04f;

        private Animator cardAnimator;

        private Dictionary<int, bool> activatableCards;

        private InventoryComponent inventoryComponent;
        private CardComponent cardComponent;

        protected override void Init()
        {
            inventoryComponent = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
            cardComponent = componentContainer.GetComponent("CardComponent") as CardComponent;

            // Should add into Account Component
            activatableCards = new Dictionary<int, bool>();

            backgroundImage.sizeDelta = GetCanvasSize();

            CalculateAllCardButtonPlaces();
        }

        public void RequestCardShow()
        {
            if (OnCardShowRequest != null)
            {
                AdjustTheCardCanvas();
                OnCardShowRequest();
            }
        }

        public void RequestSpaceshipShow()
        {
            if (OnSpaceshipShowRequest != null)
            {
                OnSpaceshipShowRequest();
            }
        }

        // Place the cards automatically on canvas
        public void CalculateAllCardButtonPlaces()
        {
            Vector2 canvasSize = GetCanvasSize();

            //Vertical Info
            float topOffset = canvasSize.y - (canvasSize.y * topOffsetMultiplier);
            float bottomOffset = canvasSize.y * bottomOffsetMultiplier;
            float verticalTotalArea = topOffset - bottomOffset;
            float verticalNumberOfSpacesBetweenCards = (rowNumber - 1 <= 0 ? 1 : rowNumber - 1);
            float verticalSpaceAreaBetweenCards = verticalTotalArea * (verticalSpaceAreaMultiplier / verticalNumberOfSpacesBetweenCards);

            //Horizontal Info
            float rightOffset = canvasSize.x - (canvasSize.x * rightOffsetMultiplier);
            float leftOffset = canvasSize.x * leftOffsetMultiplier;
            float horizontalTotalArea = rightOffset - leftOffset;
            float horizontalNumberOfSpacesBetweenCards = (columnNumber - 1 <= 0 ? 1 : columnNumber - 1);
            float horizontalSpaceAreaBetweenCards = horizontalTotalArea * (horizontalSpaceAreaMultiplier / horizontalNumberOfSpacesBetweenCards);

            //Card Size Calculations
            Vector2 rectTransformSizeDelta = buttons[0].GetComponent<RectTransform>().sizeDelta;
            float cardVerticalSizeMultiplier = (verticalTotalArea - verticalSpaceAreaBetweenCards) / (rectTransformSizeDelta.y * rowNumber);
            float cardHorizontalSizeMultiplier = (horizontalTotalArea - horizontalSpaceAreaBetweenCards) / (rectTransformSizeDelta.x * columnNumber);
            float cardSizeMultiplier = Mathf.Min(cardVerticalSizeMultiplier, cardHorizontalSizeMultiplier);
            Vector2 cardNewSize = rectTransformSizeDelta * cardSizeMultiplier;
            rectTransformSizeDelta = cardNewSize;

            //Card Position Calculations
            float cardNewVerticalPosition = topOffset - (rectTransformSizeDelta.y / 2f);
            float cardNewHorizontalPosition = leftOffset + (rectTransformSizeDelta.x / 2f);
            Vector2 cardNewPosition;

            bool cardNumberOverflow = false;
            int rowCount = 1;
            int columnCount = 1;
            foreach (Button button in buttons)
            {
                if (cardNumberOverflow) Debug.LogWarning("More Card Buttons than expected! Increase Row or Column Numbers!");

                button.GetComponent<RectTransform>().sizeDelta = cardNewSize;
                cardNewPosition.y = cardNewVerticalPosition;
                cardNewPosition.x = cardNewHorizontalPosition;
                button.GetComponent<RectTransform>().anchoredPosition = cardNewPosition;

                if (columnCount < columnNumber)
                {
                    cardNewHorizontalPosition += (rectTransformSizeDelta.x + horizontalSpaceAreaBetweenCards);
                    columnCount++;
                }
                else if (rowCount < rowNumber)
                {
                    cardNewVerticalPosition -= (rectTransformSizeDelta.y + verticalSpaceAreaBetweenCards);
                    rowCount++;
                    cardNewHorizontalPosition = leftOffset + (rectTransformSizeDelta.x / 2f);
                    columnCount = 1;
                }
                else cardNumberOverflow = true;
            }
        }

        // Change owned card's sprites and set the corresponding button active
        public void AdjustTheInventoryCanvas()
        {
            SetCardsAsActivatable();

            List<int> ownedPermanentCards = inventoryComponent.GetOwnedPermanentCards();
            List<int> ownedTemporalCards = inventoryComponent.GetOwnedTemporalCards();

            int permanentCardCount = cardComponent.GetPermanentCardCount();

            foreach (int index in ownedPermanentCards)
            {
                ActivateButton(index);

                if (activatableCards[index])
                {
                    Sprite sprite = cardComponent.GetPermanentCardSpriteWithBorders(index);

                    ChangeButtonImage(index, sprite);
                }
                else
                {
                    cardAnimator = buttons[index].GetComponent<Animator>();
                    cardAnimator.SetBool("IsActivatable", true);
                }
            }

            foreach (int index in ownedTemporalCards)
            {
                ActivateButton(index + permanentCardCount);

                if (activatableCards[index + permanentCardCount])
                {
                    Sprite sprite = cardComponent.GetTemporalCardSpriteWithBorders(index);

                    ChangeButtonImage(index + permanentCardCount, sprite);
                }
                else
                {
                    cardAnimator = buttons[index + permanentCardCount].GetComponent<Animator>();
                    cardAnimator.SetBool("IsActivatable", true);
                }
            }
        }

        //TODO: rename
        // With clicked card index adjust the selected card's canvas
        private void AdjustTheCardCanvas()
        {
            Button clickedCard = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
            int clickedCardIndex = buttons.IndexOf(clickedCard);

            activatableCards[clickedCardIndex] = true;

            cardAnimator = clickedCard.GetComponent<Animator>();
            cardAnimator.SetBool("IsActivated", true);

            inventoryComponent.SetCurrentSelectedCardIndex(clickedCardIndex);
        }

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

        // Set owned cards as activatable and show card back for cards until that cards are clicked.
        private void SetCardsAsActivatable()
        {
            int permanentCardCount = cardComponent.GetPermanentCardCount();

            foreach (int index in inventoryComponent.GetOwnedPermanentCards())
            {
                bool isKeyExist = activatableCards.ContainsKey(index);

                if (!isKeyExist)
                {
                    activatableCards.Add(index, false);
                }
            }

            foreach (int index in inventoryComponent.GetOwnedTemporalCards())
            {
                bool isKeyExist = activatableCards.ContainsKey(index + permanentCardCount);

                if (!isKeyExist)
                {
                    activatableCards.Add(index + permanentCardCount, false);
                }
            }
        }
    }
}
