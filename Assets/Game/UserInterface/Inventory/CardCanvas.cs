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
    }
}