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
           
        }

        public void CallFixedUpdate()
        {
            
        }

        public void CallLateUpdate()
        {
            
        }

        public void OnTouchUp()
        {
            
            
        }

        public void OnTouchEnter()
        {
            var screenPoz = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = Vector2.MoveTowards(transform.position,
                screenPoz,
                10f * Time.deltaTime);
        
           // var screenLimitX = Screen.width/Screen.currentResolution.width;
           // var screenLimitY = Screen.height/Screen.currentResolution.height;
           // TODO min max ekran değerleri için fonksiyon yazılacak
           
            gameObject.transform.position = new Vector2(Mathf.Clamp(gameObject.transform.position.x,-2.5f,2.5f),
                Mathf.Clamp(gameObject.transform.position.y,-4.5f,4.5f));

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