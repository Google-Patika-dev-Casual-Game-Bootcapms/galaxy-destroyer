namespace SpaceShooterProject.State
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;

    public class SelectedSpaceshipState : StateMachine
    {
        private UIComponent uiComponent;
        private SpaceshipCanvas spaceshipCanvas;

        public SelectedSpaceshipState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent; //Inventory'den geçmesi gerekiyor
            spaceshipCanvas = uiComponent.GetCanvas(UIComponent.MenuName.SPACESHIP) as SpaceshipCanvas;
        }
        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.SPACESHIP);
            //spaceshipCanvas.OnReturnToInventory += OnReturnToInventory;
        }
        private void OnReturnToInventory()
        {
            //spaceshipCanvas.OnCardShowRequest -= OnCardShowRequest;
            //spaceshipCanvas.OnSpaceshipShowRequest -= OnSpaceshipShowRequest;
        }
        protected override void OnExit()
        {
            //spaceshipCanvas.OnReturnToInventory -= OnReturnToInventory;
        }

        protected override void OnUpdate()
        {

        }
    }
}
