using System.Collections.Generic;

namespace SpaceShooterProject.Component 
{
    using System;
    using System.IO;
    using Devkit.Base.Component;
    using Devkit.Base.Object;
    using UnityEngine;

    public class AccountComponent : IComponent, IDestructible
    {
        private const int MAX_SUPER_POWER_ITEM_COUNT = 4;
        private const int MAX_SPACESHIP_COUNT = 6;
        private const int MAX_PART_UPGRADE_LEVEL = 41;

        #region Variables        
        private AccountData accountData;
        private AchievementsComponent achievementsComponent;
        private GamePlayComponent gamePlayComponent;
        private SuperPowerComponent superPowerComponent;
        private CurrencyComponent currencyComponent;
        private AudioComponent audioComponent;

        //TODO: Add inventory system reference when inventory component created!!!
            // private InventoryComponent inventoryComponent;

        //TODO: Add copilot system reference when inventory component created!!!
            // private CopilotComponent copilotComponent;

        private string accountDataPath;
        private string accountDataFile;

        private LoadComponent loadComponent;
        private SaveComponent saveComponent;

        #endregion        

        public void Initialize(ComponentContainer componentContainer)
        {
            achievementsComponent = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;
            gamePlayComponent = componentContainer.GetComponent("GamePlayComponent") as GamePlayComponent;
            superPowerComponent = componentContainer.GetComponent("SuperPowerComponent") as SuperPowerComponent;
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
            audioComponent = componentContainer.GetComponent("AudioComponent") as AudioComponent;
            // TODO: Activate below components when they are created
                // inventoryComponent = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
                // copilotComponent = componentContainer.GetComponent("CopilotComponent") as CopilotComponent;
            
            //Debug.Log("<color=green>Account Component initialized!</color>");

            accountDataFile = "accountData.txt";
#if UNITY_EDITOR
            accountDataPath = Application.dataPath + "/" + accountDataFile;
#else
            accountDataPath = Application.persistentDataPath + "/" + accountDataFile;
#endif

            Debug.Log("<color=red>" + accountDataPath + "</color>");
            
            loadComponent = new LoadComponent();
            saveComponent = new SaveComponent();

            if(File.Exists(accountDataPath)){
                //Debug.Log("File exists and successfully read.");
                accountData = loadComponent.Load<AccountData>(accountDataPath);
            } else {
                //Debug.Log("Initialize for first time");
                InitializeForFirstTime();
            }

            InitializeSpaceShipUpgradeData();

            //Debug.Log("Name:" + GetPlayerName());

        }

        private void InitializeForFirstTime(){

            accountData = new AccountData
            {
                Name = "Name", // TODO: Ask for name to the user
                PlayerLevel = 1,
                OwnedGold = 1000,
                SelectedSpaceShipId = 0
            };

            InitializeSpaceShipUpgradeData();
            InitializeSuperPowerData();
            
            // TODO: Assign default values for other components in the future
            
            saveComponent.Save(accountData, accountDataPath);
        }

        private void InitializeSpaceShipUpgradeData()
        {
            
            accountData.SpaceShipUpgradeDatas = new SpaceShipUpgradeData[MAX_SPACESHIP_COUNT];

            for (int i = 0; i < accountData.SpaceShipUpgradeDatas.Length; i++)
            {
                accountData.SpaceShipUpgradeDatas[i].PartLevels = new int[(int)UpgradablePartType.COUNT];
            }
        }
        
        private void InitializeSuperPowerData()
        {
           
            accountData.SuperPowerItemCounts = new int[(int) SuperPowerType.COUNT];
            
            for (int i = 0; i < (int) SuperPowerType.COUNT; i++)
            {
                accountData.SuperPowerItemCounts[i] = 0;
            }
        }
        

        public void SaveBeforeClosing(){
            // TODO: Activate below when corresponding methods are created.
            
            /*accountData.CompletedAchievements = achievementsComponent.GetCompletedAchievements();
            accountData.LastReachedLevel = gamePlayComponent.GetLastReachedComponent();
            accountData.MaxScore = gamePlayComponent.GetMaxScore();
            accountData.OwnedSpaceShips = inventoryComponent.GetOwnedSpaceShips();
            accountData.SpaceShipUpgradeDatas = inventoryComponent.GetSpaceShipUpgradeDatas();
            accountData.OwnedCards = inventoryComponent.GetOwnedCards();
            accountData.OwnedPowerUps = inventoryComponent.GetOwnedPowerUps();
            accountData.LastSelectedSpaceShip = inventoryComponent.GetLastSelectedSpaceShip();
            accountData.AudioSetting = audioComponent.GetAudioSetting();
            accountData.OwnedGold = currencyComponent.GetOwnedGold();
            accountData.OwnedDiamond = currencyComponent.GetOwnedDiamond();
            accountData.CopilotSetting = copilotComponent.GetCopilotSetting();
            */
            
            accountData.OwnedGold = currencyComponent.GetOwnedGold();
            
            saveComponent.Save(accountData, accountDataPath);
        }

