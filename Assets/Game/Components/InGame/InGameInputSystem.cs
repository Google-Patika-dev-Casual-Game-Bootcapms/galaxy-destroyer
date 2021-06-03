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
        public void CallUpdate()
        {
            if(Input.touchCount>0){
                if(OnScreenTouch != null){
                    OnScreenTouch();
                }
            }
        }

        public void Initialize(ComponentContainer componentContainer)
        {
            
        }
    }
}