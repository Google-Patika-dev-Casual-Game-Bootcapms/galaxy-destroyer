using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathMovement : IMovement
{
    protected int currentRouteIndex;
    protected float tParam;
    protected bool couroutineAllowed;

    public abstract void Initialize(Minion minion);
   

    public void Move(Minion minion)
    {
        if (couroutineAllowed && minion.GetRoutes().Length != 0)
        {
            minion.StartCoroutine(FollowRoute(minion, currentRouteIndex));
        }
    }

    public IEnumerator FollowRoute(Minion minion, int routeIndex)
    {
        couroutineAllowed = false;

        while(tParam < 1)
        {
            tParam += Time.deltaTime * minion.GetSpeed();

            Vector2 newPosition = minion.GetRoute(routeIndex).CalculateBezierCurve(tParam);

            minion.SetPosition(newPosition);

            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        if(currentRouteIndex < minion.GetRoutes().Length - 1)
        {
            currentRouteIndex++;
        }
        else
        {
            currentRouteIndex = 0; // When all routes are finished starts again from beginning
        }

        couroutineAllowed = true;
    }


}
