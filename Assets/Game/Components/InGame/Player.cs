using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devkit.Base.Object;
using System;

namespace SpaceShooterProject.Component {
    public class Player : MonoBehaviour, IUpdatable, IInitializable, IDestructible
    {
        private InGameInputSystem inputSystemReferance ;
        [SerializeField]
        private ObjectPooler ObjectPooler;
       
        private Transform myTransform;

        public void Init()
        {
           
        
            InvokeRepeating("Shoot", .33f, .33f);
        }
        public void PreInit()
        {
            
        }

        public void CallUpdate()
        {
            Shoot();
            myTransform = transform;
        }
        
        public void OnTouchUp()
        {
            
            
        }

        public void OnTouchEnter()
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position,
                                                                Input.mousePosition,
                                                                1000f * Time.deltaTime);

        }

        public void InjectInpuSystem(InGameInputSystem inputSystem){
            inputSystemReferance = inputSystem;
            inputSystemReferance.OnScreenTouch += OnScrenTouch;
            inputSystemReferance.OnScreenTouchEnter += OnTouchEnter;
            inputSystemReferance.OnScreenTouchExit += OnTouchUp;
            

        }

        private void OnScrenTouch()
        {
            
        }

        public void OnDestruct()
        {
            inputSystemReferance.OnScreenTouch -= OnScrenTouch;
            inputSystemReferance.OnScreenTouchEnter -= OnTouchEnter;
            inputSystemReferance.OnScreenTouchExit -= OnTouchUp;
        }
        void Shoot()
        {
            Debug.Log("a");
            GameObject bullet = ObjectPooler.GetPooledObject(0);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        }

        
    }
}