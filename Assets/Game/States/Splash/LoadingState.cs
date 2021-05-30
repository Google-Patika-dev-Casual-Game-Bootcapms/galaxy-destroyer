namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using UnityEngine;

    public class LoadingState : StateMachine
    {
        private const float fakeLoadingTime = 1.2f;
        private float time;

        protected override void OnEnter()
        {
            Debug.Log("Loading State On Enter");
        }

        protected override void OnExit()
        {
            Debug.Log("Loading State On Exit");
        }

        protected override void OnUpdate()
        {
            time += Time.deltaTime;

            if (time > fakeLoadingTime) 
            {
                SendTrigger((int)StateTriggers.SPLASH_COMPLETED);
            }
        }
    }
}


