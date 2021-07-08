namespace SpaceShooterProject.UserInterface
{
    using UnityEngine;
    using UnityEngine.UI;

    public class MarketCanvas : BaseCanvas
    {
        [SerializeField] private RectTransform backgroundImage;

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

    }
}
