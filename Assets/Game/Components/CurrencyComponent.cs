namespace SpaceShooterProject.Component 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Devkit.Base.Component;

    public class CurrencyComponent : IComponent
    {
        #region Variables
            private AccountComponent accountComponent;
            private int OwnedGold,OwnedDiamond;
        #endregion

        public void Initialize(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;

            OwnedDiamond = accountComponent.GetOwnedDiamond();
            OwnedGold = accountComponent.GetOwnedGold();
        }

        #region Currency Methods
            void EarnGold(int goldIncome)
            {
                OwnedGold += goldIncome;
            }

            void EarnDiamond(int diamondIncome)
            {
                OwnedDiamond += diamondIncome;
            }

            void SpendGold(int goldOutcome)
            {
                if(OwnedGold < goldOutcome)
                {
                    //TODO give an error
                }
                else
                {
                    OwnedGold -= goldOutcome;
                }
            }

            void SpendDiamond(int diamondOutcome)
            {
                if(OwnedDiamond < diamondOutcome)
                {
                    //TODO give an error
                }
                else
                {
                    OwnedDiamond -= diamondOutcome;
                }
            }


        #endregion
    
        #region Getter Methods
            int GetOwnedGold()
            {
                return OwnedGold;
            }

            int GetOwnedDiamond()
            {
                return OwnedDiamond;
            }
        #endregion
    }
}