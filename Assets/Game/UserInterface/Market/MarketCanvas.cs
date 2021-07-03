namespace SpaceShooterProject.UserInterface
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;

    public class MarketCanvas : BaseCanvas
    {
        [SerializeField] private RectTransform backgroundImage;
        [SerializeField] private GameObject coinEarnScene;


        public int earnedCoin;

        


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
            coinEarnScene.transform.GetChild(4).transform.gameObject.GetComponent<TextMeshProUGUI>().SetText(earnedCoin.ToString());

        }

        public void IsBackgroundActive(bool isActive){
            backgroundImage.gameObject.SetActive(isActive);
        }

        public void IsCoinSceneActive(bool isActive){
            coinEarnScene.SetActive(isActive);
        }
        
        public void IsMarketSceneActive(bool isActive){
            Debug.Log(isActive);
            for( int i=0; i<gameObject.transform.childCount; i++){
                Debug.Log("buna da girdi!");
                transform.GetChild(i).gameObject.SetActive(isActive);
            }
            
        }


    }
}
