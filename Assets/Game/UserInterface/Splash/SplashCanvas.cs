namespace SpaceShooterProject.UserInterface 
{
    using SpaceShooterProject.Component;
    using System;
    using System.Collections;
    using UnityEngine;

    public class SplashCanvas : BaseCanvas
    {
        private bool isIntroAnimCompleted;

        protected override void Init()
        {
            
        }

        public void PlayIntroAnimation()
        {
            StartCoroutine("IntroAnimation");
        }

        private IEnumerator IntroAnimation() 
        {
            yield return new WaitForSeconds(2);
            isIntroAnimCompleted = true;
        }

        public bool IsIntroCompleted() 
        {
            return isIntroAnimCompleted;
        }
    }
}


