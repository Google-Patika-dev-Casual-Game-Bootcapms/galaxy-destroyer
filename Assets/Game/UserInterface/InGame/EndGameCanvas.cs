namespace SpaceShooterProject.UserInterface
{
    using UnityEngine.UI;
    using TMPro;
    using UnityEngine;

    public class EndGameCanvas : BaseCanvas
    {
        [SerializeField] private TextMeshProUGUI coinContainer;
        [SerializeField] private TextMeshProUGUI diamondContainer;

        public delegate void EndGameCanvasDelegate();

        public event EndGameCanvasDelegate OnRestartButtonClick;

        protected override void Init()
        {
        }

        public void RequestRestart()
        {
            if (OnRestartButtonClick != null)
                OnRestartButtonClick();
        }

        public void UpdateGoldCount(int coinCount)
        {
            if (coinContainer == null)
            {
                Debug.LogError("Coin container reference is null!!!");
                return;
            }

            coinContainer.text = coinCount.ToString();
        }

    }
}