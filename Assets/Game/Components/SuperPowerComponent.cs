namespace SpaceShooterProject.Component
{
  using Devkit.Base.Component;
  using System;
  using UnityEngine;

  public class SuperPowerComponent : IComponent
  {
    public enum SuperPowerType { Laser, Shield, MegaBomb};

    private AccountComponent accountComponent;

    private SuperPowerData laser;
    private SuperPowerData shield;
    private SuperPowerData megaBomb;


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

  public void upgradeSuperPower(SuperPowerType type) {
    switch(type) {
      case SuperPowerTypes.Laser:
      laser.Upgrade();
      break;
      case SuperPowerTypes.Laser:
      shield.Upgrade();
      break;
      case SuperPowerTypes.MegaBomb:
      megaBomb.upgrade();
      break;
      default:
      break;
    }
  }

  public void GetUpgradePrice(SuperPowerType type) {
    switch(type) {
      case SuperPowerTypes.Laser:
      laser.GetUpgradePrice();
      break;
      case SuperPowerTypes.Laser:
      shield.GetUpgradePrice();
      break;
      case SuperPowerTypes.MegaBomb:
      megaBomb.GetUpgradePrice();
      break;
      default:
      break;
    }
  }

  [Serializable]
  public struct SuperPowerData
  {
    public SuperPowerType type { get; };
    public int level { get; };
    public List<int> upgradePriceArray { get; };

    public void Upgrade() {
      level++;
    }

    public void GetUpgradePrice() {
      upgradePriceArray[level];
    }
  }
}
