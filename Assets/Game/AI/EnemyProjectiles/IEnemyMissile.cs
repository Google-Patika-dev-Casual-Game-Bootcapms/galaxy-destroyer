namespace SpaceShooterProject.AI.Projectiles
{
   
    using System;
    public interface IEnemyMissile
    {
        public void InjectEnemyMissileCollector(IMissileCollector enemyMissileCollector);
    }

}
