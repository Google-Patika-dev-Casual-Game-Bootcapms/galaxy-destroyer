using System;
using System.Collections.Generic;
using SpaceShooterProject.Component;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooterProject.UserInterface
{
    public class AchievementsCanvas : BaseCanvas
    {
        [SerializeField] public RectTransform achievementsContentPanel; //This panel stands for parent of AchievementCards.
        [SerializeField] private GameObject achievementCard;
        private AchievementsComponent achievementsComponent;
        [HideInInspector] public List<Achievement> achievementCardData;

        protected override void Init()
        {
            achievementsComponent = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;
            achievementCardData = achievementsComponent.achievementsList;


            for (var i = 0; i < achievementCardData.Count; i++)
            {
                GameObject newCardUIObject = Instantiate(achievementCard);
                newCardUIObject.transform.SetParent(achievementsContentPanel.transform);
                AchievementCard card = newCardUIObject.GetComponent<AchievementCard>();
                card.Data = achievementCardData[i];
                card.Init();
            }
        }
    }
}
