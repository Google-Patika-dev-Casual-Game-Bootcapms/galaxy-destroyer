namespace SpaceShooterProject.AI.Enemies
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class Helicopter : Minion, IHelicopter
    {

        public bool IsDeath()
        {
            return false;
        }

        public bool IsEnterTheSceneAnimationFinish()
        {
            return false;
        }

        public bool IsPatrolTimeFinished()
        {
            return false;
        }

        public bool IsShootingSessionEnd()
        {
            return false;
        }


    }

}


