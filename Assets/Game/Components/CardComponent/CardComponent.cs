namespace SpaceShooterProject.Component
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using System.Linq;
    using Devkit.Base.Component;
    using UnityEngine;

    public class CardComponent : MonoBehaviour, IComponent
    {
        [SerializeField] private List<CardData> permanentCardList;
        [SerializeField] private List<CardData> temporalCardList;

        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>Card Component initialized!</color>");
        }

        #region Getter Methods

        #region Card List
        public List<CardData> GetAllCards()
        {
            return permanentCardList.Concat(temporalCardList).ToList();
        }

        public List<CardData> GetPermanentCardList()
        {
            return permanentCardList;
        }

        public List<CardData> GetTemporalCardList()
        {
            return temporalCardList;
        }

        #endregion

        #region Card Info

        public string GetPermanentCardName(int index)
        {
            return permanentCardList[index].cardName;
        }

        public string GetPermanentCardDescription(int index)
        {
            return permanentCardList[index].cardDescription;
        }

        public Sprite GetPermanentCardSprite(int index)
        {
            return permanentCardList[index].cardArtwork;
        }

        public string GetTemporalCardName(int index)
        {
            return temporalCardList[index].cardName;
        }

        public string GetTemporalCardDescription(int index)
        {
            return temporalCardList[index].cardDescription;
        }

        public Sprite GetTemporalCardSprite(int index)
        {
            return temporalCardList[index].cardArtwork;
        }

        #endregion

        #region Card Count


        public int GetPermanentCardCount()
        {
            return permanentCardList.Count;
        }

        public int GetTemporalCardCount()
        {
            return temporalCardList.Count;
        }

        #endregion

        #endregion
    }
}