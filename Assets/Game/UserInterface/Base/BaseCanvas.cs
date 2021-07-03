namespace SpaceShooterProject.UserInterface 
{
    using Devkit.Base.Component;
    using Devkit.Base.Object;
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Canvas))]
    public abstract class BaseCanvas : MonoBehaviour
    {
        public delegate void ReturnToMainMenuDelegate();
        public event ReturnToMainMenuDelegate OnReturnToMainMenu;

        private Canvas canvasComponent = null;
        [SerializeField]
        private ICanvasElement[] canvasElements;    

        protected ComponentContainer componentContainer;


        public void Initialize(ComponentContainer componentContainer)
        {
            this.componentContainer = componentContainer;
            canvasComponent = this.GetComponent<Canvas>();
            canvasElements = transform.GetComponentsInChildren<ICanvasElement>();

            PreInit();
            Init();
        }

        public Canvas GetCanvasComponent()
        {
            return canvasComponent;
        }

        public void Activate()
        {
            canvasComponent.enabled = true;

            for (int i = 0; i < canvasElements.Length; i++)
            {
                if (canvasElements[i] != null)
                {
                    canvasElements[i].Activate();
                }
            }

        }

        public void Deactivate()
        {
            canvasComponent.enabled = false;

            for (int i = 0; i < canvasElements.Length; i++)
            {
                if (canvasElements[i] != null)
                {
                    canvasElements[i].Deactivate();
                }
            }

        }

        protected virtual void PreInit() { }
        protected virtual void Init() 
        {
            for (int i = 0; i < canvasElements.Length; i++)
            {
                canvasElements[i].Init();
            }
        }

        public void ReturnToMainMenu()
        {
            if (OnReturnToMainMenu != null)
            {
                OnReturnToMainMenu();
            }
        }

        protected Vector2 GetCanvasSize()
        {
            Vector2 screenSize = new Vector2(Screen.width, Screen.height);
            CanvasScaler canvasScaler = GetComponent<CanvasScaler>();
            var m_ScreenMatchMode = canvasScaler.screenMatchMode;
            var m_ReferenceResolution = canvasScaler.referenceResolution;
            var m_MatchWidthOrHeight = canvasScaler.matchWidthOrHeight;

            float scaleFactor = 0;
            float logWidth = Mathf.Log(screenSize.x / m_ReferenceResolution.x, 2);
            float logHeight = Mathf.Log(screenSize.y / m_ReferenceResolution.y, 2);
            float logWeightedAverage = Mathf.Lerp(logWidth, logHeight, m_MatchWidthOrHeight);
            scaleFactor = Mathf.Pow(2, logWeightedAverage);

            return new Vector2(screenSize.x / scaleFactor, screenSize.y / scaleFactor);
        }
    }
}


