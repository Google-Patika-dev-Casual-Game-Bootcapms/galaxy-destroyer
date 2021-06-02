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
    }
}


