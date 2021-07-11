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
        public delegate void EnemyOutOfScreenDelegate(Enemy enemy);

        private event EnemyDestroyedDelegate onEnemyDestroyed;
        private event EnemyOutOfScreenDelegate onEnemyOutOfScreen;

        private List<EnemyDestroyedDelegate> enemyDestroyDelegates;
        private List<EnemyOutOfScreenDelegate> enemyOutOfScreenDelegates;

        public event EnemyDestroyedDelegate OnEnemyDestroyed
        {
            add
            {
                onEnemyDestroyed += value;
                enemyDestroyDelegates.Add(value);
            }
            remove
            {
                onEnemyDestroyed -= value;
                enemyDestroyDelegates.Remove(value);
            }
        }

        public event EnemyOutOfScreenDelegate OnEnemyOutOfScreen
        {
            add
            {
                onEnemyOutOfScreen += value;
                enemyOutOfScreenDelegates.Add(value);
            }
            remove
            {
                onEnemyOutOfScreen -= value;
                enemyOutOfScreenDelegates.Remove(value);
            }
        }

        public void RemoveAllEvents()
        {
            foreach (var enemyDestroyEvent in enemyDestroyDelegates)
            {
                onEnemyDestroyed -= enemyDestroyEvent;
            }

            foreach (var enemyOutOfScreenDelegate in enemyOutOfScreenDelegates)
            {
                onEnemyOutOfScreen -= enemyOutOfScreenDelegate;
            }

            enemyDestroyDelegates.Clear();
            enemyOutOfScreenDelegates.Clear();
        }

        public void Initialize(ComponentContainer componentContainer)
        {
            enemyDestroyDelegates = new List<EnemyDestroyedDelegate>();
            enemyOutOfScreenDelegates = new List<EnemyOutOfScreenDelegate>();
        }

        public void TriggerEnemyDeath(Enemy enemy)
        {
            if (onEnemyDestroyed != null)
            {
                onEnemyDestroyed(enemy);
            }
        }

        public void TriggerEnemyOutOfScreen(Enemy enemy) 
        {
            if (onEnemyOutOfScreen != null) 
            {
                onEnemyOutOfScreen(enemy);
            }
        }
    }
}

