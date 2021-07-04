using DG.Tweening;

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

    public class ProvisionCanvas : BaseCanvas
    {
        
        private CurrencyComponent currencyComponent;
        private InventoryComponent inventoryComponent;
        private SuperPowerComponent superPowerComponent;
        private AccountComponent accountComponent;
        
        public delegate void RequestPurchaseDelegate(SuperPowerType superPowerType);
        public event RequestPurchaseDelegate OnSuperPowerPurchaseRequest;
        

        public delegate void ProvisionDelegate();
        
        public event ProvisionDelegate OnNextShipSelectionRequest;
        public event ProvisionDelegate OnPreviousShipSelectionRequest;
        public event ProvisionDelegate OnPauseRequest;
        public event ProvisionDelegate OnStartRequest;

        [SerializeField]
        private TextMeshProUGUI[] superPowerPriceTexts;


        [SerializeField] private RectTransform backgroundImage;
        [SerializeField] private TMP_Text ownedGoldText, health, power;

        [SerializeField]
        private GameObject[] progressBarArray;

        protected override void Init()
        {
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
            inventoryComponent = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;
            superPowerComponent = componentContainer.GetComponent("SuperPowerComponent") as SuperPowerComponent;
            
            backgroundImage.sizeDelta = GetCanvasSize();
            ownedGoldText.text = accountComponent.GetOwnedGold().ToString();
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

        public void OnSuperPowerPurchaseButtonClick(int superPowerPartType)
        {
            if (OnSuperPowerPurchaseRequest != null)
            {
                OnSuperPowerPurchaseRequest((SuperPowerType)superPowerPartType);
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

        //TODO: GetCanvasSize() Function is going to add in Base Canvas
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

        // TODO RE-UPDATE UI WHENEVER USER CHANGES CURRENT SELECTED SHIP
        public void UpdateUI(int[] spaceShipSuperPowerData, int ownedGold)
        {
            for (var i = 0; i < (int)SuperPowerType.COUNT; i++)
            {
                progressBarArray[i].transform
                    .DOScaleY(spaceShipSuperPowerData[i] * 0.25f, .1f);
                superPowerPriceTexts[i].text = superPowerComponent.CalculateSuperPowerPrice((SuperPowerType)i).ToString();
            }
            
            ownedGoldText.text = ownedGold.ToString();
        }

        public void OnSuperPowerPurchaseCompleted(SuperPowerPurchaseProcessData superPowerPurchaseProcessData, int ownedGold)
        {
            switch (superPowerPurchaseProcessData.ProcessStatus)
            {
                case SuperPowerProcessStatus.NOT_ENOUGH_GOLD:
                    //TODO: handle
                    break;
                case SuperPowerProcessStatus.MAXIMUM_SUPER_POWER_ITEM_COUNT:
                    //TODO: 
                    break;
                case SuperPowerProcessStatus.SUCCESS:
                    SuperPowerPurchased(superPowerPurchaseProcessData.SuperPowerType);
                    ownedGoldText.text = ownedGold.ToString();
                    ownedGoldText.rectTransform.DOScale(new Vector3(1.1f,1.1f, 1.1f), .1f).SetEase(Ease.InOutBounce).OnComplete(
                        () =>
                        {
                            ownedGoldText.rectTransform.DOScale(new Vector3(1f, 1f, 1f), .1f)
                                .SetEase(Ease.InOutBounce);
                        });
                    break;
                default:
                    break;
            }
        }

        public void SuperPowerPurchased(SuperPowerType superPowerType)
        {
            progressBarArray[(int) superPowerType].transform
                .DOScaleY(progressBarArray[(int) superPowerType].transform.localScale.y + 0.25f, .1f);
        }
    }
}
