namespace SpaceShooterProject.UserInterface
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using DG.Tweening;
    using TMPro;
    using UnityEngine.UI;
    using Devkit.Base.Component;
    using SpaceShooterProject.Component;
    using UnityEngine.EventSystems;

    public class CardCanvas : BaseCanvas, IComponent
    {
        //Events
        public delegate void CardRequestDelegate();
        public event CardRequestDelegate OnReturnToInventory;

        //Fields
        [SerializeField] private RectTransform backgroundImage;
        [SerializeField] private RectTransform cardSprite;
        [SerializeField] private TMP_Text header;
        [SerializeField] private TMP_Text description;

        private CardComponent cardComponent;


        protected override void Init()
        {
            cardComponent = componentContainer.GetComponent("CardComponent") as CardComponent;

            backgroundImage.sizeDelta = GetCanvasSize();
        }

        public void RequestReturnToInventory()
        {
            if (OnReturnToInventory != null)
            {
                OnReturnToInventory();
            }
        }

        public void AdjustTheCanvas(int index)
        {
            int permanentCardCount = cardComponent.GetPermanentCardCount();

            if (index < permanentCardCount)
            {
                header.text = cardComponent.GetPermanentCardName(index);
                description.text = "\"" +  cardComponent.GetPermanentCardDescription(index) + "\"";

                cardSprite.GetComponent<Image>().sprite = cardComponent.GetPermanentCardSprite(index);
            }
            else
            {
                index -= permanentCardCount;

                header.text = cardComponent.GetTemporalCardName(index);
                description.text = "\"" + cardComponent.GetTemporalCardDescription(index) + "\"";

                cardSprite.GetComponent<Image>().sprite = cardComponent.GetTemporalCardSprite(index);
            }
        }
    }
}