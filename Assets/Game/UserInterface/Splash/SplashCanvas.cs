namespace SpaceShooterProject.UserInterface 
{
    using SpaceShooterProject.Component;
    using System;
    using System.Collections;
    using UnityEngine;

    public class SplashCanvas : BaseCanvas
    {
        [SerializeField]
        private Logo publisherLogo;
        [SerializeField]
        private Logo appLogo;
        [SerializeField]
        private LoadingIcon loadingIcon;

        private const float animationTime = 1.0f;

        private bool isIntroAnimCompleted;

        protected override void Init()
        {
            if (appLogo == null) 
            {
                Debug.LogError("App logo reference is null!!!");
            }

            appLogo.Init();
            appLogo.SetScreenSize(GetCanvasSize());
        }

        public void PlayIntroAnimation()
        {
            StartCoroutine("IntroAnimation");
        }

        private IEnumerator IntroAnimation() 
        {
            loadingIcon.gameObject.SetActive(false);
            appLogo.gameObject.SetActive(false);

            publisherLogo.PlayFadeInAnimation(animationTime);

            yield return new WaitForSeconds(animationTime);
            
            publisherLogo.PlayFadeOutAnimation(animationTime);

            yield return new WaitForSeconds(animationTime);

            appLogo.gameObject.SetActive(true);
            appLogo.PlayFadeInAnimation(animationTime);

            yield return new WaitForSeconds(animationTime);

            isIntroAnimCompleted = true;

            loadingIcon.gameObject.SetActive(true);
            loadingIcon.PlayLoadingAnimation();
        }

        public bool IsIntroCompleted() 
        {
            return isIntroAnimCompleted;
        }
    }
}


