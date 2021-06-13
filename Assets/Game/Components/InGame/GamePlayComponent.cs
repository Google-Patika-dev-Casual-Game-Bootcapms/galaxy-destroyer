using Devkit.Base.Component;
using System.Collections;
using System.Collections.Generic;
using Devkit.Base.Object;
using Devkit.HSM;
using UnityEngine;


namespace SpaceShooterProject.Component 
{
    public class GamePlayComponent : MonoBehaviour, IComponent, IUpdatable
    {
        [SerializeField] private Player player;
        private InGameInputSystem inputSystem;

        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>GamePlayComponent initialized!</color>");
            inputSystem = componentContainer.GetComponent("InGameInputSystem") as InGameInputSystem;

            player.InjectInpuSystem(inputSystem);
        }

        public void CallUpdate()
        {
            Debug.Log("GamePlayComponent is on");
            inputSystem.CallUpdate();
            player.CallUpdate();
        }

        public void CallFixedUpdate()
        {
        }

        public void CallLateUpdate()
        {
        }
    }

    public class WeaponUpgradeLevelComponent : IComponent //datayı yönetir
    {
        private const int maxWeaponLevel = 10;
        private const float firePowerUpgradeMultiplier = 0.1f; // Ex: 0.1f : level 1: 1f level 2: 1.1f ..... level 10: 1.9f
        private const float fireRateUpgradeMultiplier = 0.1f;
        private int level = 1; // Level is between 1 to 10

        private float[] weaponFirePowerData;
        private float[] weaponFireRateData;

        public WeaponUpgradeLevelComponent()
        {
            weaponFirePowerData = new float[maxWeaponLevel];
            weaponFireRateData = new float[maxWeaponLevel];

            for(int i = 0; i < maxWeaponLevel; i++)
            {
                weaponFirePowerData[i] = 1f + (i * firePowerUpgradeMultiplier);
                weaponFireRateData[i] = 1f + (i * fireRateUpgradeMultiplier);
            }
        }

        public void SetLevel(int level)
        {
            if(level <= maxWeaponLevel && level >= 1)
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
            return weaponFirePowerData[level-1];
        }

        public float GetCurrentFireRate()
        {
            return weaponFireRateData[level-1];
        }

        public void UpgradeWeaponLevel()
        {
            if(level < maxWeaponLevel)
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
            componentContainer.AddComponent("WeaponUpgradeLevelComponent", this);
        }
    }



}

