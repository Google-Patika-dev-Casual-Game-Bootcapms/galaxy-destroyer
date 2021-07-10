using Devkit.Base.Object;
using System;
using UnityEngine;

namespace SpaceShooterProject.Component
{
    public class GameCamera : MonoBehaviour, ILateUpdatable
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float cameraSpeed;

        private bool isAvailable = false;


        public void CallLateUpdate()
        {
            mainCamera.transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime, Space.World);
        }

        public bool IsAvailable
        {
            get => isAvailable;
            set => isAvailable = value;
        }

        public float CameraSpeed => cameraSpeed;

        public Camera MainCamera => mainCamera;

        public Vector3 ScreenToWorldPoint(Vector3 mousePosition)
        {
            return mainCamera.ScreenToWorldPoint(mousePosition);
        }
    }
}