namespace SpaceShooterProject.UserInterface 
{
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;

    public class Logo : MonoBehaviour, ICanvasElement
    {
        [SerializeField]
        private Image image;

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }

        public void PlayFadeInAnimation(float duration) 
        {
            if (image == null) 
            {
                return;
            }

            image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 1), duration).SetEase(Ease.InOutSine);
        }

        public void PlayFadeOutAnimation(float duration)
        {
            if (image == null)
            {
                return;
            }

            image.DOColor(new Color(image.color.r, image.color.g, image.color.b, 0), duration).SetEase(Ease.InOutSine);
        }

    }
}


