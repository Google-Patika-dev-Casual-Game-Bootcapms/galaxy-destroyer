namespace SpaceShooterProject.Component
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface IGameCamera
    {
        Vector3 ScreenToWorldPoint(Vector3 position);
        Vector3 ViewportToWorldPoint(Vector3 position);
        Vector3 WorldToViewportPoint(Vector3 position);
    }
}


