namespace SpaceShooterProject.AI.Enemies
{
    public interface IHelicopter 
    {
        bool IsEnterTheSceneAnimationFinish();
        bool IsShootingSessionEnd();
        bool IsPatrolTimeFinished();
        bool IsDeath();
    }
}

