namespace SpaceShooterProject.UserInterface
{
    using UnityEngine;
    using UnityEngine.UI;

    public class MarketCanvas : BaseCanvas
    {
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
        }

        public void IsBackgroundActive(bool isActive){
            backgroundImage.gameObject.SetActive(isActive);
        }

    }
}
