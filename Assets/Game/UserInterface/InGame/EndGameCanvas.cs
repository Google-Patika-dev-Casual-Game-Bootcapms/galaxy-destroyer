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
        public event EndGameCanvasDelegate OnMainMenuButtonClick;
        public event EndGameCanvasDelegate OnSettingsButtonClick;


        protected override void Init()
        {
        }

        public void RequestRestart()
        {
            if (OnRestartButtonClick != null)
                OnRestartButtonClick();
        }
        public void RequestMainMenu()
        {
            if (OnMainMenuButtonClick != null)
                OnMainMenuButtonClick();
        }
        public void RequestSettings()
        {
            if (OnSettingsButtonClick != null)
                OnSettingsButtonClick();
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