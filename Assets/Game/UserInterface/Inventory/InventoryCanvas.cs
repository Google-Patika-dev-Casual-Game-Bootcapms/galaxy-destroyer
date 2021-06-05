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

    public class InventoryCanvas : BaseCanvas, IComponent
    {
        //StateTriggers ve UIComponent içine Enum'lar ve fonksiyonları eklendi
       
        public delegate void InventoryRequestDelegate();

        public event InventoryRequestDelegate OnReturnToInventory; //SpaceshipCanvas ve CardCanvas kullanmalı

        //SpaceshipCanvas ve CardCanvas geçişleri için InventoryState içinde kullanılmalı
        public event InventoryRequestDelegate OnCardShowRequest;
        public event InventoryRequestDelegate OnSpaceshipShowRequest;
        


        //[SerializeField] private float animationDuration;
        [SerializeField] private int activeCardIndex = 0;
        [SerializeField] private int activeSpaceshipIndex = 0;
        [SerializeField] private RectTransform backgroundImage;
        [SerializeField] private Sprite ownedCardSprite;


        protected override void Init()
        {
            var inventory = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
            backgroundImage.sizeDelta = GetCanvasSize();
        }

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

        public void RequestReturnToInventory()
        {
            if (OnReturnToInventory != null)
            {
                OnReturnToInventory();
            }
        }

        // TODO: Get card data and change the card 
        public void ChangeCardButtonImage()
        {
            EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite = ownedCardSprite;
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
    }
}
