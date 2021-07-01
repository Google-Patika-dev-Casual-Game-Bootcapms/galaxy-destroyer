using DG.Tweening;

namespace SpaceShooterProject.UserInterface
{
    using UnityEngine;
    using UnityEngine.UI;
    using SpaceShooterProject.Component;
    using TMPro;

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
                    break;
                case UpgradablePartType.LASER:
                    laserUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.MEGABOMB:
                    megabombUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.MAGNET:
                    magnetUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.HEALTH:
                    healthUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.MISSILE:
                    missileUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.WING_CANNON:
                    wingCannonUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.MAIN_CANNON:
                    mainCannonUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.FIRE_RATE:
                    fireRateUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                case UpgradablePartType.SPEED:
                    speedUpgradeInfo.UpdateMinorAndMajorLevels(level);
                    break;
                default:
                    break;
            }

        }

        public void UpdateUI(SpaceShipUpgradeData spaceShipUpgradeData, int ownedGold) 
        {
            //TODO update UI
            shieldUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.SHIELD]);
            laserUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.LASER]);
            megabombUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MEGABOMB]);
            magnetUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MAGNET]);
            healthUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.HEALTH]);
            missileUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MISSILE]);
            wingCannonUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.WING_CANNON]);
            mainCannonUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.MAIN_CANNON]);
            fireRateUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.FIRE_RATE]);
            speedUpgradeInfo.UpdateMinorAndMajorLevels(spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.SPEED]);
            currentCurrencyContainer.text = ownedGold.ToString();
        }

    }
}


