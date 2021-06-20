using System;
using UnityEngine;


public class Y_AxisMovement : IMovement
{

    public void Move(Minion minion)
    {
        float speed = minion.GetSpeed();
        if (minion.GetMovementDirection() == MovementDirection.South)
        {
            speed *= -1;
        }
        minion.transform.Translate(0,Time.deltaTime*speed,0,null);
    }
}
