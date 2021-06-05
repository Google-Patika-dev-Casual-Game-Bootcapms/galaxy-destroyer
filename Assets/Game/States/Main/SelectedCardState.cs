
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

        public SelectedCardState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent; //Inventory'den geçmesi gerekiyor
            cardCanvas = uiComponent.GetCanvas(UIComponent.MenuName.CARD) as CardCanvas;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.CARD);
            //cardCanvas.OnReturnToInventory += OnReturnToInventory;
        }
        private void OnReturnToInventory()
        {
            //cardCanvas.OnCardShowRequest -= OnCardShowRequest;
            //cardCanvas.OnSpaceshipShowRequest -= OnSpaceshipShowRequest;
        }
        protected override void OnExit()
        {
            //cardCanvas.OnReturnToInventory -= OnReturnToInventory;
        }

        protected override void OnUpdate()
        {

        }
    }
}
