namespace SpaceShooterProject.Component
{
    using UnityEngine;
    using System.Collections;
    using Devkit.Base.Pattern.ObjectPool;
    using Devkit.Base.Object;

    public class EnemyFactory : IEnemyFactory, IInitializable
    {
        private Pool<RoadTracker> roadTrackerPool;
        private const string SOURCE_OBJECT_PATH = "Prefabs/RoadTrackerEnemy";

        public void Init()
        {
            roadTrackerPool = new Pool<RoadTracker>(SOURCE_OBJECT_PATH);
            roadTrackerPool.PopulatePool(5);
        }

        public void PreInit()
        {
           // TODO: Refector
        }

        public IEnemy ProduceEnemy(EnemyType type)
        {
            IEnemy enemy = null;
            switch (type)
            {
                case EnemyType.RoadTracker:
                    enemy = roadTrackerPool.GetObjectFromPool();
                    break;
            }
            return enemy;
        }
    }

}

