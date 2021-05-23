namespace SpaceShooterProject.Component 
{
    using System;
    using System.IO;
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

        private string accountDataPath;
        private string accountDataFile;
        private LoadComponent loadComponent;
        private SaveComponent saveComponent;

        #endregion        

        public void Initialize(ComponentContainer componentContainer)
        {
            achievementsComponent = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;
            gamePlayComponent = componentContainer.GetComponent("GamePlayComponent") as GamePlayComponent;
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
            audioComponent = componentContainer.GetComponent("AudioComponent") as AudioComponent;
            // TODO: Activate below components when they are created
                // inventoryComponent = componentContainer.GetComponent("InventoryComponent") as InventoryComponent;
                // copilotComponent = componentContainer.GetComponent("CopilotComponent") as CopilotComponent;
            
            //Debug.Log("<color=green>Account Component initialized!</color>");

            accountDataFile = "accountData.txt";
            accountDataPath = Application.persistentDataPath + "/" + accountDataFile;
            
            loadComponent = new LoadComponent();
            saveComponent = new SaveComponent();

            if(File.Exists(accountDataPath)){
                //Debug.Log("File exists and successfully read.");
                accountData = loadComponent.Load<AccountData>(accountDataPath);
            } else {
                //Debug.Log("Initialize for first time");
                InitializeForFirstTime();
            }
            
            //Debug.Log("Name:" + GetPlayerName());

        }

        private void InitializeForFirstTime(){
            //Debug.Log("Entered first initialization");
            accountData.Name = "Name"; // TODO: Ask for name to the user
            accountData.PlayerLevel = 1;
            
            // TODO: Assign default values for other components in the future
            
            saveComponent.Save(accountData, accountDataPath);
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
            saveComponent.Save(accountData, accountDataPath);
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
        public int[] OwnedSpaceShips;// Inventory Component
        public UpgradeData[] SpaceShipUpgradeDatas;// Inventory Component
        public int[] OwnedCards;// Inventory Component
        public int[] OwnedPowerUps;// Inventory Component
        public UpgradeData LastSelectedSpaceShip;// Inventory Component
        public int AudioLevel;// Audio Component
        public int OwnedGold;// Currency Component
        public int OwnedDiamond;// Currency Component
        public int[] CopilotSetting;// Copilot Component
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


