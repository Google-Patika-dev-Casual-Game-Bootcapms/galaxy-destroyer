using Devkit.Base.Object;
using UnityEngine;

namespace SpaceShooterProject.Component
{
    public class GameCamera : MonoBehaviour, IUpdatable
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private float cameraSpeed;


        public void CallUpdate()
        {
            mainCamera.transform.Translate(Vector3.up * cameraSpeed * Time.deltaTime, Space.World);
        }
    }
}