namespace SpaceShooterProject.Component
{
    using Devkit.Base.Component;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class SuperPowerComponent : IComponent
    {
        public enum SuperPowerType { Laser, Shield, MegaBomb };

        private AccountComponent accountComponent;

        private SuperPowerData laser;
        private SuperPowerData shield;
        private SuperPowerData megaBomb;
        #region Test
        /*
                public void Start()
                {
                    megaBomb.type = SuperPowerType.MegaBomb;
                    megaBomb.level = 0;
                    megaBomb.maxLevel = 10;

                    megaBomb.upgradePriceArray = new List<int>();
                    laser.upgradePriceArray = new List<int>();
                    shield.upgradePriceArray = new List<int>();

                    for (int i = 0; i < megaBomb.maxLevel; i++)
                    {
                        megaBomb.upgradePriceArray.Add(i * 2);
                    }

                    shield.type = SuperPowerType.Shield;
                    shield.level = 0;
                    shield.maxLevel = 10;
                    for (int i = 0; i < shield.maxLevel; i++)
                    {
                        shield.upgradePriceArray.Add(i * 5);
                    }

                    laser.type = SuperPowerType.Laser;
                    laser.level = 0;
                    laser.maxLevel = 10;
                    for (int i = 0; i < laser.maxLevel; i++)
                    {
                        laser.upgradePriceArray.Add(i * 5);

                    }


                }
                public void Update()
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        UpgradeSuperPower(SuperPowerType.Laser);
                        GetUpgradePrice(SuperPowerType.Laser);
                        UpgradeSuperPower(SuperPowerType.MegaBomb);
                        GetUpgradePrice(SuperPowerType.MegaBomb);
                        UpgradeSuperPower(SuperPowerType.Shield);
                        GetUpgradePrice(SuperPowerType.Shield);
                    }
                }
                */
        #endregion

        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>Super Power Component initialized!</color>");
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;

            // Following methos can be added to Account compenent
            /*
            public SuperPowerData[] GetSuperPowerDatas(){
            return accountData.SuperPowerDatas;
          }
          */

            // TODO: Initializate super power components
            // SuperPowerData[] superPowerDatas = accountComponent.GetSuperPowerDatas();
            // laser = superPowerDatas[0];
            // shield = superPowerDatas[1];
            // megaBomb = superPowerDatas[2];

        }

        public void UpgradeSuperPower(SuperPowerType type)
        {
            switch (type)
            {
                case SuperPowerType.Laser:
                    laser.Upgrade();
                    break;
                case SuperPowerType.Shield:
                    shield.Upgrade();
                    break;
                case SuperPowerType.MegaBomb:
                    megaBomb.Upgrade();
                    break;
                default:
                    break;
            }
        }

        public void GetUpgradePrice(SuperPowerType type)
        {
            switch (type)
            {
                case SuperPowerType.Laser:
                    laser.GetUpgradePrice();

                    break;
                case SuperPowerType.Shield:
                    shield.GetUpgradePrice();
                    break;
                case SuperPowerType.MegaBomb:
                    megaBomb.GetUpgradePrice();
                    break;
                default:
                    break;
            }
        }

        [Serializable]
        public struct SuperPowerData
        {
            public SuperPowerType type { get; set; }
            public int maxLevel { get; set; }
            public int level { get; set; }
            public List<int> upgradePriceArray;

            public void Upgrade()
            {
                if (maxLevel > level)
                {
                    level++;
                    Debug.Log(type + " Level = " + level);
                }
                else
                {
                    Debug.Log(type + ": At the last level you can no longer raise");
                }

            }

            public int GetUpgradePrice()
            {
                Debug.Log(upgradePriceArray[level] + " " + type + "  Price");
                return upgradePriceArray[level];

            }
        }
    }
}