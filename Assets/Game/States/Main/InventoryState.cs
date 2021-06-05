namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;

    public class InventoryState : StateMachine
    {
        private UIComponent uiComponent;
        private InventoryCanvas inventoryCanvas;

        public InventoryState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            inventoryCanvas = uiComponent.GetCanvas(UIComponent.MenuName.INVENTORY) as InventoryCanvas;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.INVENTORY);
            inventoryCanvas.OnReturnToMainMenu += OnReturnToMainMenu;
            inventoryCanvas.OnCardShowRequest += OnCardShowRequest; //lazımlı
            inventoryCanvas.OnSpaceshipShowRequest += OnSpaceshipShowRequest; //lazımlı
        }

        private void OnReturnToMainMenu()
        {
            SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        //InventoryCanvas çağırıyor
        private void OnCardShowRequest()
        {
            SendTrigger((int)StateTriggers.OPEN_CARD);
        }

        //InventoryCanvas çağırıyor
        private void OnSpaceshipShowRequest()
        {
            SendTrigger((int)StateTriggers.OPEN_SPACESHIP);
        }

        protected override void OnExit()
        {
            inventoryCanvas.OnReturnToMainMenu -= OnReturnToMainMenu;
        }

        protected override void OnUpdate()
        {
            
        }
    }

}

