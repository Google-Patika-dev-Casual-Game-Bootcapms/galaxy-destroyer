using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMinion : Minion
{
    protected override void Initialize()
    {
        movement = new IndependentMovement();
    }
}
