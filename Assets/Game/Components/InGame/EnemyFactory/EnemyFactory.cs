namespace SpaceShooterProject.Component
{
    using UnityEngine;
    using System.Collections;
    using Devkit.Base.Pattern.ObjectPool;
    using Devkit.Base.Object;
    using System.Collections.Generic;

    public class EnemyFactory : IEnemyFactory, IInitializable, IUpdatable, IDestructible
    {

        private Pool<RoadTracker> roadTrackerPool;
        private const string ROADTRACKER_OBJECT_PATH = "Prefabs/RoadTracker";

        private Pool<Kamikaze> kamikazePool;
        private const string KAMIKAZE_OBJECT_PATH = "Prefabs/Kamikaze";

        private Pool<FlameThrower> flameThrowerPool;
        private const string FLAMETHROWER_OBJECT_PATH = "Prefabs/FlameThrower";

        private Pool<HeliA14> heliA14Pool;
        private const string HELIA14_OBJECT_PATH = "Prefabs/HeliA14";

        private Pool<HeliA17> heliA17Pool;
        private const string HELIA17_OBJECT_PATH = "Prefabs/HeliA17";

        private WaveData waveData1;

        private WaveData[] waveData;

        private LevelWaveData levelWaveData;
        
        private Camera gameCamera = null;

        private Player player;// TODO refactor!!!
        private InGameMessageBroadcaster inGameMessageBroadcaster;

        private List<Enemy> liveEnemies;

        public EnemyFactory(InGameMessageBroadcaster inGameMessageBroadcaster)
        {
            this.inGameMessageBroadcaster = inGameMessageBroadcaster;
        }

        private EnemyFactory() { }

        public void Init()
        {
            liveEnemies = new List<Enemy>();
            gameCamera = Camera.main;
            player = GameObject.FindObjectOfType<Player>();
            inGameMessageBroadcaster.OnEnemyDestroyed += OnEnemyDestroyed;

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

            flameThrowerPool = new Pool<FlameThrower>(FLAMETHROWER_OBJECT_PATH);
            flameThrowerPool.PopulatePool(5);

            heliA14Pool = new Pool<HeliA14>(HELIA14_OBJECT_PATH);
            heliA14Pool.PopulatePool(5);

            heliA17Pool = new Pool<HeliA17>(HELIA17_OBJECT_PATH);
            heliA17Pool.PopulatePool(5);

        }

        //TODO solve cyclomatic complexity and casting issues!!! // TODO: refactor
        private void OnEnemyDestroyed(Enemy enemy)
        {
            AddEnemyToPool(enemy);

            if (liveEnemies.Contains(enemy)) 
            {
                enemy.gameObject.SetActive(false);
                liveEnemies.Remove(enemy);
            }

        }

        private void AddEnemyToPool(Enemy enemy)
        {
            switch (enemy.GetEnemyType())
            {
                case EnemyType.RoadTracker:
                    roadTrackerPool.AddObjectToPool((RoadTracker)enemy);
                    break;
                case EnemyType.Kamikaze:
                    kamikazePool.AddObjectToPool((Kamikaze)enemy);
                    break;
                case EnemyType.FlameThrower:
                    flameThrowerPool.AddObjectToPool((FlameThrower)enemy);
                    break;
                case EnemyType.HeliA14:
                    heliA14Pool.AddObjectToPool((HeliA14)enemy);
                    break;
                case EnemyType.HeliA17:
                    heliA17Pool.AddObjectToPool((HeliA17)enemy);
                    break;
                default:
                    break;
            }
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
                    enemy = flameThrowerPool.GetObjectFromPool();
                    break;
                case EnemyType.HeliA14:
                    enemy = heliA14Pool.GetObjectFromPool();
                    break;
                case EnemyType.HeliA17:
                    enemy = heliA17Pool.GetObjectFromPool();
                    break;
            }

            enemy.ResetHealth();
            enemy.SetType(type);//TODO call when the object is initialized!!!
            enemy.InjectMessageBroadcaster(inGameMessageBroadcaster);
            enemy.gameObject.SetActive(true);
            liveEnemies.Add(enemy);
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

        public void CallUpdate()
        {
            for (int i = 0; i < liveEnemies.Count; i++)
            {
                liveEnemies[i].CallUpdate();
            }
        }

        public void OnDestruct()
        {
            while (liveEnemies.Count > 0)
            {
                liveEnemies[liveEnemies.Count - 1].OnDestruct();
                OnEnemyDestroyed(liveEnemies[liveEnemies.Count - 1]);
            }

            liveEnemies.Clear();
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

