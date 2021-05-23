namespace SpaceShooterProject.Component
{
    using System;
    using System.Collections.Generic;
    using Devkit.Base.Component;
    using UnityEngine;

    public class InventoryComponent : MonoBehaviour, IComponent
    {
        private InventoryData inventoryData;

        public void Initialize(ComponentContainer componentContainer)
        {
            
            // To-Do: Save & Load Data

             if (true) // if data not exist
            {
                FirstInitialization();
            }

            Debug.Log("<color=green>Inventory Component initialized!</color>");
        }

        public void FirstInitialization()
        {
            inventoryData.SpaceShips = new List<SpaceShipData>()
            {
                new SpaceShipData { ID = 0, Name = "Gemi 1"},
                new SpaceShipData { ID = 1, Name = "Gemi 2"},
                new SpaceShipData { ID = 2, Name = "Gemi 3"}
            };

            inventoryData.PermanentCards = new List<CardData>()
            {
                new CardData { ID = 0, Name = "Kuvvetli Refleksler", Description = "Hareket hızı artar." },
                new CardData { ID = 1, Name = "Füze Hasarı", Description = "Füze hasarı artar." },
                new CardData { ID = 2, Name = "Son Şans", Description = "Ölümcül bir hasar aldığındığında bir süreliğine koruyucu bir kalkan devreye girer." },
                new CardData { ID = 3, Name = "Destek Kuvveti", Description = "Düşman saldırılarını savuşturan bir drone belirir." },
            };

            inventoryData.TemporalCards = new List<CardData>()
            {
                new CardData { ID = 0, Name = "Leprikon", Description = "Düşmanlar daha fazla altın düşürürler." },
                new CardData { ID = 1, Name = "Ekstra Muhimmat", Description = "Bölüme başlarken bir süper güç kullanım hakkı kazanılır." }
            };

            inventoryData.OwnedPermanentCards = new List<int>();
            inventoryData.OwnedTemporalCards = new List<int>();
            inventoryData.OwnedSpaceShips = new List<int>();
            inventoryData.CollectedSpaceShipParts = new int[inventoryData.SpaceShips.Count];
        }
        
        // Get return value from Gacha Component and add looted item to inventory
        public void GachaLoot(int chestReturn)
        {
            if (chestReturn < 4)
            {
                inventoryData.OwnedPermanentCards.Add(chestReturn);
            }
            else if (chestReturn > 9 && chestReturn < 12)
            {
                inventoryData.OwnedTemporalCards.Add(chestReturn - 10);
            }
            else if (chestReturn > 19 && chestReturn < 32)
            {
                int index = chestReturn - 20;
                
                while (index > 3)
                {
                    index -= 4;
                }

                inventoryData.CollectedSpaceShipParts[index]++;
            }
        }

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

    }

    [Serializable]
    public struct InventoryData
    {
        public List<SpaceShipData> SpaceShips;
        public List<CardData> PermanentCards;
        public List<CardData> TemporalCards;

        public List<int> OwnedPermanentCards;
        public List<int> OwnedTemporalCards;
        public List<int> OwnedSpaceShips;
        public int[] CollectedSpaceShipParts;
    }

    [Serializable]
    public struct SpaceShipData
    {
        public int ID;
        public string Name;
    }

    [Serializable]
    public struct CardData
    {
        public int ID;
        public string Name;
        public string Description;
    }
}