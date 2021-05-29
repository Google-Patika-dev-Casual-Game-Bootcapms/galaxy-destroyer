namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class IntroComponent : MonoBehaviour, IComponent
    {

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
            StartCoroutine("SplashAnimation");
            
        }

        public bool IsCompleted()
        {
            return true;
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


