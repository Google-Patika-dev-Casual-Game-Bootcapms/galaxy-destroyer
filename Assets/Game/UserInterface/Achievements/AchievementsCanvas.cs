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

        //TODO: create achievement object pool public Queue<AchievementCard> achievementObjects;



        public delegate void AchievementListener(string name);
        public event AchievementListener AchievementCompleted;

        protected override void Init()
        {
            //achievementsComponent = componentContainer.GetComponent("AchievementsComponent") as AchievementsComponent;
            //achievementCardData = achievementsComponent.achievementsList;
        }

        public void SetData(List<Achievement> achievements)
        {
            this.achievementCardData = achievements;

            UpdateUI();
        }

        private void UpdateUI()
        {
            //Bütün o an ekranda bulunan objeleri Object pool'a ekle. Ve deactivate et. Yani ekranda görünmesinler.
            //TODO: Instantiate edilen Gameobjeleri basit bir pool'ta tut.
            //TODO: Get object from pool isimli bir fonksiyon yap.
                //TODO: Eğer bu pool'da yeterince obje yoksa, bir tane obje create edip Objecr pool queue'suna ekle.
                //TODO: object pool queue'sundan bir tane object al
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
