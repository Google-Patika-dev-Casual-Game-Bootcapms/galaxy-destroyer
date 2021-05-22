namespace SpaceShooterProject.Component
{
    using System.Collections;
    using System.Collections.Generic;
    using Devkit.Base.Component;
    using UnityEngine;

    public class GachaComponent : MonoBehaviour, IComponent
    {
        // TODO Set up access modifiers
        // Pools
        List<int> permanentCards = new List<int>();
        List<int> spaceshipParts = new List<int>();

        // Counts
        private int _permanentCardCount = 4;
        private int _temporalCardCount = 2;
        private int _spaceshipPartCount = 10;

        // Starts
        private int _permanentCardStart = 0;
        private int _temporalCardStart = 10;
        private int _spaceshipPartStart = 20;

        // Ends
        private int _permanentCardEnd;
        private int _temporalCardEnd;
        private int _spaceshipPartEnd;

        // Gold
        private int _defaultGoldAmount = 50;
        private int _goldAmountMultiplierStart = 1;
        private int _goldAmountMultiplierEnd = 11;

        private int _dice;

        // For Inventory System
        // TODO Check: if chest's return 0-9, Permanent Card
        // TODO Check: if chest's return 10-19,Temporal Card
        // TODO Check: if chest's return 20-29, Spaceship Part
        // TODO Check: if chest's return % _defaultGoldAmount == 0, Gold. 
        
        // TODO Optimize chest possibilities

        public void Initialize(ComponentContainer componentContainer)
        {
            // TODO add necessary components
        }

        // Start is called before the first frame update
        void Start()
        {
            SetUpEnds();
            AddItemsToList();

            TestGoldenChest();
        }

        private void TestGoldenChest()
        {
            for (int i = 0; i < 100; i++)
            {
                int returnCheck = OpenGoldenChest();
                if (returnCheck < 10)
                    Debug.Log("Permanent Card: " + returnCheck);
                else if (returnCheck < 20)
                    Debug.Log("Temporal Card: " + returnCheck);
                else if (returnCheck < 30)
                    Debug.Log("Spaceship Part: " + returnCheck);
                else if (returnCheck % _defaultGoldAmount == 0)
                    Debug.Log("Gold: " + returnCheck);
            }
        }

        private void SetUpEnds()
        {
            _permanentCardEnd = _permanentCardCount + _permanentCardStart;
            _temporalCardEnd = _temporalCardCount + _temporalCardStart;
            _spaceshipPartEnd = _spaceshipPartCount + _spaceshipPartStart;
        }

        private void AddItemsToList()
        {
            for (int i = _permanentCardStart; i < _permanentCardEnd; i++)
            {
                permanentCards.Add(i);
            }

            for (int i = _spaceshipPartStart; i < _spaceshipPartEnd; i++)
            {
                spaceshipParts.Add(i);
            }
        }

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
            // Temporal Card %20
            // Spaceship Part %30
            // Gold %20
            _dice = Random.Range(1, 11);

            if (_dice <= 3)
                return GetPermanentCard();
            if (_dice <= 6)
                return GetSpaceshipPart();
            if (_dice <= 8)
                return GetTemporalCard();
            return GetGold();
        }

        public int OpenGoldenChest()
        {
            // Permanent Card %40
            // Temporal Card %10
            // Spaceship Part %40
            // Gold %10
            _dice = Random.Range(1, 11);

            if (_dice <= 4)
                return GetPermanentCard();
            if (_dice <= 8)
                return GetSpaceshipPart();
            if (_dice == 9)
                return GetTemporalCard();
            return GetGold();
        }

        private int GetGold()
        {
            // TODO Optimize gold amount. Add level multiplier for gold amount
            _dice = Random.Range(_goldAmountMultiplierStart, _goldAmountMultiplierEnd);
            int goldAmount = _defaultGoldAmount * _dice;
            return goldAmount;
        }


        private int GetSpaceshipPart()
        {
            if (spaceshipParts.Count == 0)
            {
                _dice = Random.Range(0, 2);
                if (_dice == 0)
                    return GetGold();
                return GetTemporalCard();
            }


            int spaceshipPartIndex = Random.Range(0, spaceshipParts.Count);
            int temp = spaceshipParts[spaceshipPartIndex];
            spaceshipParts.RemoveAt(spaceshipPartIndex);
            return temp;
        }

        private int GetTemporalCard()
        {
            int temporalCardIndex = Random.Range(_temporalCardStart, _temporalCardEnd);
            return temporalCardIndex;
        }

        private int GetPermanentCard()
        {
            if (permanentCards.Count == 0)
            {
                _dice = Random.Range(0, 2);
                if (_dice == 0)
                    return GetGold();
                return GetTemporalCard();
            }

            int permanentCardIndex = Random.Range(0, permanentCards.Count);
            int temp = permanentCards[permanentCardIndex];
            permanentCards.RemoveAt(permanentCardIndex);
            return temp;
        }
    }
}