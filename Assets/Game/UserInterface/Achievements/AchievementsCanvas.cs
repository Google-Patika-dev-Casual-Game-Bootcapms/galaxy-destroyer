using System;
using System.Collections.Generic;
using SpaceShooterProject.Component;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooterProject.UserInterface
{
    public class AchievementsCanvas : BaseCanvas
    {
        [SerializeField] private RectTransform achievementsContentPanel; //This panel stands for parent of AchievementCards.
        [SerializeField] private GameObject achievementCard;
        private AchievementsComponent achievementsComponent;
        private List<Achievement> achievementCardData;

        protected override void Init()
        {
            achievementsComponent = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;
            achievementCardData = achievementsComponent.achievementsList;
            achievementsContentPanel.sizeDelta += Vector2.up * achievementCardData.Count * 300;


            for (var i = 0; i < achievementCardData.Count; i++)
            {
                GameObject newCardUIObject = Instantiate(achievementCard, achievementsContentPanel);
                AchievementCard card = newCardUIObject.GetComponent<AchievementCard>();
                card.Data = achievementCardData[i];
                card.Init();
            }
        }
    }
}
