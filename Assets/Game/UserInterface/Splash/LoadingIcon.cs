namespace SpaceShooterProject.UserInterface 
{
    using System.Collections;
    using UnityEngine;

    public class LoadingIcon : MonoBehaviour, ICanvasElement
    {
        private const string ANIMATION_COROUTINE_NAME = "PlayRotateAnimation";
        private Vector3 rotationVector = new Vector3(0, 0, -60);
        private Transform iconTransform;

        public void Activate()
        {
           
        }

        public void Deactivate()
        {
            StopCoroutine(ANIMATION_COROUTINE_NAME);
        }

        public void Init()
        {
            
        }

        public void PlayLoadingAnimation() 
        {
            StartCoroutine(ANIMATION_COROUTINE_NAME);
        }

        private IEnumerator PlayRotateAnimation()
        {
            if (iconTransform == null)
            {
                iconTransform = GetComponent<Transform>();
            }

            while (true)
            {
                iconTransform.Rotate(Time.deltaTime * rotationVector);
                yield return null;
            }
        }
    }
}


