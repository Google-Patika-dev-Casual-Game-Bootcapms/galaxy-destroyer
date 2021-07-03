namespace SpaceShooterProject.UserInterface.Market{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    [RequireComponent(typeof(Animator))]
    public class ChestAnimation : MonoBehaviour
    {
        private Animator animator;

        public void Initialize()
        {
            animator = GetComponent<Animator>();
            animator.SetBool("isOpening", false);
        }

        public void OpenChestAnimation(){
            animator.SetBool("isOpening",true);
        }
        public void Activate()
        {
            gameObject.SetActive(true);
        }

        internal void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public float GetAnimationPlayTime() 
        {
            if (animator == null) 
            {
                return 0;
            }

            var animationClips = animator.runtimeAnimatorController.animationClips;

            if (animationClips.Length == 0) 
            {
                return 0;
            }

            return animationClips[0].length;
        }
    }
}
