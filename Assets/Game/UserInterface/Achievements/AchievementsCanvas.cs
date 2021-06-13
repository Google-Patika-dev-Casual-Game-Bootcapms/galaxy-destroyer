using System.Collections.Generic;
using SpaceShooterProject.Component;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooterProject.UserInterface
{
    public class AchievementsCanvas : BaseCanvas
    {
        protected override void Init()
        {
            VerticalLayoutGroup achievementsList;
            List<AchievementCard> achievementCards = new List<AchievementCard>();
            AchievementsComponent achievement = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;

            foreach (var a in achievement.achievementsList)
            {
                achievementCards.Add(new AchievementCard(a));
            }
        }
    }
}
