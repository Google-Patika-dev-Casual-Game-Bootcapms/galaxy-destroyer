namespace SpaceShooterProject.UserInterface 
{
    using UnityEngine;
    using UnityEngine.UI;
    using SpaceShooterProject.Component;
    using TMPro;

    public class QuoteCanvas : BaseCanvas
    {
        public delegate void QuoteRequestDelegate();
        public event QuoteRequestDelegate OnInGameMenuRequest;
       
        [SerializeField] private TMP_Text quoteText;
        [SerializeField] private RectTransform backgroundImage;

        protected override void Init()
        {
           backgroundImage.sizeDelta = GetCanvasSize();
           ShowQuote();
        }


        public void RequestInGameMenu() 
        {
            if (OnInGameMenuRequest != null) 
            {
                OnInGameMenuRequest();
            }
        }

        public void ShowQuote()
        {
            QuoteData quoteData = GetComponent<QuoteComponent>().GetRandomQuote();
            quoteText.text = quoteData.quote +"\n-" +  quoteData.author;
        }
    }
}
