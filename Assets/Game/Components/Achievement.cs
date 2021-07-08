using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterProject.Component
{
    [CreateAssetMenu(menuName = "Achievement")]
    public class Achievement : ScriptableObject, IDataObservable<Achievement>
    {
        [SerializeField] private int id;
        [SerializeField] private string achievementName;
        [SerializeField, TextArea(1, 4)] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private Sprite smallIcon;
        [SerializeField] private int goalCount;
        [SerializeField] private int currentCount;
        [SerializeField] private int prize;
        [SerializeField] private bool isAchived;
        private List<IDataObserver<Achievement>> observers = new List<IDataObserver<Achievement>>();

        public int Id { get => id; }
        public string Name { get => achievementName; }
        public Sprite Icon { get => icon; }
        public Sprite SmallIcon { get => smallIcon; }
        public string Descrption { get => description; }
        public int GoalCount { get => goalCount; }
        public int CurrentCount { get => currentCount; }
        public int Prize { get => prize; }
        public bool IsAchived { get => isAchived; set => isAchived = value; }

        //raises current count and if currentcount equal or greater than goalcount Notify() observers..
        public void RaiseCurrentCount()
        {
            if (isAchived) return;

            currentCount++;

            if (currentCount >= goalCount) Notify();
        }

         public void Attach(IDataObserver<Achievement> observer)
        {
            observers.Add(observer);
        }

        public void Detach(IDataObserver<Achievement> observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            for (int i = 0; i < observers.Count; i++)
            {
                observers[i].OnNotify();
            }
        }
    }
}
