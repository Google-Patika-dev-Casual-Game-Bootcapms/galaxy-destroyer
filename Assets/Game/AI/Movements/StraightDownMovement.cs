namespace SpaceShooterProject.AI.Movements
{
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System;
    using UnityEngine;
    public class StraightDownMovement : IMovement
    {
        protected bool couroutineAllowed = true;

        public void Move(Enemy enemy)
        {
            if (couroutineAllowed)
            {
                enemy.StartCoroutine(MoveDown(enemy));
            }
        }
        public void Patrol(Enemy enemy)
        {

        }

        public IEnumerator MoveDown(Enemy enemy)
        {
            couroutineAllowed = false;
            do
            {
                Vector2 translationVector;
                translationVector.y = -1 * enemy.GetSpeed();
                translationVector.x = 0;
                enemy.SetPosition(enemy.GetPosition() + translationVector);
                yield return new WaitForEndOfFrame();

            } while (!enemy.IsOutOfScreen());

            couroutineAllowed = true;
            enemy.OnOutOfScreen();

            
        }

        public void BossPatrol(Enemy enemy)
        {
            throw new NotImplementedException();
        }
    }

}
