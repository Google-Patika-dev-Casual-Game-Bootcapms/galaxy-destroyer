namespace SpaceShooterProject.Component
{
    using Devkit.Base.Component;
    using UnityEngine;
    using System;
    using System.Collections.Generic;

    public class AchievementsComponent : MonoBehaviour, IComponent, IDataObserver<Achievement>
    {
        public List<Achievement> achievementsList;
        CurrencyComponent currencyComponent;

        public void Initialize(ComponentContainer componentContainer)
        {
            currencyComponent = componentContainer.GetComponent("CurrencyComponent") as CurrencyComponent;
            LoadAchievementsAndAddToList();
        }

        //Subscribe AchievementsComponent as achievement observer..
        public void LoadAchievementsAndAddToList()
        {
            Debug.Log($"<color=black>{achievementsList.Count}</color>");

            for (int i = 0; i < achievementsList.Count; i++)
            {
                Subscribe(achievementsList[i]);
            }
        }

        //we progress the achievenemt count which we find with FindAchievementByName function..
        public void IncreaseAchievement(string name)
        {
            Achievement achievement = FindAchievement(name);

            if (achievement == null)
                return;

            achievement.RaiseCurrentCount();
        }

        public List<Achievement> GetAchievementsData()
        {
            return achievementsList;
        }

        //to find the achievement..
        private Achievement FindAchievement(string name)
        {
            foreach (var a in achievementsList)
            {
                if (a.Name == name) return a;
            }
            return null;
        }

        public void CompleteAchievement(string name)
        {
            if (currencyComponent == null) 
            {
                return;
            }

            currencyComponent.EarnGold(FindAchievement(name).Prize);
        }

        public void Subscribe(IDataObservable<Achievement> observable)
        {
            observable.Attach(this);
        }

        public void OnNotify()
        {
           
        }
    }
}