        // Created by Inventory Component to check if JSON file exists in the given directory
        public bool IsFileExist()
        {
            return File.Exists(accountDataPath);
        }

#region Getter Methods for Account Data
        public string GetPlayerName() {
            return accountData.Name;
        }

        public int GetPlayerLevel(){
            return accountData.PlayerLevel;
        }

        public int[] GetCompletedAchievements(){
            return accountData.CompletedAchievements;
        }

        public int GetLastReachedLevel() {
            return accountData.LastReachedLevel;
        }

        public int GetMaxScore() {
            return accountData.MaxScore;
        }

        public List<int> GetOwnedSpaceShips(){
            return accountData.OwnedSpaceShips;
        }

        public List<int> OwnedTemporalCards()
        {
            return accountData.OwnedTemporalCards;
        }

        public List<int> OwnedPermanentCards()
        {
            return accountData.OwnedPermanentCards;
        }

        public int[] CollectedSpaceShipParts()
        {
            return accountData.CollectedSpaceShipParts;
        }

        public SpaceShipUpgradeData[] GetSpaceShipUpgradeDatas(){
            return accountData.SpaceShipUpgradeDatas;
        }

        public  SpaceShipUpgradeData GetCurrentSpaceShipUpgradePartData() 
        {
            return accountData.SpaceShipUpgradeDatas[accountData.SelectedSpaceShipId];
        }

        public int[] GetOwnedCards(){
            return accountData.OwnedCards;
        }

        public int[] GetOwnedPowerUps(){
            return accountData.OwnedPowerUps;
        }

        public int GetAudioLevel() {
            return accountData.AudioLevel;
        }

        public int GetOwnedGold() {
            return accountData.OwnedGold;
        }

        public int GetOwnedDiamond() {
            return accountData.OwnedDiamond;
        }

        public int[] GetCopilotSetting(){
            return accountData.CopilotSetting;
        }

        public bool IsPartUpgradable(UpgradablePartType upgradablePartType)
        {
            return accountData.SpaceShipUpgradeDatas[accountData.SelectedSpaceShipId].
                PartLevels[(int)upgradablePartType] < MAX_PART_UPGRADE_LEVEL;
        }

        public void UpgradePart(UpgradablePartType upgradablePartType) 
        {
            if (!IsPartUpgradable(upgradablePartType)) 
            {
                return;
            }
            
            accountData.SpaceShipUpgradeDatas[accountData.SelectedSpaceShipId].PartLevels[(int)upgradablePartType]++;
       }

        public int GetPartLevel(UpgradablePartType upgradablePartType)
        {
            return accountData.SpaceShipUpgradeDatas[accountData.SelectedSpaceShipId].PartLevels[(int)upgradablePartType];
        }
        
        public bool IsSuperPowerPurchaseable(SuperPowerType superPowerType)
        {
            return accountData.SuperPowerItemCounts[(int)superPowerType] < MAX_SUPER_POWER_ITEM_COUNT; 
        }
        
        public void PurchaseSuperPowerItem(SuperPowerType superPowerType)
        {
            if (!IsSuperPowerPurchaseable(superPowerType)) 
            {
                return;
            }
            accountData.SuperPowerItemCounts[(int)superPowerType]++;
        }
        
        public int GetSuperPowerItemCount(SuperPowerType superPowerType){
            return accountData.SuperPowerItemCounts[(int)superPowerType];
        }
        
        // TODO: refactor name
        public int[] GetSuperPowerData() 
        {
            return accountData.SuperPowerItemCounts;
        }

        public int GetMaxSpaceshipCount()
        {
            return MAX_SPACESHIP_COUNT;
        }

        public int GetSelectedSpaceShipId()
        {
            return accountData.SelectedSpaceShipId;
        }
        
        public void SetSelectedSpaceShipId(int newSelectedSpaceShip)
        {
            accountData.SelectedSpaceShipId = newSelectedSpaceShip;
        }

        
        public void OnDestruct()
        {
            SaveBeforeClosing();
        }

        #endregion
    }
#region Account Data Struct
    [Serializable]
    public struct AccountData 
    {
        public string Name;
        public int PlayerLevel;
        public int SelectedSpaceShipId;
        public int[] CompletedAchievements;//   Achievement Component
        public int LastReachedLevel;//  Gameplay Component
        public int MaxScore;//  Gameplay Component
        public List<int> OwnedSpaceShips;// Inventory Component
        public List<int> OwnedTemporalCards;
        public List<int> OwnedPermanentCards;
        public int[] CollectedSpaceShipParts;
        public SpaceShipUpgradeData[] SpaceShipUpgradeDatas;
        public int[] SuperPowerItemCounts;
        public int[] OwnedCards;// Inventory Component
        public int[] OwnedPowerUps;// Inventory Component
        public int AudioLevel;// Audio Component
        public int OwnedGold;// Currency Component
        public int OwnedDiamond;// Currency Component
        public int[] CopilotSetting;// Copilot Component
    }

    [Serializable]
    public struct SpaceShipUpgradeData
    {
        public int[] PartLevels;
    }

#endregion

}


