using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadTracker : Minion
{
    [SerializeField]
    private float pathLength;

    protected override void Initialize()
    {
        movement = new StraightDownMovement();
    }

    public float GetPathLength()
    {
        return pathLength;
    }
}
