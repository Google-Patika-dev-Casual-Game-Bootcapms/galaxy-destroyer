namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;

    public class SettingsState : StateMachine
    {
        private UIComponent uiComponent;
        private SettingsCanvas settingsCanvas;

        public SettingsState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            settingsCanvas = uiComponent.GetCanvas(UIComponent.MenuName.SETTINGS) as SettingsCanvas;
        }

        protected override void OnEnter()
        {
            settingsCanvas.OnReturnToMainMenu += ReturnToMainMenu;
            uiComponent.EnableCanvas(UIComponent.MenuName.SETTINGS);
        }

        private void ReturnToMainMenu()
        {
            SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        protected override void OnExit()
        {
            settingsCanvas.OnReturnToMainMenu -= ReturnToMainMenu;
        }

        protected override void OnUpdate()
        {
            
        }
    }
}


