using DG.Tweening;

namespace SpaceShooterProject.UserInterface
{
    using UnityEngine;
    using UnityEngine.UI;
    using SpaceShooterProject.Component;
    using TMPro;
    using Devkit.Base.Component;

    public class GarageCanvas : BaseCanvas
    {
        public delegate void RequestUpdateDelegate(UpgradablePartType upgradablePartType);
        public event RequestUpdateDelegate OnPartUpgradeRequest;
      
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


        [SerializeField]
        private TextMeshProUGUI currentCurrencyContainer;

        [SerializeField] 
        private RectTransform backgroundImage;

        private UpgradeComponent upgradeComponent;
        
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

        protected override void Init()
        {
            backgroundImage.sizeDelta = GetCanvasSize();
            upgradeComponent = componentContainer.GetComponent("UpgradeComponent") as UpgradeComponent;
        }


        public void OnUpgradeProcessCompleted(UpgradeProcessData upgradeProcessData, int ownedGold)
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
                    currentCurrencyContainer.text = ownedGold.ToString();
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
                    shieldUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    shieldUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.SHIELD));
                    break;
                case UpgradablePartType.LASER:
                    laserUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    laserUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.LASER));
                    break;
                case UpgradablePartType.MEGABOMB:
                    megabombUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    megabombUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MEGABOMB));
                    break;
                case UpgradablePartType.MAGNET:
                    magnetUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    magnetUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MAGNET));
                    break;
                case UpgradablePartType.HEALTH:
                    healthUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    healthUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.HEALTH));
                    break;
                case UpgradablePartType.MISSILE:
                    missileUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    missileUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MISSILE));
                    break;
                case UpgradablePartType.WING_CANNON:
                    wingCannonUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    wingCannonUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.WING_CANNON));
                    break;
                case UpgradablePartType.MAIN_CANNON:
                    mainCannonUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    mainCannonUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MAIN_CANNON));
                    break;
                case UpgradablePartType.FIRE_RATE:
                    fireRateUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    fireRateUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.FIRE_RATE));
                    break;
                case UpgradablePartType.SPEED:
                    speedUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    speedUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.SPEED));
                    break;
                default:
                    break;
            }

        }

        public void UpdateUI(SpaceShipUpgradeData spaceShipUpgradeData, int ownedGold) 
        {
            //TODO update UI
            shieldUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.SHIELD]);
            shieldUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.SHIELD));

            laserUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.LASER]);
            laserUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.LASER));

            megabombUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MEGABOMB]);
            megabombUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MEGABOMB));

            magnetUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MAGNET]);
            magnetUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MAGNET));

            healthUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.HEALTH]);
            healthUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.HEALTH));

            missileUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MISSILE]);
            missileUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MISSILE));

            wingCannonUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.WING_CANNON]);
            wingCannonUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.WING_CANNON));

            mainCannonUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MAIN_CANNON]);
            mainCannonUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.MAIN_CANNON));

            fireRateUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.FIRE_RATE]);
            fireRateUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.FIRE_RATE));

            speedUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.SPEED]);
            speedUpgradeInfo.updateCost(upgradeComponent.CalculatePartUpgradePrice(UpgradablePartType.SPEED));

            currentCurrencyContainer.text = ownedGold.ToString();

        }

    }
}


