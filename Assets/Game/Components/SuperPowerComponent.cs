namespace SpaceShooterProject.Component
{
    using Devkit.Base.Component;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class SuperPowerComponent : IComponent
    {
        public delegate void SuperPowerProcessDelegate(SuperPowerPurchaseProcessData superPowerProcessData);
        public event SuperPowerProcessDelegate OnSuperPowerProcessCompleted;
        
        private AccountComponent accountComponent;
        private CurrencyComponent currencyComponent;
        
        //public const int MAX_SUPER_POWER_ITEM_COUNT = 4;
        private int[] superPowerPriceWeights;
        
        private int[] superPowerItemCounts;
        
        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>Super Power Component initialized!</color>");
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
            superPowerItemCounts = accountComponent.GetSuperPowerData();
            
            InitializeSuperPowerPriceWeights();
        }
        
        public void PurchaseSuperPower(SuperPowerType superPowerType) 
        {
            if (!accountComponent.IsSuperPowerPurchaseable(superPowerType)) 
            {
                if (OnSuperPowerProcessCompleted != null) 
                {
                    SuperPowerPurchaseProcessData superPowerPurchaseProcessData = new SuperPowerPurchaseProcessData
                    {
                        ProcessStatus = SuperPowerProcessStatus.MAXIMUM_SUPER_POWER_ITEM_COUNT
                    };
                    OnSuperPowerProcessCompleted(superPowerPurchaseProcessData);
                }

                return;
            }

            if (!currencyComponent.IsGoldAffordable(CalculateSuperPowerPrice(superPowerType))) 
            {
                if (OnSuperPowerProcessCompleted != null)
                {
                    SuperPowerPurchaseProcessData superPowerPurchaseProcessData = new SuperPowerPurchaseProcessData
                    {
                        ProcessStatus = SuperPowerProcessStatus.NOT_ENOUGH_GOLD
                    };
                    OnSuperPowerProcessCompleted(superPowerPurchaseProcessData);
                }

                return;
            }

            accountComponent.PurchaseSuperPowerItem(superPowerType);
            currencyComponent.SpendGold(CalculateSuperPowerPrice(superPowerType));

            if (OnSuperPowerProcessCompleted != null)
            {
                SuperPowerPurchaseProcessData superPowerPurchaseProcessData = new SuperPowerPurchaseProcessData
                {
                    ProcessStatus = SuperPowerProcessStatus.SUCCESS,
                    SuperPowerType = superPowerType,
                    CurrentSuperPowerItemCount = accountComponent.GetSuperPowerItemCount(superPowerType)
                };

                OnSuperPowerProcessCompleted(superPowerPurchaseProcessData);
            }
        } 
        
        public int CalculateSuperPowerPrice(SuperPowerType superPowerType) 
        {
            return superPowerPriceWeights[(int)superPowerType];
        }

        private void InitializeSuperPowerPriceWeights()
        {
            superPowerPriceWeights = new int[]
            { 
                20, 30, 40
            };
        }
    }
    
    [Serializable]
    public struct SuperPowerData
    {
        public int[] SuperPowerItemCounts;
        public int MaxItemCount;
    }
    
    public struct SuperPowerPurchaseProcessData
    {
        public SuperPowerProcessStatus ProcessStatus;
        public SuperPowerType SuperPowerType;
        public int CurrentSuperPowerItemCount;
    }
    
    public enum SuperPowerType 
    {
        LASER,
        SHIELD,
        MEGABOMB,
        COUNT
    }
    
    public enum SuperPowerProcessStatus 
    {
        NONE,
        NOT_ENOUGH_GOLD,
        MAXIMUM_SUPER_POWER_ITEM_COUNT,
        SUCCESS
    }
}