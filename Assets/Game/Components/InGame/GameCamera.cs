using Devkit.Base.Object;
using System;
using UnityEngine;

namespace SpaceShooterProject.Component
{
    public class GameCamera : MonoBehaviour, ILateUpdatable, IGameCamera
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

        public Vector3 ViewportToWorldPoint(Vector3 position)
        {
            return mainCamera.ViewportToWorldPoint(position);
        }

        public Vector3 WorldToViewportPoint(Vector3 position)
        {
            return mainCamera.WorldToViewportPoint(position);
        }

        public float GetOrtographicSize()
        {
            return mainCamera.orthographicSize;
        }

        public float GetAspect()
        {
            return mainCamera.aspect;
        }
    }
}