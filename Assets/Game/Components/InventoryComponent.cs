﻿namespace SpaceShooterProject.Component
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

        private int spaceShipCount;
        private int temporalCardCount;
        private int permanentCardCount;

        public void Initialize(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;

            // To-Do: Assign values when necessary components are created!
            // spaceShipCount = SpaceShips.GetSpaceShipCount(); ?
            // temporalCardCount = CardComponent.GetTemporalCardCount();
            // permanentCardCount = CardComponent.GetPermanentCardCount();

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
            inventoryData.CollectedSpaceShipParts = new int[spaceShipCount];
        }

        // Get return value from Gacha Component and add looted item to inventory
        public void AddItem(int chestReturn)
        {
            if (chestReturn < permanentCardCount)
            {
                inventoryData.OwnedPermanentCards.Add(chestReturn);
            }
            else if (chestReturn >= 10 && chestReturn < temporalCardCount + 10)
            {
                inventoryData.OwnedTemporalCards.Add(chestReturn - 10);
            }
            else if (chestReturn >= 20 && chestReturn < spaceShipCount * 4)
            {
                int index = chestReturn - 20;

                while (index > spaceShipCount)
                {
                    index -= 4;
                }

                inventoryData.CollectedSpaceShipParts[index]++;
            }
        }

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