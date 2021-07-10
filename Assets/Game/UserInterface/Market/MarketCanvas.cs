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

        protected override void Init()
        {
            if (backgroundImage == null) 
            {
                Debug.LogError("Background image in Market Canvas is null!!!");
                return;
            }

            backgroundImage.sizeDelta = GetCanvasSize();

        }

        public void IsBackgroundActive(bool isActive)
        {
            if (backgroundImage == null)
            {
                Debug.LogError("Background image in Market Canvas is null!!!");
                return;
            }

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

        public void CloseCoinScene(){
           IsCoinSceneActive(false); 
           IsMarketSceneActive(true);
        }
    }
}
