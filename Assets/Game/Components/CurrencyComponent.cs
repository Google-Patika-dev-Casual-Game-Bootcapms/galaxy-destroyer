namespace SpaceShooterProject.Component
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using Devkit.Base.Component;
    using System;

    public class CurrencyComponent : IComponent
    {
        public delegate void CurrencyChangeDelegate(int currencyCount);
        public event CurrencyChangeDelegate OnGoldChanged;
        public event CurrencyChangeDelegate OnDiamondChanged;

        private AccountComponent accountComponent;
        private int ownedGold;
        private int ownedDiamond;

        public void Initialize(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;

            ownedDiamond = accountComponent.GetOwnedDiamond();
            ownedGold = accountComponent.GetOwnedGold();
        }

        public void EarnGold(int goldIncome)
        {
            ownedGold += goldIncome;
            TriggerGoldCountChange();
        }

        private void TriggerGoldCountChange()
        {
            if (OnGoldChanged != null)
            {
                OnGoldChanged(ownedGold);
            }
        }

        public void EarnDiamond(int diamondIncome)
        {
            ownedDiamond += diamondIncome;
            TriggerDiamondCountChange();
        }

        private void TriggerDiamondCountChange()
        {
            if (OnDiamondChanged != null)
            {
                OnDiamondChanged(ownedDiamond);
            }
        }

        public void SpendGold(int goldOutcome)
        {
            if (!IsGoldAffordable(goldOutcome)) 
            {
                return;
            }

            ownedGold -= goldOutcome;
            TriggerGoldCountChange();
        }

        public bool IsGoldAffordable(int goldAmount)
        {
            return goldAmount <= ownedGold;
        }

        public bool IsDiamondAffordable(int diamondAmount)
        {
            return diamondAmount <= ownedDiamond;
        }

        public void SpendDiamond(int diamondOutcome)
        {
            if (!IsDiamondAffordable(diamondOutcome)) 
            {
                return;
            }

            ownedDiamond -= diamondOutcome;
            TriggerDiamondCountChange();
        }

        public int GetOwnedGold()
        {
            return ownedGold;
        }

        public int GetOwnedDiamond()
        {
            return ownedDiamond;
        }
    }
}