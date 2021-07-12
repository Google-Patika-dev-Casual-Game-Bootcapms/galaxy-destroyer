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
        private CardComponent cardComponent;

        private int permanentCardCount;
        private int temporalCardCount;

        public void Initialize(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;
            cardComponent = componentContainer.GetComponent("CardComponent") as CardComponent;

            permanentCardCount = cardComponent.GetPermanentCardCount();
            temporalCardCount = cardComponent.GetTemporalCardCount();


            if (!accountComponent.IsInitializedForFirstTime())
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

            AddCardsToInventory();

            Debug.Log("<color=green>Inventory Component initialized!</color>");
        }

        public void FirstInitialization()
        {
            inventoryData.OwnedPermanentCards = new List<int>();
            inventoryData.OwnedTemporalCards = new List<int>();
            inventoryData.OwnedSpaceShips = new List<int>();
            inventoryData.CollectedSpaceShipParts = new int[4];
        }

        // Add missing cards in order to show in inventory canvas
        private void AddCardsToInventory()
        {
            int upperLimit = Math.Max(temporalCardCount, permanentCardCount);

            for (int i = 0; i < upperLimit; i++)
            {
                if (i < permanentCardCount)
                {
                    if (!inventoryData.OwnedPermanentCards.Contains(i))
                    {
                        inventoryData.OwnedPermanentCards.Add(i);
                    }
                }

                if (i < temporalCardCount)
                {
                    if (!inventoryData.OwnedTemporalCards.Contains(i))
                    {
                        inventoryData.OwnedTemporalCards.Add(i);
                    }
                }
            }
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