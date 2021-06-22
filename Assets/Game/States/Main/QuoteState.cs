namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;

    public class QuoteState : StateMachine
    {
        private UIComponent uiComponent;
        private QuoteCanvas quoteCanvas;

        public QuoteState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            quoteCanvas = uiComponent.GetCanvas(UIComponent.MenuName.QUOTE) as QuoteCanvas;
        }

        protected override void OnEnter()
        {
            quoteCanvas.OnInGameMenuRequest += OnInGameMenuRequest;
            uiComponent.EnableCanvas(UIComponent.MenuName.QUOTE);
            
        }

        private void OnInGameMenuRequest()
        {
            SendTrigger((int)StateTriggers.START_GAME_REQUEST);
        }

        protected override void OnExit()
        {
            quoteCanvas.OnInGameMenuRequest -= OnInGameMenuRequest;

        }

        protected override void OnUpdate()
        { }
    }
}

