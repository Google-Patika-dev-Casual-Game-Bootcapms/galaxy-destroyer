namespace SpaceShooterProject.Component
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Devkit.Base.Component;
    using Devkit.Base.Object;

    public class InGameInputSystem : IUpdatable, IComponent, IDestructible, IInitializable
    {
        public delegate void TouchMessageDelegate();
        public event TouchMessageDelegate OnScreenTouch; 
        public event TouchMessageDelegate OnScreenTouchEnter; 
        public event TouchMessageDelegate OnScreenTouchExit;

        private bool isInputAvailable = false;

        public void CallUpdate()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("OnTouchEnter");
                if (OnScreenTouch != null) 
                {
                    OnScreenTouch();
                }
                
                
            }

            if (Input.GetMouseButton(0))
            {
                Debug.Log("Mouse's position="+Input.mousePosition);
                if (OnScreenTouchEnter != null) 
                {
                    OnScreenTouchEnter();
                }
                

            }

            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("Mouse's last position="+Input.mousePosition);
                if (OnScreenTouchExit != null) 
                {
                    OnScreenTouchExit();
                }
            }
        }

        public void CallFixedUpdate()
        {
            
        }

        public void CallLateUpdate()
        {
            
        }

        public void Initialize(ComponentContainer componentContainer)
        {
            
        }

        public void OnDestruct()
        {
            //TODO: finish input system
            isInputAvailable = false;
        }

        public void Init()
        {
            //TODO: initialize the system
            isInputAvailable = true;
        }
    }
}