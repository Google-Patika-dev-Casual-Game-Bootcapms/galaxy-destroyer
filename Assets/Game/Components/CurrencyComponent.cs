namespace SpaceShooterProject.Component 
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Devkit.Base.Component;
    using System;

    public class CurrencyComponent : IComponent
    {
            private AccountComponent accountComponent;
            private int ownedGold,ownedDiamond;
            private bool isGoldEnough,isDiamondEnough;

        public void Initialize(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;

            ownedDiamond = accountComponent.GetOwnedDiamond();
            ownedGold = accountComponent.GetOwnedGold();
        }

        #region Currency Methods
            public void EarnGold(int goldIncome)
            {
                ownedGold += goldIncome;
            }

            public void EarnDiamond(int diamondIncome)
            {
                ownedDiamond += diamondIncome;
            }

            public bool SpendGold(int goldOutcome)
            {
                if(ownedGold < goldOutcome)
                {
                    //TODO give an error
                    isGoldEnough = false;
                }
                else
                {
                    ownedGold -= goldOutcome;
                    isGoldEnough=true;
                }
                
                return isGoldEnough;
            }

        public bool IsAffordable(int amount)
        {
            return amount <= ownedGold;
        }

        public bool SpendDiamond(int diamondOutcome)
            {
                if(ownedDiamond < diamondOutcome)
                {
                    //TODO give an error
                    isDiamondEnough=false;
                }
                else
                {
                    ownedDiamond -= diamondOutcome;
                    isDiamondEnough = true;
                }

                return isDiamondEnough;
            }


        #endregion
    
        #region Getter Methods
            int GetOwnedGold()
            {
                return ownedGold;
            }

            int GetOwnedDiamond()
            {
                return ownedDiamond;
            }
        #endregion
    }
}