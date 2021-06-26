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

        public int count=0;

        public delegate void AchievementListener(string name);
        public event AchievementListener AchievementCompleted;

        protected override void Init()
        {
            for (int i = 0; i < count; i++)
            {
                GameObject newCardUIObject = Instantiate(achievementCard);
                newCardUIObject.transform.SetParent(achievementsContentPanel.transform);
            }
        }

        public void SetData(List<Achievement> achievements)
        {
            this.achievementCardData = achievements;
            UpdateUI();
        }

        private void UpdateUI()
        {
            for (var i = 0; i < achievementsContentPanel.transform.childCount; i++)
            {
                achievementsContentPanel.transform.GetChild(i).GetComponent<AchievementCard>().Data = achievementCardData[i];
                achievementsContentPanel.transform.GetChild(i).GetComponent<AchievementCard>().Init();
            }
        }

        public void AchievementButton(Button button)
        {
            if (AchievementCompleted != null)
            {
            }
        }
    }
}
