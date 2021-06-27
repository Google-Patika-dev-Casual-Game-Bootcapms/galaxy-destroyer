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
        [SerializeField] private GameObject achievementCardPrefab;
        private AchievementsComponent achievementsComponent;
        [HideInInspector] public List<Achievement> achievementCardData;

        public delegate void AchievementListener(string name);
        public event AchievementListener AchievementCompletedEvent;

        private Dictionary<int, AchievementCard> achievementCardById;

        private const int achievementCount = 5;

        protected override void Init()
        {
            achievementCardById = new Dictionary<int, AchievementCard>();

            for (int i = 0; i < achievementCount; i++)
            {
                var achievementCard = Instantiate(achievementCardPrefab).GetComponent<AchievementCard>();
                achievementCard.transform.SetParent(achievementsContentPanel.transform);
                achievementCardById.Add(achievementCard.GetInstanceID(), achievementCard);

                achievementCard.OnAchievementButtonClick += CollectAchievement;
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

        public void CollectAchievement(int id)
        {
            Debug.Log(AchievementCompletedEvent);
            if (AchievementCompletedEvent != null)
            {
                var card = achievementCardById[id];

                if (card == null) 
                {
                    Debug.LogError("Card is null!!!");
                    return;
                }

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
