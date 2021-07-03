namespace SpaceShooterProject.UserInterface
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using SpaceShooterProject.UserInterface.Market;
    using System.Collections;

    public class MarketCanvas : BaseCanvas
    {
        public delegate void ChestOpenAnimationDelegate();
        public event ChestOpenAnimationDelegate OnChestOpenAnimationComplete;

        [SerializeField] private RectTransform backgroundImage;
        [SerializeField] private Canvas earnRewardCanvas;
        [SerializeField] private TextMeshProUGUI gainedGoldAmountContainer;

        public int earnedCoin;

        private bool isChestOpened = false;


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

        public void IsCoinSceneActive(bool isActive)
        {
            earnRewardCanvas.enabled = isActive;
        }
        
        public void IsMarketSceneActive(bool isActive){
            
            if (isActive)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }

        }

        public void PlayChestOpenAnimation(ChestAnimation chest)
        {
            StartCoroutine("PlayChestAnimation", chest);
        }

        private IEnumerator PlayChestAnimation(ChestAnimation chest) 
        {
            if(chest)
            {
                chest.OpenChestAnimation();
                isChestOpened = true;
            }

            const float animationDelay = 1.0f;

            gainedGoldAmountContainer.text = earnedCoin.ToString();

            yield return new WaitForSeconds(chest.GetAnimationPlayTime() + animationDelay);

            if (OnChestOpenAnimationComplete != null) 
            {
                OnChestOpenAnimationComplete();
            }

            isChestOpened = false;
        }

        public void SetGainedCoinAmount(int gainedGold)
        {
            earnedCoin = gainedGold;
        }
    }
}
