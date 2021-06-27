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
        private AudioComponent audioComponent;

        public SettingsState(ComponentContainer componentContainer)
        {
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            settingsCanvas = uiComponent.GetCanvas(UIComponent.MenuName.SETTINGS) as SettingsCanvas;

            audioComponent = componentContainer.GetComponent("AudioComponent") as AudioComponent;
        }

        protected override void OnEnter()
        {
            settingsCanvas.OnReturnToMainMenu += ReturnToMainMenu;
            settingsCanvas.OnVolumeValueChanged += RequestVolumeChange;
            uiComponent.EnableCanvas(UIComponent.MenuName.SETTINGS);
        }

        private void ReturnToMainMenu()
        {
            SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        private void RequestVolumeChange(float volumeValue)
        {
            //TODO: Audio Component içinden Volume'u deðiþtirebileceðimiz bir method yazýlmasý lazým.
            //audioComponent.AudioControl(volumeValue);
        }

        protected override void OnExit()
        {
            settingsCanvas.OnReturnToMainMenu -= ReturnToMainMenu;
            settingsCanvas.OnVolumeValueChanged -= RequestVolumeChange;
        }

        protected override void OnUpdate()
        {
        }
    }
}