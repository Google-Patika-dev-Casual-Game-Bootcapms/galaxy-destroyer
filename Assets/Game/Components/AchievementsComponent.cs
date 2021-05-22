using Devkit.Base.Component;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace SpaceShooterProject.Component
{
    public class AchievementsComponent : IComponent, IObserver<Achievement>
    {

        string path = "Achievements";//we create a folder in Resources folder called Achievements to keep scriptable objects
        List<Achievement> achievementsList;

        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>AchievementsComponent initialized!</color>");
            LoadAchievementsAndAddToList();
        }

        //List the achievements and subscribe them as achievement observer..
        private void LoadAchievementsAndAddToList()
        {
            var achievements = Resources.LoadAll<Achievement>(path);

            for (int i = 0; i < achievements.Length; i++)
            {
                achievements[i].Subscribe(this);
                achievementsList.Add(achievements[i]);
            }
        }

        //we raise the achievenemt count which we find with FindAchievementByName function..
        public void ProgressAchievementWithName(string name)
        {
            Achievement achievement = FindAchievementByName(name);

            //if achievement is null, prevent the error
            if (achievement == null)
                return;

            achievement.RaiseCurrentCount();
        }

        //to find the achievement..
        private Achievement FindAchievementByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;

            return achievementsList.Find(a => a.Name.Equals(name));
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


