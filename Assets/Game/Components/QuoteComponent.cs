namespace SpaceShooterProject.Component
{
    using System;
    using System.IO;
    using System.Collections.Generic;
    using Devkit.Base.Component;
    using UnityEngine;

    public class QuoteComponent : MonoBehaviour, IComponent
    {

        private string quoteDataPath;
        private string quoteDataFile;
        
        public void Initialize(ComponentContainer componentContainer)
        {
            JsonUtility.FromJson<string>("ggdf.json");
        }

         

    }

}