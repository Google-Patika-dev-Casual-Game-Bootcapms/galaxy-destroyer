namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using UnityEngine;
    public class AccountComponent : IComponent
    {
        private AccountData accountData;

        public void Initialize(ComponentContainer componentContainer)
        {
            //TODO: Inject dependencies
            Debug.Log("<color=green>Account Component initialized!</color>");

            //TODO: read account data from local storage
            //TODO: serialize data
            //TODO: fill account data

            //TODO: Delete!!!
            accountData.Name = "Ne Diyelim!!!";

        }

        public string GetPlayerName() 
        {
            return accountData.Name;
        }


    }

    public struct AccountData 
    {
        public string Name;
    }
}


