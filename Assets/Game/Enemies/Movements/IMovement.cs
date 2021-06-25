using System;
using UnityEngine;
public interface IMovement
{
    public void Initialize(Minion minion);
    public void Move(Minion minion);
}
