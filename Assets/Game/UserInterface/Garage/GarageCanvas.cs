using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooterProject.UserInterface
{

    public class GarageCanvas : BaseCanvas
    {
        public delegate void RequestUpdateDelegate();

        public event RequestUpdateDelegate OnShieldUpgradeRequest;
        public event RequestUpdateDelegate OnMegaBombUpgradeRequest;
        public event RequestUpdateDelegate OnLaserUpgradeRequest;
        public event RequestUpdateDelegate OnMagnetUpgradeRequest;
        public event RequestUpdateDelegate OnHealthUpgradeRequest;
        public event RequestUpdateDelegate OnMissilesUpgradeRequest;
        public event RequestUpdateDelegate OnWingCannonUpgradeRequest;
        public event RequestUpdateDelegate OnMainCannonUpgradeRequest;

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

            foreach(Button fakeButton in buttons)
            {
                fakeButton.GetComponent<Image>().alphaHitTestMinimumThreshold = 0.1f;
            }
        }

        public void OnShieldUpgradeClick()
        {
            if(OnShieldUpgradeRequest != null)
            {
                OnShieldUpgradeRequest();
            }
        }

        public void OnMegaBombUpgradeClick()
        {
            if (OnMegaBombUpgradeRequest != null)
            {
                OnMegaBombUpgradeRequest();
            }
        }

        public void OnLaserUpgradeClick()
        {
            if (OnLaserUpgradeRequest != null)
            {
                OnLaserUpgradeRequest();
            }
        }
        public void OnMagnetUpgradeClick()
        {
            if (OnMagnetUpgradeRequest != null)
            {
                OnMagnetUpgradeRequest();
            }
        }
        public void OnHealthUpgradeClick()
        {
            if (OnHealthUpgradeRequest != null)
            {
                OnHealthUpgradeRequest();
            }
        }
        public void OnMissilesUpgradeClick()
        {
            if (OnMissilesUpgradeRequest != null)
            {
                OnMissilesUpgradeRequest();
            }
        }
        public void OnWingCannonUpgradeClick()
        {
            if (OnWingCannonUpgradeRequest != null)
            {
                OnWingCannonUpgradeRequest();
            }
        }
        public void OnMainCannonUpgradeClick()
        {
            if (OnMainCannonUpgradeRequest != null)
            {
                OnMainCannonUpgradeRequest();
            }
        }

    }
}


