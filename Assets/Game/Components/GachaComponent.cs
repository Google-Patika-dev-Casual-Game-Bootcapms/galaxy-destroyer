namespace SpaceShooterProject.Component
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.Base.Component;
    using UnityEngine;

    public class GachaComponent : IComponent
    {
        private AccountComponent accountComponent;

        List<int> permanentCards = new List<int>();
        List<int> temporalCards = new List<int>();
        int permanentCardNumber = 4;
        int temporalCardNumber = 2;

        public void Initialize(ComponentContainer componentContainer)
        {
            accountComponent = componentContainer.GetComponent("AccountComponent") as AccountComponent;
        }

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < permanentCardNumber; i++)
            {
                permanentCards.Add(i);
            }
            for (int i = 0; i < temporalCardNumber; i++)
            {
                temporalCards.Add(i);
            }
        }

        public void GetChest()
        {
            int rnd = Random.Range(0, 20);
            if (rnd == 0)
            {
                //%5 ihtimal
                GetPermanentCard();
            }
            else if (rnd == 1)
            {
                //%5 ihtimal
                GetTemporalCard();
            }
            else
            {
                //%90 ihtimal
                //GetStars()
                Debug.Log("Sandýktan Star çýktý");
            }
        }

        private int GetTemporalCard()
        {
            int cardIndex = Random.Range(0, temporalCards.Count);
            int temp = temporalCards[cardIndex];
            Debug.Log("TemporalCard: " + temp);
            return temp;
        }

        private int GetPermanentCard()
        {
            if (permanentCards.Count == 0)
            {
                //tüm permanent kartlar açýlmýþ
                //GetStars()
                return -1;
            }
            else
            {
                int cardIndex = Random.Range(0, permanentCards.Count);
                int temp = permanentCards[cardIndex];
                permanentCards.RemoveAt(cardIndex);
                Debug.Log("PermanentCard: " + temp);
                return temp;
            }
        }
    }
}

