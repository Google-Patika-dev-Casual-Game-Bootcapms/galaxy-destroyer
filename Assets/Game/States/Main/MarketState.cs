namespace SpaceShooterProject.State 
{
    using Devkit.Base.Component;
    using Devkit.HSM;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MarketState : StateMachine
    {
        private ComponentContainer componentContainer;

        public MarketState(ComponentContainer componentContainer)
        {
            this.componentContainer = componentContainer;
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

