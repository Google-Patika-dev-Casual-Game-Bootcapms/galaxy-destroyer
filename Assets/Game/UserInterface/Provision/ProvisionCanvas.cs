namespace SpaceShooterProject.UserInterface
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using TMPro;
    using UnityEngine.UI;
    using Devkit.Base.Component;
    using SpaceShooterProject.Component;
    using UnityEngine.EventSystems;

    public class ProvisionCanvas : BaseCanvas, IComponent
    {
        
        private CurrencyComponent currencyComponent;
        private InventoryComponent inventoryComponent;
        private AccountComponent accountComponent;
        

        public delegate void ProvisionDelegate();
        public delegate void ProvisionSuperPowerDelegate(SuperPowerComponent.SuperPowerType type);
        public event ProvisionDelegate OnNextShipSelectionRequest;
        public event ProvisionDelegate OnPreviousShipSelectionRequest;
        public event ProvisionSuperPowerDelegate OnSuperPowerLaserRequest;
        public event ProvisionSuperPowerDelegate OnSuperPowerShieldRequest;
        public event ProvisionSuperPowerDelegate OnSuperPowerMegaBombRequest;
        public event ProvisionDelegate OnPauseRequest;
        public event ProvisionDelegate OnStartRequest;



        [SerializeField] private RectTransform backgroundImage;
        [SerializeField] private TMP_Text ownedGold, health, power;
        

        protected override void Init()
        {
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
            inventoryComponent = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;
            
            backgroundImage.sizeDelta = GetCanvasSize();
            ownedGold.text = accountComponent.GetOwnedGold().ToString();
        }

        public void RequestNextShip()
        {
            if (OnNextShipSelectionRequest != null)
            {
                Debug.Log("On Next Request Send...");
                OnNextShipSelectionRequest();
            }
        }

        public void RequestPreviousShip()
        {
            if (OnPreviousShipSelectionRequest != null)
            {
                Debug.Log("On Previous Request Send...");
                OnPreviousShipSelectionRequest();
            }
        }

        public void RequestSuperPowerLaser()
        {
            if (OnSuperPowerLaserRequest != null)
            {
                Debug.Log("On Super Power Laser Request Send...");
                OnSuperPowerLaserRequest(SuperPowerComponent.SuperPowerType.Laser);
            }
        }

        public void RequestSuperPowerShield()
        {
            if (OnSuperPowerShieldRequest != null)
            {
                Debug.Log("On Super Power Shield Request Send...");
                OnSuperPowerShieldRequest(SuperPowerComponent.SuperPowerType.Shield);
            }
        }

        public void RequestSuperPowerMegaBomb()
        {
            if (OnSuperPowerMegaBombRequest != null)
            {
                Debug.Log("On Super Power Mega Bomb Request Send...");
                OnSuperPowerMegaBombRequest(SuperPowerComponent.SuperPowerType.MegaBomb);
            }
        }

        public void RequestPause()
        {
            if (OnPauseRequest != null)
            {
                Debug.Log("On Pause Request Send...");
                OnPauseRequest();
            }
        }

        public void RequestStart()
        {
            if (OnStartRequest != null)
            {
                Debug.Log("On Start Request Send...");
                OnStartRequest();
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
