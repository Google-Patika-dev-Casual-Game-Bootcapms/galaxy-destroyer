namespace SpaceShooterProject.AI.Movements
{
    using SpaceShooterProject.AI.Enemies;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class PathMovement : IMovement
    {
        protected int currentRouteIndex;
        protected float tParam;
        protected bool couroutineAllowed = true;

        //public abstract void Initialize(Enemy minion);


        public void Move(Enemy enemy)
        {
            if (couroutineAllowed && enemy.GetRoutes().Count != 0)
            {
                enemy.StartCoroutine(FollowRoute(enemy, currentRouteIndex));
            }
        }

        public void Patrol(Enemy enemy)
        {
            if (couroutineAllowed && enemy.GetRoutes().Count != 0)
            {
                enemy.StartCoroutine(PatrolRoute(enemy, currentRouteIndex));
            }
        }

        public void BossPatrol(Enemy enemy)
        {
            if (couroutineAllowed && enemy.GetRoutes().Count != 0)
            {
                enemy.StartCoroutine(BossPatrol(enemy, currentRouteIndex));
            }
        }

        public IEnumerator PatrolRoute(Enemy enemy, int routeIndex)
        { 
            couroutineAllowed = false;
            tParam = 0f;
            bool isReverse = false;
            while (!enemy.IsOutOfScreen())
            {
                if (tParam >= 1) tParam = Mathf.Floor(tParam);
                if (tParam <= 0) tParam = Mathf.Ceil(tParam);
                while (tParam <= 1 && tParam >= 0)
                {
                    if (enemy.IsMovementInterrupted())
                    {
                        yield return new WaitUntil(() => enemy.IsMovementContinue());
                    }
                    

                    if (isReverse)
                    {
                        tParam -= Time.deltaTime * enemy.GetSpeed();
                    }
                    else
                    {
                        tParam += Time.deltaTime * enemy.GetSpeed();
                    }


                    Vector2 newPosition = enemy.GetRoute(routeIndex).CalculateBezierCurve(tParam);

                    enemy.SetPosition(newPosition);

                    yield return new WaitForEndOfFrame();
                    
                    
                }
                isReverse = !isReverse;
                
                
            }


            if (currentRouteIndex < enemy.GetRoutes().Count - 1)
            {
                currentRouteIndex++;
            }
            else
            {
                currentRouteIndex = 0; // When all routes are finished starts again from beginning
            }


            couroutineAllowed = true;
            Debug.Log("Before on out of screen.");
            enemy.OnOutOfScreen();

        }

        public IEnumerator FollowRoute(Enemy enemy, int routeIndex)
        {
            couroutineAllowed = false;
            tParam = 0f;
            
            
            while (tParam < 1)
            {
                    
                tParam += Time.deltaTime * enemy.GetSpeed();
                    


                Vector2 newPosition = enemy.GetRoute(routeIndex).CalculateBezierCurve(tParam);

                enemy.SetPosition(newPosition);

                if (enemy.IsOutOfScreen())
                {
                    enemy.OnOutOfScreen();
                    break;
                }

                yield return new WaitForEndOfFrame();
            }

            tParam = 0f;

            couroutineAllowed = true;

            enemy.RouteFinished();
        }

        public IEnumerator BossPatrol(Enemy enemy, int routeIndex)
        {
            couroutineAllowed = false;
            int currentRouteIndex = 0;
            

            while (!enemy.IsOutOfScreen())
            {
                currentRouteIndex = currentRouteIndex % enemy.GetRoutes().Count;
                tParam = 0f;
                while (tParam < 1)
                {

                    tParam += Time.deltaTime * enemy.GetSpeed();



                    Vector2 newPosition = enemy.GetRoute(currentRouteIndex).CalculateBezierCurve(tParam);

                    enemy.SetPosition(newPosition);

                    if (enemy.IsOutOfScreen())
                    {
                        break;
                    }

                    yield return new WaitForEndOfFrame();
                }

                if (enemy.IsOutOfScreen())
                {
                    enemy.OnOutOfScreen();
                    break;
                }

                enemy.PatrolRouteFinished();


                yield return new WaitUntil(() => enemy.IsMovementContinue());
                currentRouteIndex++;
            }
            couroutineAllowed = true;
            tParam = 0f;
            


        }






    }
}