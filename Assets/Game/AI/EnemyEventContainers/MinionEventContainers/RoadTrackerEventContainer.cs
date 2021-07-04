namespace SpaceShooterProject.AI
{
    using System;
    public class RoadTrackerEventContainer
    {
        public delegate void MessageDelegate();

        public event MessageDelegate OnActionStateEnter;
        public event MessageDelegate OnActionStateExit;
        public event MessageDelegate OnDeathStateEnter;
        public event MessageDelegate OnDeathStateExit;

        public void TriggerEnterTheDeathState()
        {
            if (OnDeathStateEnter != null)
            {
                OnDeathStateEnter();
            }
        }

        public void TriggerExitFromDeathState()
        {
            if (OnDeathStateExit != null)
            {
                OnDeathStateExit();
            }
        }

        public void TriggerEnterActionState()
        {
            if (OnActionStateEnter != null)
            {
                OnActionStateEnter();
            }
        }

        public void TriggerExitFromActionState()
        {
            if (OnActionStateExit != null)
            {
                OnActionStateExit();
            }
        }
    }
}
    
