namespace SpaceShooterProject.Component 
{
    using System;
    using Devkit.Base.Component;
    using UnityEngine;
    public class AccountComponent : IComponent
    {
        private AccountData accountData;
        private AchievementsComponent achievementsComponent;
        private GamePlayComponent gamePlayComponent;
        private CurrencyComponent currencyComponent;
        private AudioComponent audioComponent;

        //TODO: Add inventory system reference when inventory component created!!!
        // private InventoryComponent inventoryComponent;

        //TODO: Add copilot system reference when inventory component created!!!
        // private CopilotComponent copilotComponent;

        public void Initialize(ComponentContainer componentContainer)
        {
            achievementsComponent = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;
            gamePlayComponent = componentContainer.GetComponent("GamePlayComponent") as GamePlayComponent;
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
            audioComponent = componentContainer.GetComponent("AudioComponent") as AudioComponent;
            // inventoryComponent = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
            // copilotComponent = componentContainer.GetComponent("CopilotComponent") as CopilotComponent;
            
            Debug.Log("<color=green>Account Component initialized!</color>");

            //TODO: read account data from local storage
            //TODO: serialize data
            //TODO: fill account data

            accountData = LoadComponent.Load<AccountData>(); // Try to read from the path. If there is no txt file, initialize from components.
            
            if(LoadComponent.fileNotExist == true){
                accountData.Name = "Name"; // What should be the name?
                accountData.PlayerLevel = 1;
                /*
                accountData.CompletedAchievements = achievementsComponent.GetCompletedAchievements();
                /accountData.LastReachedLevel = gamePlayComponent.GetLastReachedComponent();
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
            }

        }

        public void Save(){
            SaveComponent.Save(accountData);
        }

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

        public int[] GetOwnedSpaceShips(){
            return accountData.OwnedSpaceShips;
        }

        public UpgradeData[] GetSpaceShipUpgradeDatas(){
            return accountData.SpaceShipUpgradeDatas;
        }

        public int[] GetOwnedCards(){
            return accountData.OwnedCards;
        }

        public int[] GetOwnedPowerUps(){
            return accountData.OwnedPowerUps;
        }

        public UpgradeData GetLastSelectedSpaceShip(){
            return accountData.LastSelectedSpaceShip;
        }

        // TODO: Ses ayarı için getter method ekle
        public int GetAudioSetting() {
            return accountData.AudioSetting;
        }

        public int GetOwnedGold() {
            return accountData.OwnedGold;
        }

        public int GetOwnedDiamond() {
            return accountData.OwnedDiamond;
        }

        // TODO: Co-pilot bilgisi için getter method ekle
        public int[] GetCopilotSetting(){
            return accountData.CopilotSetting;
        }

    }

    [Serializable]
    public struct AccountData 
    {
        public string Name;
        public int PlayerLevel;
        public int[] CompletedAchievements;//   Achievement Component
        public int LastReachedLevel;//  Gameplay Component
        public int MaxScore;//  Gameplay Component
        public int[] OwnedSpaceShips;// Inventory Component?
        public UpgradeData[] SpaceShipUpgradeDatas;// Inventory Component?
        public int[] OwnedCards;// Inventory Component?
        public int[] OwnedPowerUps;// Inventory Component?
        public UpgradeData LastSelectedSpaceShip;// Inventory Component?
        // Ses ayarını nasıl tutalım?
        public int AudioSetting;
        public int OwnedGold;// Currency Component
        public int OwnedDiamond;// Currency Component
        // Co-pilot bilgisini nasıl tutalım?
        public int[] CopilotSetting;
    }

    [Serializable]
    public struct UpgradeData 
    {
        public int SpaceShipId;
        public int SpaceShipPart;
        public int PartLevel;
    }

 
}


