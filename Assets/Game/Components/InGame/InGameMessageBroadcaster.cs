namespace SpaceShooterProject.Component 
{
    using Devkit.Base.Component;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class InGameMessageBroadcaster : IComponent
    {
        public delegate void EnemyDestroyedDelegate(Enemy enemy);

        private event EnemyDestroyedDelegate onEnemyDestroyed;

        private List<EnemyDestroyedDelegate> delegates;

        public event EnemyDestroyedDelegate OnEnemyDestroyed
        {
            add 
            {
                onEnemyDestroyed += value;
                delegates.Add(value);
            }
            remove 
            {
                onEnemyDestroyed -= value;
                delegates.Remove(value);
            }
        }

        public void RemoveAllEvents() 
        {
            foreach (var item in delegates)
            {
                onEnemyDestroyed -= item;
            }

            delegates.Clear();
        }

        public void Initialize(ComponentContainer componentContainer)
        {
            delegates = new List<EnemyDestroyedDelegate>();
        }

        public void TriggerEnemyDeath(Enemy enemy)
        {
            if (onEnemyDestroyed != null) 
            {
                onEnemyDestroyed(enemy);
            }
        }
    }
}

