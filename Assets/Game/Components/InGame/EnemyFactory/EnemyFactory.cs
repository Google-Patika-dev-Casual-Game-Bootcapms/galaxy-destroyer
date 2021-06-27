namespace SpaceShooterProject.Component
{
    using UnityEngine;
    using System.Collections;
    using Devkit.Base.Pattern.ObjectPool;
    using Devkit.Base.Object;

    public class EnemyFactory : IEnemyFactory, IInitializable
    {

        private Pool<RoadTracker> roadTrackerPool;
        private const string ROADTRACKER_OBJECT_PATH = "Prefabs/RoadTrackerEnemy";

        private Pool<Kamikaze> kamikazePool;
        private const string KAMIKAZE_OBJECT_PATH = "Prefabs/KamikazeEnemy";

        private Pool<FlameThrower> FlameThrowerPool;
        private const string FLAMETHROWER_OBJECT_PATH = "Prefabs/FlameThrowerEnemy";

        private Pool<HeliA14> HeliA14Pool;
        private const string HELIA14_OBJECT_PATH = "Prefabs/HeliA14Enemy";

        private Pool<HeliA17> HeliA17Pool;
        private const string HELIA17_OBJECT_PATH = "Prefabs/HeliA17Enemy";

        public void Init()
        {
            roadTrackerPool = new Pool<RoadTracker>(ROADTRACKER_OBJECT_PATH);
            roadTrackerPool.PopulatePool(5);

            kamikazePool = new Pool<Kamikaze>(KAMIKAZE_OBJECT_PATH);
            kamikazePool.PopulatePool(5);

            FlameThrowerPool = new Pool<FlameThrower>(FLAMETHROWER_OBJECT_PATH);
            FlameThrowerPool.PopulatePool(5);

            HeliA14Pool = new Pool<HeliA14>(HELIA14_OBJECT_PATH);
            HeliA14Pool.PopulatePool(5);

            HeliA17Pool = new Pool<HeliA17>(HELIA17_OBJECT_PATH);
            HeliA17Pool.PopulatePool(5);
        }

        public void PreInit()
        {
           // TODO: Refactor
        }

        public IEnemy ProduceEnemy(EnemyType type)
        {
            IEnemy enemy = null;
            switch (type)
            {
                case EnemyType.RoadTracker:
                    enemy = roadTrackerPool.GetObjectFromPool();
                    break;
                case EnemyType.Kamikaze:
                    enemy = kamikazePool.GetObjectFromPool();
                    break;
                case EnemyType.FlameThrower:
                    enemy = FlameThrowerPool.GetObjectFromPool();
                    break;
                case EnemyType.HeliA14:
                    enemy = HeliA14Pool.GetObjectFromPool();
                    break;
                case EnemyType.HeliA17:
                    enemy = HeliA17Pool.GetObjectFromPool();
                    break;
            }
            return enemy;
        }
    }

}

