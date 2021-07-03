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

        private Camera gameCamera = null;

        private Player player;// TODO refactor!!!

        public void Init()
        {
            gameCamera = Camera.main;
            player = GameObject.FindObjectOfType<Player>();

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
            float height = 2f * gameCamera.orthographicSize;
            float width = height * gameCamera.aspect;            

            var spawnEnemyPosition = new Vector2(0, player.transform.position.y + height * .7f);

            var currentWaveData = levelWaveData.waveDatas[levelIndex];

            for (int i = 0; i < (int)EnemyType.COUNT; i++)
            {
                spawnEnemyPosition = new Vector2(-width * 0.17f, player.transform.position.y + height * .7f + i * height * 0.1f);
                var spawnHeight = spawnEnemyPosition.y;
                for (int j = 0; j < currentWaveData.waveInfo[i]; j++)
                {
                    var enemy = ProduceEnemy((EnemyType)i);
                    enemy.transform.position = spawnEnemyPosition;
                    spawnEnemyPosition = new Vector2(enemy.transform.position.x + width * 0.1f, spawnHeight);
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

