namespace SpaceShooterProject.Component
{
    using System.Collections.Generic;
    using Devkit.Base.Object;
    using Devkit.Base.Pattern.ObjectPool;

    public class BulletCollector : IBulletCollector, IDestructible
    {
        private Pool<Bullet> pool;
        private const string SOURCE_OBJECT_PATH = "Prefabs/FriendlyBulletForPooling";
        private List<Bullet> activeBullets = new List<Bullet>();

        public BulletCollector()
        {
            pool = new Pool<Bullet>(SOURCE_OBJECT_PATH);
            pool.PopulatePool(20);
        }

        public Bullet GetBullet()
        {
            var bullet = pool.GetObjectFromPool();
            bullet.InjectBulletCollector(this);
            if (!activeBullets.Contains(bullet))
                activeBullets.Add(bullet);

            return bullet;
        }

        public void AddBulletToPool(Bullet bullet)
        {
            pool.AddObjectToPool(bullet);
        }

        public void UpdateBullets()
        {
            foreach (var activeBullet in activeBullets)
            {
                if (activeBullet.isActiveAndEnabled)
                    activeBullet.CallUpdate();
            }
        }

        public void OnDestruct()
        {
            //TODO:
            // remove all live bullets from scene
                // then send it to the pool

        }
    }
}