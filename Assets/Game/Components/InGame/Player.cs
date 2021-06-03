using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devkit.Base.Object;
using System;

namespace SpaceShooterProject.Component {
    public class Player : MonoBehaviour, IUpdatable, IInitializable, IDestructible
    {
        private InGameInputSystem inputSystemReferance ;
        public void Init()
        {
            throw new System.NotImplementedException();
        }
        public void PreInit()
        {
            throw new System.NotImplementedException();
        }

        public void CallUpdate()
        {
            throw new System.NotImplementedException();
        }
        public void OnTouchUp()
        {
            
        }

        public void InjectInpuSystem(InGameInputSystem inputSystem){
            inputSystemReferance = inputSystem;
            inputSystemReferance.OnScreenTouch += OnScrenTouch;

        }

        private void OnScrenTouch()
        {
            //TODO Move
        }

        public void OnDestruct()
        {
            inputSystemReferance.OnScreenTouch -= OnScrenTouch;
        }

        
    }
}