namespace SpaceShooterProject.Component
{
    public enum EnemyType
    {
        RoadTracker = 1,
        Kamikaze = 2,
        FlameThrower = 3,
        HeliA14 = 4,
        HeliA17 = 5
    }

    public interface IEnemyFactory
    {
        IEnemy ProduceEnemy(EnemyType type);
    }

}