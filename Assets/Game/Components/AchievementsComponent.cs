using Devkit.Base.Component;
using UnityEngine;
using System;
using System.Collections.Generic;

namespace SpaceShooterProject.Component
{
    public class AchievementsComponent : IComponent, IObserver<Achievement>
    {
        public ComponentContainer container;
        string path = "Achievements";//we create a folder in Resources folder called Achievements to keep scriptable objects
        public List<Achievement> achievementsList = new List<Achievement>();

        public void Initialize(ComponentContainer componentContainer)
        {
            container = componentContainer;
            LoadAchievementsAndAddToList();
        }

        //List the achievements and subscribe them as achievement observer..
        public void LoadAchievementsAndAddToList()
        {
            Debug.Log("AchievementsLoaded");
            var achievements = Resources.LoadAll<Achievement>(path);

            for (int i = 0; i < achievements.Length; i++)
            {
                achievementsList.Add(achievements[i]);
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
            Debug.Log($"<color=black>{achievementsList.Count}</color>");
            foreach (var a in achievementsList)
            {
                if (a.Name == name) return a;
            }
            return null;
        }

        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Achievement value)
        {
            throw new NotImplementedException();
        }
    }
}


