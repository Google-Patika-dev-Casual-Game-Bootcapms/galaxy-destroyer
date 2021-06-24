using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightMovement : IMovement
{
    
    public void Move(Minion minion)
    {
        float speed = minion.GetSpeed();
        float angle = minion.GetAngle();
        float x_speed = speed * Mathf.Sin(Mathf.Deg2Rad * angle);
        float y_speed = speed * Mathf.Cos(Mathf.Deg2Rad * angle);

        minion.transform.Translate(Time.deltaTime * x_speed, Time.deltaTime * y_speed, 0, null);
    }
}
