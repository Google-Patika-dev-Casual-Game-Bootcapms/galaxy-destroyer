namespace SpaceShooterProject.Component 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Devkit.Base.Object;
    using System;
    using UnityEditor;
    using UnityEngine.UIElements;

    public class Player : MonoBehaviour, IUpdatable, IInitializable, IDestructible
    {
        private InGameInputSystem inputSystemReferance ;
        [SerializeField]
        private ObjectPooler ObjectPooler;
        private Transform myTransform;
        private float shipSpeed = 10f;
        private float frameRate = 0;
        private float fireRate = 20;
        
        public void Init()
        {
            
        }
        public void PreInit()
        {
            
        }

        public void CallUpdate()
        {
            frameRate++;
            if (frameRate%fireRate==0)
            {
                Shoot();
            }
            
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

        public void OnTouch()
        {
            var screenPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            gameObject.transform.position = Vector2.MoveTowards(transform.position,
                screenPos,
                shipSpeed * Time.deltaTime);
        
           // var screenLimitX = Screen.width/Screen.currentResolution.width;
           // var screenLimitY = Screen.height/Screen.currentResolution.height;
           // TODO min max ekran değerleri için fonksiyon yazılacak
           
            gameObject.transform.position = new Vector2(Mathf.Clamp(gameObject.transform.position.x,-2.5f,2.5f),
                Mathf.Clamp(gameObject.transform.position.y,-4.5f,4.5f));

        }

        public void InjectInpuSystem(InGameInputSystem inputSystem){
            inputSystemReferance = inputSystem;
            inputSystemReferance.OnScreenTouch += OnScreenTouch;
            inputSystemReferance.OnScreenTouchEnter += OnTouch;
            inputSystemReferance.OnScreenTouchExit += OnTouchUp;
            

        }

        private void OnScreenTouch()
        {
            
        }

        public void OnDestruct()
        {
            inputSystemReferance.OnScreenTouch -= OnScreenTouch;
            inputSystemReferance.OnScreenTouchEnter -= OnTouch;
            inputSystemReferance.OnScreenTouchExit -= OnTouchUp;
        }
        void Shoot()
        {
            GameObject bullet = ObjectPooler.GetPooledObject(0);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
        }
    }
}