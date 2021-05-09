namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using UnityEngine;

    public class TutorialComponent : IComponent
    {
        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>TutorialComponent is initialized!</color>");
        }
    }
}


