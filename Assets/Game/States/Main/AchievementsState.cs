namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using SpaceShooterProject.Component;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class AchievementsState : StateMachine
    {
        private AchievementsComponent achievementsComponent;

        public AchievementsState(ComponentContainer componentContainer) 
        {
            achievementsComponent = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;
        }

        protected override void OnEnter()
        {
            
        }

        protected override void OnExit()
        {
            
        }

        protected override void OnUpdate()
        {
            
        }
    }
}


