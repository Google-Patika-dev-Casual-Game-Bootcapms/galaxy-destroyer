using Devkit.Base.Component;
using UnityEngine;

namespace SpaceShooterProject.Component 
{
    public class AchievementsComponent : IComponent
    {
        public void Initialize(ComponentContainer componentContainer)
        {
            Debug.Log("<color=green>AchievementsComponent initialized!</color>");
        }

    }
}


