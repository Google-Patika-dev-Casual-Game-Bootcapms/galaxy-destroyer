namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using UnityEngine;

    public class InGameWeaponUpgradeComponent : IComponent //datayı yönetir
    {
        private const int maxWeaponLevel = 10;
        private const float firePowerUpgradeMultiplier = 0.1f; // Ex: 0.1f : level 1: 1f level 2: 1.1f ..... level 10: 1.9f
        private const float fireRateUpgradeMultiplier = 0.1f;
        private int level = 1; // Level is between 1 to 10

        private float[] weaponFirePowerData;
        private float[] weaponFireRateData;

        public InGameWeaponUpgradeComponent()
        {
            weaponFirePowerData = new float[maxWeaponLevel];
            weaponFireRateData = new float[maxWeaponLevel];

            for (int i = 0; i < maxWeaponLevel; i++)
            {
                weaponFirePowerData[i] = 1f + (i * firePowerUpgradeMultiplier);
                weaponFireRateData[i] = 1f + (i * fireRateUpgradeMultiplier);
            }
        }

        public void SetLevel(int level)
        {
            if (level <= maxWeaponLevel && level >= 1)
            {
                this.level = level;
            }
            else
            {
                Debug.Log("Invalid Weapon Level. ");
            }

        }

        public float GetCurrentFirePower()
        {
            return weaponFirePowerData[level - 1];
        }

        public float GetCurrentFireRate()
        {
            return weaponFireRateData[level - 1];
        }

        public void UpgradeWeaponLevel()
        {
            if (level < maxWeaponLevel)
            {
                level++;
            }
            else
            {
                Debug.Log("Max Level Reached. ");
            }

        }

        public void Initialize(ComponentContainer componentContainer)
        {
            componentContainer.AddComponent("InGameWeaponUpgradeComponent", this);
        }
    }

}

