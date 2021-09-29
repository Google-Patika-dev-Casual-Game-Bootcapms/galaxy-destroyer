
namespace SpaceShooterProject.State
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;

    public class SelectedCardState : StateMachine
    {
        private UIComponent uiComponent;
        private CardCanvas cardCanvas;
        private InventoryComponent inventoryComponent;

        public SelectedCardState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            cardCanvas = uiComponent.GetCanvas(UIComponent.MenuName.CARD) as CardCanvas;
            inventoryComponent = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.CARD);
            cardCanvas.AdjustTheCanvas(inventoryComponent.GetSelectedCardIndex());
            cardCanvas.OnReturnToInventory += OnReturnToInventory;
        }
        private void OnReturnToInventory()
        {
            SendTrigger((int)StateTriggers.RETURN_TO_INVENTORY);
        }

        protected override void OnExit()
        {
            cardCanvas.OnReturnToInventory -= OnReturnToInventory;
        }

        protected override void OnUpdate()
        {

        }
    }
}
