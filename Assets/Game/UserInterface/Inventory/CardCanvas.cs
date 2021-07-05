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
    using static SpaceShooterProject.UserInterface.InventoryCanvas;

    public class CardCanvas : BaseCanvas, IComponent
    {
        //Events
        public delegate void CardRequestDelegate();
        public event CardRequestDelegate OnReturnToInventory;

        //Fields
        [SerializeField] private RectTransform backgroundImage;


        protected override void Init()
        {
            var inventory = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
            backgroundImage.sizeDelta = GetCanvasSize();
        }

        public void RequestReturnToInventory()
        {
            if (OnReturnToInventory != null)
            {
                OnReturnToInventory();
            }
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