namespace SpaceShooterProject.UserInterface
{
    using System;
    using System.Collections.Generic;
    using SpaceShooterProject.Component;
    using UnityEngine;
    using UnityEngine.UI;

    public class AchievementsCanvas : BaseCanvas
    {
        [SerializeField] public RectTransform achievementsContentPanel; //This panel stands for parent of AchievementCards.
        [SerializeField] private GameObject achievementCard;
        private AchievementsComponent achievementsComponent;
        [HideInInspector] public List<Achievement> achievementCardData;

        public delegate void AchievementListener(string name);
        public event AchievementListener AchievementCompleted;

        protected override void Init()
        {
            //achievementsComponent = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;
            //achievementCardData = achievementsComponent.achievementsList;

            for (var i = 0; i < achievementCardData.Count; i++)
            {
                GameObject newCardUIObject = Instantiate(achievementCard);
                newCardUIObject.transform.SetParent(achievementsContentPanel.transform);
                newCardUIObject.GetComponent<AchievementCard>().Data = achievementCardData[i];
                newCardUIObject.GetComponent<AchievementCard>().Init();
            }
        }

        public void AchievementButton(Button button)
        {
            if(AchievementCompleted != null)
            {
            }
        }
    }
}
