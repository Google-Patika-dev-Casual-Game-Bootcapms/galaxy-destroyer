using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class X_AxisMovement : IMovement
{
    public void Move(Minion minion)
    {
        float speed = minion.GetSpeed();
        if (minion.GetMovementDirection() == MovementDirection.West)
        {
            speed *= -1;
        }
        minion.transform.Translate(Time.deltaTime * speed,0, 0, null);
    }
}
