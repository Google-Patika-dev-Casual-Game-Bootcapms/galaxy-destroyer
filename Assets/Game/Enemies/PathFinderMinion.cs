using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinderMinion : Minion
{
    
    //[SerializeField] public RectTransform minionImage = null;
    
    private string type = "Flying";

    protected override void Initialize()
    {
        movement = new Y_AxisMovement();
    }
}
