using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooterProject.Component
{
    [CreateAssetMenu(menuName = "Achievement")]
    public class Achievement : ScriptableObject, IObservable<Achievement>
    {
        #region Variables
        [SerializeField] private int id;
        [SerializeField] private string achievementName;
        [SerializeField, TextArea(1, 4)] private string description;
        [SerializeField] private Sprite icon;
        [SerializeField] private Sprite smallIcon;
        [SerializeField] private int goalCount;
        [SerializeField] private int currentCount;
        [SerializeField] private int prize;
        [SerializeField] private bool isAchived;
        private List<IObserver<Achievement>> observers = new List<IObserver<Achievement>>();
        #endregion

        #region  Properties
        public int Id { get => id; }
        public string Name { get => achievementName; }
        public Sprite Icon { get => icon; }
        public Sprite SmallIcon { get => smallIcon; }
        public string Descrption { get => description; }
        public int GoalCount { get => goalCount; }
        public int CurrentCount { get => currentCount; }
        public int Prize { get => prize; }
        public bool IsAchived { get => isAchived; set => isAchived = value; }
        #endregion

        //raises current count and if currentcount equal or greater than goalcount Notify() observers..
        public void RaiseCurrentCount()
        {
            if (isAchived) return;

            currentCount++;

            if (currentCount >= goalCount) Notify();
        }

        //notify observers..
        private void Notify()
        {
            foreach (var o in observers)
                o.OnNext(this);
        }

        public IDisposable Subscribe(IObserver<Achievement> observer)
        {
            return null;
        }
    }
}
