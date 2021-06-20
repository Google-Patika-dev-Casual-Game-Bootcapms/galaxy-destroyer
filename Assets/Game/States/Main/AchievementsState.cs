namespace SpaceShooterProject.State
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using SpaceShooterProject.UserInterface;

    public class AchievementsState : StateMachine
    {
        private AchievementsComponent achievementsComponent;
        private UIComponent uiComponent;
        private AchievementsCanvas achievementsCanvas;

        public AchievementsState(ComponentContainer componentContainer)
        {
            achievementsComponent = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;
            uiComponent = componentContainer.GetComponent("UIComponent") as UIComponent;
            achievementsCanvas = uiComponent.GetCanvas(UIComponent.MenuName.ACHIEVEMENTS) as AchievementsCanvas;
        }

        protected override void OnEnter()
        {
            uiComponent.EnableCanvas(UIComponent.MenuName.ACHIEVEMENTS);
            achievementsCanvas.OnReturnToMainMenu += OnReturnToMainMenu;
            achievementsCanvas.AchievementCompleted += AchievementCompleted;
        }

        private void AchievementCompleted(string name)
        {
            achievementsComponent.IsAchievementCompleted(name);
        }

        private void OnReturnToMainMenu()
        {
            SendTrigger((int)StateTriggers.GO_TO_MAIN_MENU_REQUEST);
        }

        protected override void OnExit()
        {
            achievementsCanvas.OnReturnToMainMenu -= OnReturnToMainMenu;
        }

        protected override void OnUpdate()
        {
            for (var i = 0; i < achievementsCanvas.achievementsContentPanel.childCount; i++)
            {
                achievementsCanvas.achievementsContentPanel.transform.GetChild(i).GetComponent<AchievementCard>().Init();
            }
        }
    }
}


