namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using SpaceShooterProject.UserInterface;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class IntroComponent : MonoBehaviour, IComponent
    {
        private UIComponent uiComponent;
        private SplashCanvas splashCanvas;

        [SerializeField]
        private UIComponent uiComponent;
        [SerializeField]
        private LoadingIcon loadingIcon;
        [SerializeField]
        private Logo logo;
        [SerializeField]
        private KodluyoruzLogo kodluyoruzLogo;

        public void Initialize(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            splashCanvas = uiComponent.GetCanvas(UIComponent.MenuName.SPLASH) as SplashCanvas;
        }

        public bool IsIntroAnimationCompleted()
        {
            return splashCanvas.IsIntroCompleted();
        }

        public void StartIntro()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.SPLASH);
            splashCanvas.PlayIntroAnimation();
        }

        private IEnumerator SplashAnimation()
        {
            kodluyoruzLogo.StartCoroutine("KodluyoruzLogoAnimation");
            yield return new WaitForSeconds(3f);
            logo.StartCoroutine("GalaxyLogoAnimation");
            yield return new WaitForSeconds(3f);
            loadingIcon.gameObject.SetActive(true);
        }

    }
}


