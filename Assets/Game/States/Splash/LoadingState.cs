namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using UnityEngine;

    public class LoadingState : StateMachine
    {
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
            Debug.Log("Loading State On Update");
        }
    }
}


