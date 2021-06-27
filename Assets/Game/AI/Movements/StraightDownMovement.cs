using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightDownMovement : PathMovement
{

    public override void Initialize(Minion minion)
    {
        RoadTracker pathFinderMinion = minion as RoadTracker;

        currentRouteIndex = 0;
        tParam = 0f;
        couroutineAllowed = true;

        Route linearRoute = new GameObject().AddComponent<LinearRoute>() as LinearRoute;

        Transform controlPoint1 = new GameObject().transform;
        controlPoint1.position = pathFinderMinion.transform.position;

        Transform controlPoint2 = new GameObject().transform;
        controlPoint2.position = pathFinderMinion.transform.position;
        controlPoint2.Translate(0, pathFinderMinion.GetPathLength(), 0);

        linearRoute.AddControlPoint(controlPoint1);
        linearRoute.AddControlPoint(controlPoint2);

        pathFinderMinion.AddRoute(linearRoute);

    }

    
}
