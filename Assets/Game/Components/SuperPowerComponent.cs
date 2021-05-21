namespace SpaceShooterProject.Component
{
  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;

  public class SuperPowerComponent : IComponent
  {
    private SuperPowerData superPowerData;
    private CurrencyComponent currencyComponent;

    public void Initialize(ComponentContainer componentContainer)
    {
      currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;

      Debug.Log("<color=green>Super Power Component initialized!</color>");

      superPowerData = LoadComponent.Load<SuperPowerData>();
    }

    public int GetOwnedLaserItems() {
      return superPowerData.NumberOfOwnedLaserItems;
    }

    public int GetOwnedShieldItems() {
      return superPowerData.NumberOfOwnedShieldItems;
    }
    
    public int GetOwnedMegaBombItems() {
      return superPowerData.NumberOfOwnedMegaBombItems;
    }

    // Decrease the number of super power items when player use them
    public void UseLaser() {
      superPowerData.NumberOfOwnedLaserItems--;
    }

    public void UseEnergyShield() {
      superPowerData.NumberOfOwnedEnergyShieldItems--;
    }

    public void UseMegaBomb() {
      superPowerData.NumberOfOwnedMegaBombItems--;
    }

    [Serializable]
    public struct SuperPowerData
    {
      public int NumberOfOwnedLaserItems;
      public int NumberOfOwnedEnergyShieldItems;
      public int NumberOfOwnedMegaBombItems;
    }
  }
}
