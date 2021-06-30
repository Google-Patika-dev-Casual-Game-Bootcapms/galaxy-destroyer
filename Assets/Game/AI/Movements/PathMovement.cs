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

        public IEnumerator PatrolRoute(Enemy minion, int routeIndex)
        {
            int counter = 10;
            couroutineAllowed = false;
            tParam = 0f;
            bool isReverse = false;
            while (counter > 0)
            {
                if (tParam >= 1) tParam = Mathf.Floor(tParam);
                if (tParam <= 0) tParam = Mathf.Ceil(tParam);
                Debug.Log(isReverse);
                Debug.Log(tParam);
                while (tParam <= 1 && tParam >= 0)
                {
                    if (isReverse)
                    {
                        tParam -= Time.deltaTime * minion.GetSpeed();
                    }
                    else
                    {
                        tParam += Time.deltaTime * minion.GetSpeed();
                    }
                    

                    Vector2 newPosition = minion.GetRoute(routeIndex).CalculateBezierCurve(tParam);

                    minion.SetPosition(newPosition);

                    yield return new WaitForEndOfFrame();
                }
                isReverse = !isReverse;
                counter--;
                
            }


            if (currentRouteIndex < minion.GetRoutes().Count - 1)
            {
                currentRouteIndex++;
            }
            else
            {
                currentRouteIndex = 0; // When all routes are finished starts again from beginning
            }

            couroutineAllowed = true;

        }

        public IEnumerator FollowRoute(Enemy minion, int routeIndex)
        {
            couroutineAllowed = false;
            tParam = 0f;
            
            
                while (tParam < 1)
                {
                    
                    tParam += Time.deltaTime * minion.GetSpeed();
                    


                    Vector2 newPosition = minion.GetRoute(routeIndex).CalculateBezierCurve(tParam);

                    minion.SetPosition(newPosition);

                    yield return new WaitForEndOfFrame();
                }

            tParam = 0f;

            if (currentRouteIndex < minion.GetRoutes().Count - 1)
            {
                currentRouteIndex++;
            }
            else
            {
                currentRouteIndex = 0; // When all routes are finished starts again from beginning
            }
            
            couroutineAllowed = true;


            minion.RouteFinished();
        }






    }
}