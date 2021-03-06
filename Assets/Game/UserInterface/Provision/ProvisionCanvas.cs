using DG.Tweening;
using SpaceShooterProject.Data;

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

        public delegate void ProvisionChangeShipDelegate(bool isNextShip);
        public event ProvisionChangeShipDelegate OnRequestShipChange;
        
        
        public delegate void ProvisionDelegate();
        public event ProvisionDelegate OnPauseRequest;
        public event ProvisionDelegate OnStartRequest;

        [SerializeField]
        private TextMeshProUGUI[] superPowerPriceTexts;


        [SerializeField] private RectTransform backgroundImage;
        [SerializeField] private TMP_Text ownedGoldText, spaceShipNameText, health, power;
        [SerializeField] private Image spaceshipImage;
        [SerializeField] private SpaceShipContainer spaceShipContainer;
        
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

        public void OnRequestShipChangeButtonClick(bool isNextShip)
        {
            if (OnRequestShipChange != null)
            {
                OnRequestShipChange(isNextShip);
            }
        }
        
        public void OnSuperPowerPurchaseButtonClick(int superPowerPartType)
        {
            if (OnSuperPowerPurchaseRequest != null)
            {
                OnSuperPowerPurchaseRequest((SuperPowerType)superPowerPartType);
            }
        }

        public void OnSpaceShipChangeSucces(int shipID)
        {
            spaceshipImage.sprite = spaceShipContainer.GetSpaceShipImage(shipID);
            spaceShipNameText.text = spaceShipContainer.GetSpaceShipName(shipID);
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
        
        public void UpdateUI(int[] spaceShipSuperPowerData, int ownedGold)
        {
            for (var i = 0; i < (int)SuperPowerType.COUNT; i++)
            {
                progressBarArray[i].transform
                    .DOScaleY(spaceShipSuperPowerData[i] * 0.25f, .1f);
                superPowerPriceTexts[i].text = superPowerComponent.CalculateSuperPowerPrice((SuperPowerType)i).ToString();
            }
            
            spaceshipImage.sprite = spaceShipContainer.GetSpaceShipImage(accountComponent.GetSelectedSpaceShipId());
            spaceShipNameText.text = spaceShipContainer.GetSpaceShipName(accountComponent.GetSelectedSpaceShipId());
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
