namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using UnityEngine;

    public class UIComponent : MonoBehaviour, IComponent
    {
        private AccountComponent accountComponent;
        //TODO create all menus here!!!
        public void Initialize(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;

            Debug.Log("<color=green>UI Component initialized!</color>");
        }
    }
}


