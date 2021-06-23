namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using SpaceShooterProject.UserInterface.Market;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MarketComponent : MonoBehaviour, IComponent
    {
        [SerializeField]
        private ChestAnimation[] chests;


        public void Initialize(ComponentContainer componentContainer)
        {
            for (int i = 0; i < chests.Length; i++)
            {
                chests[i].Initialize();
            }

        }

        public void OnMarketActivated() 
        {
            for (int i = 0; i < chests.Length; i++)
            {
                chests[i].Activate();
            }
        }

        public void OnMarketDeactivated()
        {
            for (int i = 0; i < chests.Length; i++)
            {
                chests[i].Deactivate();
            }
        }
    }
}


