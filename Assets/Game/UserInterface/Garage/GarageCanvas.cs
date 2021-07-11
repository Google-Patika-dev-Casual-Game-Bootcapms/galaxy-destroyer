namespace SpaceShooterProject.UserInterface
{
    using DG.Tweening;
    using UnityEngine;
    using UnityEngine.UI;
    using SpaceShooterProject.Component;
    using TMPro;
    using Devkit.Base.Component;
    using SpaceShooterProject.Data;

    public class GarageCanvas : BaseCanvas
    {
        public delegate void RequestUpdateDelegate(UpgradablePartType upgradablePartType);
        public event RequestUpdateDelegate OnPartUpgradeRequest;

        public delegate void GarageChangeSpaceShipDelegate(bool isNextShip);
        public event GarageChangeSpaceShipDelegate OnRequestSpaceShipChange;

        [SerializeField] private GarageUIHexagonUpgrader shieldUpgradeInfo;
        [SerializeField] private GarageUIHexagonUpgrader laserUpgradeInfo;
        [SerializeField] private GarageUIHexagonUpgrader megabombUpgradeInfo;
        [SerializeField] private GarageUIHexagonUpgrader magnetUpgradeInfo;
        [SerializeField] private GarageUIHexagonUpgrader healthUpgradeInfo;
        [SerializeField] private GarageUIHexagonUpgrader missileUpgradeInfo;
        [SerializeField] private GarageUIHexagonUpgrader wingCannonUpgradeInfo;
        [SerializeField] private GarageUIHexagonUpgrader mainCannonUpgradeInfo;
        [SerializeField] private GarageUIHexagonUpgrader fireRateUpgradeInfo;
        [SerializeField] private GarageUIHexagonUpgrader speedUpgradeInfo;

        [SerializeField] private SpaceShipContainer spaceShipContainer;

        [SerializeField]
        private TextMeshProUGUI currentCurrencyContainer;

        [SerializeField] 
        private RectTransform backgroundImage;

        [SerializeField]
        private Image spaceShipImage;
        [SerializeField]
        private Image spaceShipCodeImage;

        private UpgradeComponent upgradeComponent;

       /* private Vector2 GetCanvasSize()
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

        }*/

        protected override void Init()
        {
            backgroundImage.sizeDelta = GetCanvasSize();
            upgradeComponent = componentContainer.GetComponent("UpgradeComponent") as UpgradeComponent;
        }


        public void OnUpgradeProcessCompleted(UpgradeProcessData upgradeProcessData,int ownedGold)
        {
            switch (upgradeProcessData.ProcessStatus)
            {
                case UpgradeProcessStatus.NOT_ENOUGH_GOLD:
                    //TODO: handle
                    break;
                case UpgradeProcessStatus.MAXIMUM_PART_LEVEL:
                    //TODO: handle
                    break;
                case UpgradeProcessStatus.SUCCESS:
                    PartUpgraded(upgradeProcessData.PartType, upgradeProcessData.CurrentPartLevel);
                   // currentCurrencyContainer.text = ownedGold.ToString(); TODO : Refactor
                    currentCurrencyContainer.rectTransform.DOScale(new Vector3(1.1f,1.1f, 1.1f), .1f).SetEase(Ease.InOutBounce).OnComplete(
                        () =>
                        {
                            currentCurrencyContainer.rectTransform.DOScale(new Vector3(1f, 1f, 1f), .1f)
                                .SetEase(Ease.InOutBounce);
                        });
                    break;
                default:
                    break;
            }
            
        }

        public void OnRequestSpaceShipCahngeButtonClick(bool isNextShip)
        {
            if(OnRequestSpaceShipChange != null)
            {
                OnRequestSpaceShipChange(isNextShip);
            }
        }
        public void OnSpaceShipChangeSucces(int shipID)
        {
            spaceShipImage.sprite = spaceShipContainer.GetSpaceShipImage(shipID);
            spaceShipCodeImage.sprite = spaceShipContainer.GetSpaceShipCodeImage(shipID);
            //spaceShipNameText.text = spaceShipContainer.GetSpaceShipName(shipID);
        }

        public void OnPartUpgradeButtonClick(int upgradablePartType)
        {
            if (OnPartUpgradeRequest != null)
            {
                OnPartUpgradeRequest((UpgradablePartType)upgradablePartType);
            }
        }
        public void PartUpgraded(UpgradablePartType upgradablePartType, int level)
        {
            switch (upgradablePartType)
            {
                case UpgradablePartType.SHIELD:
                    //shieldUpgradeInfo.UpdateMinorAndMajorLevels(level); 
                    break;
                case UpgradablePartType.LASER:
                    //laserUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.MEGABOMB:
                    //megabombUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.MAGNET:
                   // magnetUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.HEALTH:
                    //healthUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.MISSILE:
                   // missileUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.WING_CANNON:
                   // wingCannonUpgradeInfo.UpdateMinorAndMajorLevels(level); 
                    break;
                case UpgradablePartType.MAIN_CANNON:     
                   // mainCannonUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.FIRE_RATE:
                   // fireRateUpgradeInfo.UpdateMinorAndMajorLevels(level);     
                    break;
                case UpgradablePartType.SPEED:
                   // speedUpgradeInfo.UpdateMinorAndMajorLevels(level);   
                    break;
                default:
                    break;
            }

        }

        public void UpdateUI(SpaceShipUpgradeData spaceShipUpgradeData, int ownedGold,int spaceShipID) 
        {
            //TODO update UI
            shieldUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.SHIELD]);
            shieldUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.SHIELD));//TODO : Refactor
            shieldUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.SHIELD), ownedGold);

            laserUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.LASER]);
            laserUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.LASER));
            laserUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.LASER), ownedGold);

            megabombUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MEGABOMB), ownedGold);
            megabombUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MEGABOMB]);
            megabombUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MEGABOMB));

            magnetUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MAGNET), ownedGold);
            magnetUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MAGNET]);
            magnetUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MAGNET));

            healthUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.HEALTH), ownedGold);
            healthUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.HEALTH]);
            healthUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.HEALTH));

            missileUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MISSILE), ownedGold);
            missileUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MISSILE]);
            missileUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MISSILE));

            wingCannonUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.WING_CANNON), ownedGold);
            wingCannonUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.WING_CANNON]);
            wingCannonUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.WING_CANNON));

            mainCannonUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MAIN_CANNON), ownedGold);
            mainCannonUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MAIN_CANNON]);
            mainCannonUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MAIN_CANNON));

            fireRateUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.FIRE_RATE), ownedGold);
            fireRateUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.FIRE_RATE]);
            fireRateUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.FIRE_RATE));

            speedUpgradeInfo.SetImages(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.SPEED), ownedGold);
            speedUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.SPEED]);
            speedUpgradeInfo.UpdateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.SPEED));

            currentCurrencyContainer.text = ownedGold.ToString();
            spaceShipImage.sprite = spaceShipContainer.GetSpaceShipImage(spaceShipID);
            spaceShipCodeImage.sprite = spaceShipContainer.GetSpaceShipCodeImage(spaceShipID);

        }

    }
}


