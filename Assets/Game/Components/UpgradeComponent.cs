namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class UpgradeComponent : IComponent
    {
        //TODO: Her geminin kendine özel bir datası olacak!

        //TODO: Hangi parça ne kadar coin istiyor upgrade için?
        //TODO: WalletComponent'i kullanması gerekecek
        //TODO: Hangi parçanın kaç level olduğu bilgisini isteyeceğiz.


        //TODO(DÜŞÜN!!!): SuperPower komponentini kullanacak! ? ????????

        public delegate void UpgradeProcessDelegate(UpgradeProcessData upgradeProcessData);
        public event UpgradeProcessDelegate OnUpgradeProcessCompleted;

        AccountComponent accountComponent;
        CurrencyComponent currencyComponent;

        private int[] partPriceWeights;

        public void Initialize(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;

            InitializePartPriceWeights();
        }

        public void UpgradePart(UpgradablePartType upgradablePartType) 
        {
            if (!accountComponent.IsPartUpgradable(upgradablePartType)) 
            {
                if (OnUpgradeProcessCompleted != null) 
                {
                    UpgradeProcessData upgradeProcessData = new UpgradeProcessData
                    {
                        ProcessStatus = UpgradeProcessStatus.MAXIMUM_PART_LEVEL
                    };

                    OnUpgradeProcessCompleted(upgradeProcessData);
                }

                return;
            }

            if (!currencyComponent.IsGoldAffordable(CalculatePartUpgradePrice(upgradablePartType))) 
            {
                if (OnUpgradeProcessCompleted != null)
                {
                    UpgradeProcessData upgradeProcessData = new UpgradeProcessData
                    {
                        ProcessStatus = UpgradeProcessStatus.NOT_ENOUGH_GOLD
                    };

                    OnUpgradeProcessCompleted(upgradeProcessData);
                }

                return;
            }

            accountComponent.UpgradePart(upgradablePartType);
            currencyComponent.SpendGold(CalculatePartUpgradePrice(upgradablePartType));

            if (OnUpgradeProcessCompleted != null)
            {
                UpgradeProcessData upgradeProcessData = new UpgradeProcessData
                {
                    ProcessStatus = UpgradeProcessStatus.SUCCESS,
                    PartType = upgradablePartType,
                    CurrentPartLevel = accountComponent.GetPartLevel(upgradablePartType)
                };

                OnUpgradeProcessCompleted(upgradeProcessData);
            }
        }

        public int CalculatePartUpgradePrice(UpgradablePartType upgradablePartType) 
        {
            return partPriceWeights[(int)upgradablePartType] * accountComponent.GetPartLevel(upgradablePartType);
        }

        private void InitializePartPriceWeights()
        {
            partPriceWeights = new int[]
            { 
                1, 3, 4, 1, 2, 2, 4, 6
            };

        }
    }

    public enum UpgradablePartType 
    { 
        SHIELD,
        LASER,
        MEGABOMB,
        MAGNET,
        HEALTH,
        MISSILE,
        WING_CANNON,
        MAIN_CANNON,
        FIRE_RATE,
        SPEED,
        COUNT
    }

    public struct UpgradeProcessData
    {
        public UpgradeProcessStatus ProcessStatus;
        public UpgradablePartType PartType;
        public int CurrentPartLevel;
    }

    public enum UpgradeProcessStatus 
    {
        NONE,
        NOT_ENOUGH_GOLD,
        MAXIMUM_PART_LEVEL,
        SUCCESS
    }
}
