namespace SpaceShooterProject.AI.Movements
{
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System;
    using UnityEngine;
    public class WaveMovement : IMovement
    {
        protected bool couroutineAllowed = true;

        public void Move(Enemy enemy)
        {
            if (couroutineAllowed)
            {
                enemy.StartCoroutine(MoveWaveDown(enemy));
            }
        }
        public void Patrol(Enemy enemy)
        {

        }

        public IEnumerator MoveWaveDown(Enemy enemy)
        {
            couroutineAllowed = false;
            bool isIncreasing = true;
            float angle = 1;
            do
            {
                if(angle == 180 || angle == 0)
                {
                    isIncreasing = !isIncreasing;
                }
                if (isIncreasing)
                {
                    angle += 1;
                }
                else
                {
                    angle -= 1;
                }
                Vector2 translationVector;
                translationVector.y = -1 * Mathf.Sin(Mathf.Deg2Rad * angle) * enemy.GetSpeed();
                translationVector.x = Mathf.Cos(Mathf.Deg2Rad * angle) * enemy.GetSpeed();
                enemy.SetPosition(enemy.GetPosition() + translationVector);
                yield return new WaitForEndOfFrame();

            } while (!enemy.IsOutOfScreen());

            couroutineAllowed = true;
            enemy.OnOutOfScreen();


        }
    }
}


