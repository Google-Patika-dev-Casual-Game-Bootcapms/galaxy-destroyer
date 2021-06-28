namespace SpaceShooterProject.Component
{
    public enum EnemyType
    {
        RoadTracker = 0,
        Kamikaze = 1,
        FlameThrower = 2,
        HeliA14 = 3,
        HeliA17 = 4,
        COUNT 
    }

    public interface IEnemyFactory
    {
        Enemy ProduceEnemy(EnemyType type);
    }

}