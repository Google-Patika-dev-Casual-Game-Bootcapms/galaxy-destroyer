namespace SpaceShooterProject.UserInterface 
{
    using UnityEngine;
    using UnityEngine.UI;
    using DG.Tweening;
    using System;

    public class Logo : MonoBehaviour, ICanvasElement
    {
        [SerializeField]
        private Image image;
        [SerializeField]
        private Sprite[] sourceSprites;
        private RectTransform imageRectTransform;

        public void Activate()
        {
            
        }

        public void Deactivate()
        {
            
        }

        public void Init()
        {
            ChooseRandomLogo();
            image.SetNativeSize();
            var imageRect = image.GetComponent<RectTransform>();
        }

        private void ChooseRandomLogo()
        {
            if (sourceSprites.Length == 0) 
            {
                return;
            }

            if (sourceSprites.Length == 1) 
            {
                image.sprite = sourceSprites[0];
                return;
            }

            image.sprite = sourceSprites[UnityEngine.Random.Range(0, sourceSprites.Length)];
        }

        public void SetScreenSize(Vector2 vector2)
        {
            image.GetComponent<RectTransform>().sizeDelta = new Vector2();
        }

        public void SetWidth(float width, bool preserveAspect)
        {
            if (preserveAspect)
            {
                var currentHeight = imageRectTransform.sizeDelta.y;
                var currentWidth = imageRectTransform.sizeDelta.x;
                var futureWidth = width;
                var futureHeight = (currentHeight * futureWidth) / currentWidth;

                imageRectTransform.sizeDelta = new Vector2(futureWidth, futureHeight);
            }
            else
            {
                imageRectTransform.sizeDelta = new Vector2(width, imageRectTransform.sizeDelta.y);
            }

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


