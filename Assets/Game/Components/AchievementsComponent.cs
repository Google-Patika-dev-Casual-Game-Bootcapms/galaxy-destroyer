using Devkit.Base.Component;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace SpaceShooterProject.Component
{
    public class AchievementsComponent : MonoBehaviour, IComponent, IObserver<Achievement>
    {
        public List<Achievement> achievementsList;

        public void Initialize(ComponentContainer componentContainer)
        {
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
        public void ProgressAchievementWithName(string name)
        {
            Achievement achievement = FindAchievementByName(name);

            if (achievement == null)
                return;

            achievement.RaiseCurrentCount();
        }

        //to find the achievement..
        private Achievement FindAchievementByName(string name)
        {
            foreach (var a in achievementsList)
            {
                if (a.Name == name) return a;
            }
            return null;
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


