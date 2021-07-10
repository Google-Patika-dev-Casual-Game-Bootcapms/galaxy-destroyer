namespace SpaceShooterProject.Component
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Devkit.Base.Component;
    using UnityEngine;

    public class InventoryComponent : MonoBehaviour, IComponent
    {
        private InventoryData inventoryData;

        private AccountComponent accountComponent;

        public void Initialize(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;


            if (accountComponent.IsFileExist())
            {
                inventoryData.OwnedPermanentCards = accountComponent.OwnedPermanentCards();
                inventoryData.OwnedTemporalCards = accountComponent.OwnedTemporalCards();
                inventoryData.OwnedSpaceShips = accountComponent.GetOwnedSpaceShips();
                inventoryData.CollectedSpaceShipParts = accountComponent.CollectedSpaceShipParts();
            }
            else
            {
                FirstInitialization();
            }

            Debug.Log("<color=green>Inventory Component initialized!</color>");
        }

        public void FirstInitialization()
        {
            inventoryData.OwnedPermanentCards = new List<int>();
            inventoryData.OwnedTemporalCards = new List<int>();
            inventoryData.OwnedSpaceShips = new List<int>();
            inventoryData.CollectedSpaceShipParts = new int[4];

            // Add cards in order to show in inventory canvas
            AddPermanentCard(0);
            AddPermanentCard(1);
            AddPermanentCard(2);
            AddPermanentCard(3);

            AddTemporalCard(0);
            AddTemporalCard(1);
        }

        #region Setter Methods

        public void AddPermanentCard(int index)
        {
            inventoryData.OwnedPermanentCards.Add(index);
        }

        public void AddTemporalCard(int index)
        {
            inventoryData.OwnedTemporalCards.Add(index);
        }

        public void AddSpaceshipPart(int index)
        {
            inventoryData.CollectedSpaceShipParts[index] += 1;

            if (inventoryData.CollectedSpaceShipParts[index] == 3)
            {
                inventoryData.OwnedSpaceShips.Add(index);
            }
        }

        #endregion

        #region Getter Methods

        public List<int> GetOwnedPermanentCards()
        {
            return inventoryData.OwnedPermanentCards;
        }

        public List<int> GetOwnedTemporalCards()
        {
            return inventoryData.OwnedTemporalCards;
        }

        public List<int> GetOwnedSpaceShips()
        {
            return inventoryData.OwnedSpaceShips;
        }

        public int[] GetSpaceShipParts()
        {
            return inventoryData.CollectedSpaceShipParts;
        }

        #endregion
    }

    [Serializable]
    public struct InventoryData
    {
        public List<int> OwnedPermanentCards;
        public List<int> OwnedTemporalCards;
        public List<int> OwnedSpaceShips;
        public int[] CollectedSpaceShipParts;
    }
}