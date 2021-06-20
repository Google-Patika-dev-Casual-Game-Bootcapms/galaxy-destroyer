namespace SpaceShooterProject.Component
{
    using Devkit.Base.Component;
    using UnityEngine;
    using System;
    using System.Collections.Generic;

    public class AchievementsComponent : MonoBehaviour, IComponent, IObserver<Achievement>
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
                achievementsList[i].Subscribe(this);
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

        public void OnNext(Achievement value)
        {
            value.IsAchived = true;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }
    }
}


