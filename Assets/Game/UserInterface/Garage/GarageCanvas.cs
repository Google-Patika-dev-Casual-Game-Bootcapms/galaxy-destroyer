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

        [SerializeField]
        private TextMeshProUGUI shieldInfoContainer;
        
        [SerializeField]
        private TextMeshProUGUI currentCurrencyContainer;

        [SerializeField] private RectTransform backgroundImage;

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
            var buttons = GetComponentsInChildren<Button>();

            foreach (Button fakeButton in buttons)
            {
                fakeButton.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
            }
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
                    shieldInfoContainer.text = level.ToString();
                    break;
                case UpgradablePartType.LASER:
                    break;
                case UpgradablePartType.MEGABOMB:
                    break;
                case UpgradablePartType.MAGNET:
                    break;
                case UpgradablePartType.HEALTH:
                    break;
                case UpgradablePartType.MISSILE:
                    break;
                case UpgradablePartType.WING_CANNON:
                    break;
                case UpgradablePartType.MAIN_CANNON:
                    break;
                default:
                    break;
            }

        }

        public void UpdateUI(SpaceShipUpgradeData spaceShipUpgradeData, int ownedGold) 
        {
            //TODO update UI
            shieldInfoContainer.text = spaceShipUpgradeData.PartLevels[(int)UpgradablePartType.SHIELD].ToString();
            currentCurrencyContainer.text = ownedGold.ToString();
        }

    }
}


