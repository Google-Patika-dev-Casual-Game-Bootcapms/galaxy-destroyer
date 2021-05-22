namespace SpaceShooterProject.Component 
{
    using System;
    using Devkit.Base.Component;
    using UnityEngine;
    public class AccountComponent : IComponent
    {

        #region Variables        
        private AccountData accountData;
        private AchievementsComponent achievementsComponent;
        private GamePlayComponent gamePlayComponent;
        private CurrencyComponent currencyComponent;
        private AudioComponent audioComponent;

        //TODO: Add inventory system reference when inventory component created!!!
        // private InventoryComponent inventoryComponent;

        //TODO: Add copilot system reference when inventory component created!!!
        // private CopilotComponent copilotComponent;

        #endregion        

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

            LoadComponent loadComponent = new LoadComponent();

            accountData = loadComponent.Load<AccountData>(); // Try to read from the path. If there is no txt file, initialize from components.
            //loadcomponent.InitializeByDefault += FirstInitialization;
            if(loadComponent.fileNotExist){
                Debug.Log("File not exist. Initializing for first time usage:");
                FirstInitialization();
            }
            Debug.Log(GetPlayerName());

        }

        private void FirstInitialization()
        {
            Debug.Log("Entered first initialization");
            accountData.Name = "Name"; // Kullanıcıya nasıl sorabiliriz bunu?
            accountData.PlayerLevel = 1;
            /* // Getter metodlar eklendiğinde aktif hale getirilecek!
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
            Save();
        }

        public void Save(){
            SaveComponent.Save(accountData);
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
        public int GetAudioLevel() {
            return accountData.AudioLevel;
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
#endregion
    }
#region Account Data Struct
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
        public int AudioLevel;
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
#endregion
 
}


