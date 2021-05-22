namespace SpaceShooterProject.Component
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.Base.Component;
    using UnityEngine;

    public class GachaComponent : MonoBehaviour, IComponent
    {
        // Pools
        List<int> permanentCards = new List<int>();
        List<int> spaceshipParts = new List<int>();

        // Counts
        private int _permanentCardCount = 4;
        private int _temporalCardCount = 2;
        private int _spaceshipPartCount = 10;

        private int _goldMultiplier = 50;
        private int _dice;

        public void Initialize(ComponentContainer componentContainer)
        {
            //TODO add necessary components
        }

        // Start is called before the first frame update
        void Start()
        {
            AddItemsToList();
        }

        private void AddItemsToList()
        {
            for (int i = 0; i < _permanentCardCount; i++)
            {
                permanentCards.Add(i);
            }

            for (int i = 0; i < _spaceshipPartCount; i++)
            {
                spaceshipParts.Add(i);
            }
        }
        
        //TODO Check: if chest's return >= _goldMultiplier, its gold. 
        public int OpenBronzeChest()
        {
            // Permanent Card %25
            // Temporal Card %25
            // Spaceship Part %25
            // Gold %25
            _dice = Random.Range(1, 5);

            if (_dice == 1)
                return GetPermanentCard();
            if (_dice == 2)
                return GetSpaceshipPart();
            if (_dice == 3)
                return GetTemporalCard();
            return GetGold();
        }

        public int OpenSilverChest()
        {
            // Permanent Card %30
            // Temporal Card %30
            // Spaceship Part %30
            // Gold %10
            _dice = Random.Range(1, 11);

            if (_dice <= 3)
                return GetPermanentCard();
            if (_dice <= 6)
                return GetSpaceshipPart();
            if (_dice <= 9)
                return GetTemporalCard();
            return GetGold();
        }

        public int OpenGoldenChest()
        {
            // Permanent Card %40
            // Temporal Card %20
            // Spaceship Part %40
            _dice = Random.Range(1, 11);

            if (_dice <= 4)
                return GetPermanentCard();
            if (_dice <= 8)
                return GetSpaceshipPart();
            return GetTemporalCard();
        }

        private int GetGold()
        {
            // TODO Stabilize gold amount. Add level multiplier for gold amount
            _dice = Random.Range(1, 11);
            int goldAmount = _goldMultiplier * _dice;
            return goldAmount;
        }


        private int GetSpaceshipPart()
        {
            if (spaceshipParts.Count == 0)
            {
                return GetGold();
            }

            int spaceshipPartIndex = Random.Range(0, spaceshipParts.Count);
            int temp = spaceshipParts[spaceshipPartIndex];
            spaceshipParts.RemoveAt(spaceshipPartIndex);
            return temp;
        }

        private int GetTemporalCard()
        {
            int temporalCardIndex = Random.Range(0, _temporalCardCount);
            return temporalCardIndex;
        }

        private int GetPermanentCard()
        {
            if (permanentCards.Count == 0)
            {
                return GetGold();
            }

            int permanentCardIndex = Random.Range(0, permanentCards.Count);
            int temp = permanentCards[permanentCardIndex];
            permanentCards.RemoveAt(permanentCardIndex);
            return temp;
        }
    }
}