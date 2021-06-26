namespace SpaceShooterProject.Component
{
    using System.Collections.Generic;
    using Devkit.Base.Component;
    using UnityEngine;

    public class QuoteComponent : MonoBehaviour, IComponent
    {          
        [SerializeField] private TextAsset quotesFile;
        private Quotes quotesInJson;

        public void Initialize(ComponentContainer componentContainer)
        {
            quotesInJson = JsonUtility.FromJson<Quotes>(quotesFile.text);
            GetRandomQuote();           
        }

        public QuoteData GetRandomQuote()
        {
            int quoteIndex = Mathf.FloorToInt(Random.Range(0, quotesInJson.quotes.Length));
            return quotesInJson.quotes[quoteIndex];
        }
    }
}