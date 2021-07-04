namespace SpaceShooterProject.AI.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public interface ISimpleBoss
    {
        bool IsEnterTheSceneAnimationFinish();
        bool IsShootingSessionEnd();
        bool IsPatrolTimeFinished();
        bool IsDeath();
    }

}
