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
        private Logo patikaLogo;
        [SerializeField]
        private Logo[] appLogo;
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
        }

        public void PlayIntroAnimation()
        {
            StartCoroutine("IntroAnimation");
        }

        private IEnumerator IntroAnimation() 
        {
            loadingIcon.gameObject.SetActive(false);

            var logo = appLogo[UnityEngine.Random.Range(0, appLogo.Length)];

            publisherLogo.PlayFadeInAnimation(animationTime);

            yield return new WaitForSeconds(animationTime);
            
            publisherLogo.PlayFadeOutAnimation(animationTime);

            yield return new WaitForSeconds(animationTime);

            patikaLogo.PlayFadeInAnimation(animationTime);

            yield return new WaitForSeconds(animationTime);

            patikaLogo.PlayFadeOutAnimation(animationTime);

            yield return new WaitForSeconds(animationTime);

            logo.gameObject.SetActive(true);
            logo.PlayFadeInAnimation(animationTime);

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


