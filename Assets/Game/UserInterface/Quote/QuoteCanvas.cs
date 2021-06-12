namespace SpaceShooterProject.UserInterface 
{
    public class QuoteCanvas : BaseCanvas
    {
        public delegate void QuoteRequestDelegate();
        public event QuoteRequestDelegate OnInGameMenuRequest;

        protected override void Init()
        {

        }


        public void RequestInGameMenu() 
        {
            if (OnInGameMenuRequest != null) 
            {
                OnInGameMenuRequest();
            }
        }
    }
}
