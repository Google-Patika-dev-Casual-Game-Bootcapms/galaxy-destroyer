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

        public int count = 0;

        public delegate void AchievementListener(string name);
        public event AchievementListener AchievementCompletedEvent;

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
            Debug.Log(AchievementCompletedEvent);
            if (AchievementCompletedEvent != null)
            {
                var card = button.transform.parent.GetComponent<AchievementCard>();

                int currentCount;
                int goalCount;

                int.TryParse(card.currentCount.text, out currentCount);
                int.TryParse(card.goalCount.text, out goalCount);

                if (currentCount == goalCount)
                {
                    Debug.Log("yesssss");
                    AchievementCompletedEvent(card.header.text);
                }
            }
        }
    }
}
