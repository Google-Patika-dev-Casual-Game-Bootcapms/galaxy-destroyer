using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devkit.Base.Component;
using Devkit.Base.Object;

namespace SpaceShooterProject.Component
{
    public class InGameInputSystem : IUpdatable, IComponent
    {
        public delegate void TouchMessageDelegate();
        public event TouchMessageDelegate OnScreenTouch; 
        public event TouchMessageDelegate OnScreenTouchEnter; 
        public event TouchMessageDelegate OnScreenTouchExit; 
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
    }
}