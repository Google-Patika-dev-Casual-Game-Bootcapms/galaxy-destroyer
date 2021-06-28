namespace SpaceShooterProject.Component
{
    using UnityEngine;
    using System.Collections;
    using Devkit.Base.Pattern.ObjectPool;
    using Devkit.Base.Object;

    public class EnemyFactory : IEnemyFactory, IInitializable
    {

        private Pool<RoadTracker> roadTrackerPool;
        private const string ROADTRACKER_OBJECT_PATH = "Prefabs/RoadTracker";

        private Pool<Kamikaze> kamikazePool;
        private const string KAMIKAZE_OBJECT_PATH = "Prefabs/Kamikaze";

        private Pool<FlameThrower> FlameThrowerPool;
        private const string FLAMETHROWER_OBJECT_PATH = "Prefabs/FlameThrower";

        private Pool<HeliA14> HeliA14Pool;
        private const string HELIA14_OBJECT_PATH = "Prefabs/HeliA14";

        private Pool<HeliA17> HeliA17Pool;
        private const string HELIA17_OBJECT_PATH = "Prefabs/HeliA17";

        private WaveData waveData1;

        private WaveData[] waveData;

        private LevelWaveData levelWaveData;



        public void Init()
        {
            waveData1 = new WaveData();

            waveData1.waveInfo[0] = 3;

            waveData1.waveInfo[1] = 5;

            waveData1.waveInfo[2] = 4;

            waveData1.waveInfo[3] = 5;

            waveData1.waveInfo[4] = 6;

            levelWaveData = new LevelWaveData(1);

            levelWaveData.SetLevelWaveData(0, waveData1);

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

        public Enemy ProduceEnemy(EnemyType type)
        {
            Enemy enemy = null;
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

        public void SpawnWaveEnemies(LevelWaveData levelWaveData, int levelIndex)
        {
            var spawnEnemyPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, 1));

            var currentWaveData = levelWaveData.waveDatas[levelIndex];

            for (int i = 0; i < (int)EnemyType.COUNT; i++)
            {
                for (int j = 0; j < currentWaveData.waveInfo[i]; j++)
                {
                    var enemy = ProduceEnemy((EnemyType)i);
                    var enemyWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(spawnEnemyPosition.x, spawnEnemyPosition.y,Camera.main.nearClipPlane));
                    enemy.transform.position = enemyWorldPosition;
                }
            }
        }
        public void SpawnEnemies()
        {
            SpawnWaveEnemies(levelWaveData,0);
        }
    }

    public class LevelWaveData
    {
        public WaveData[] waveDatas;
        public LevelWaveData(int wavecount)
        {
            waveDatas = new WaveData[wavecount];
        }

        public void SetLevelWaveData(int levelIndex, WaveData waveData)
        {
            if (levelIndex >= waveDatas.Length)
            {
                return;
            }

            waveDatas[levelIndex] = waveData;

        }
    }

    public class WaveData
    {
        public int[] waveInfo = new int[(int)EnemyType.COUNT];
    }
}

