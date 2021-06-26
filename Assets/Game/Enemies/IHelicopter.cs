namespace SpaceShooterProject.AI 
{
    public interface IHelicopter 
    {
        bool IsEnterTheSceneAnimationFinish();
        bool IsShootingSessionEnd();
        bool IsPatrolTimeFinished();
        bool IsDeath();
    }
}

