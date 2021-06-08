using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Devkit.Base.Object;
using System;

namespace SpaceShooterProject.Component {
    public class Player : MonoBehaviour, IUpdatable, IInitializable, IDestructible
    {
        private InGameInputSystem inputSystemReferance ;
<<<<<<< Updated upstream
        [SerializeField]
        private ObjectPooler ObjectPooler;
       
        private Transform myTransform;
=======
<<<<<<< HEAD
        private ScreenBounds screenBounds;
        private float shipSpeed = 1000f;
      
>>>>>>> Stashed changes

        public void Init()
        {
          
=======
        [SerializeField]
        private ObjectPooler ObjectPooler;
       
        private Transform myTransform;

        public void Init()
        {
           
        
            InvokeRepeating("Shoot", .33f, .33f);
<<<<<<< Updated upstream
=======
>>>>>>> 38c4454ee6ad82c5611e617453ce845492b0f912
>>>>>>> Stashed changes
        }
        public void PreInit()
        {
            
        }

        public void CallUpdate()
        {
<<<<<<< Updated upstream
            Shoot();
            myTransform = transform;
=======
<<<<<<< HEAD

=======
            Shoot();
            myTransform = transform;
>>>>>>> 38c4454ee6ad82c5611e617453ce845492b0f912
>>>>>>> Stashed changes
        }
        
        public void OnTouchUp()
        {
            
            
        }

        public void OnTouchEnter()
        {
            gameObject.transform.position = Vector2.MoveTowards(transform.position,
                                                                Input.mousePosition,
<<<<<<< Updated upstream
                                                                1000f * Time.deltaTime);

=======
<<<<<<< HEAD
                                                                shipSpeed * Time.deltaTime);
            screenBounds.Bounds();                                                   
        
=======
                                                                1000f * Time.deltaTime);

>>>>>>> 38c4454ee6ad82c5611e617453ce845492b0f912
>>>>>>> Stashed changes
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