namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using UnityEngine;
    public class NotificationComponent : IComponent
    {
        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>Notification Component initialized!</color>");
        }
    }
}


