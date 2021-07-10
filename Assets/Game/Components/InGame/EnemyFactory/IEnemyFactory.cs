namespace SpaceShooterProject.Component
{
    using SpaceShooterProject.AI.Enemies;

    public enum EnemyType
    {
        StraightRoadTracker = 0,
        WaveRoadTracker = 1,
        HeliA14 = 2,
        HeliA17 = 3,
        COUNT 
    }

    public interface IEnemyFactory
    {
        Enemy ProduceEnemy(EnemyType type);
    }

}