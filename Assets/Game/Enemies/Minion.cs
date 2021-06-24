using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : MonoBehaviour, IMinionController
{

    [SerializeField] private float speed;
    [SerializeField] private float angle;
    protected IMovement movement;

    protected abstract void Initialize();

    void Start()
    {
        Initialize();
    }


    void Update()
    {
        Movement();
    }


    public void Attack()
    {
        throw new System.NotImplementedException();
    }

    public void Death()
    {
        throw new System.NotImplementedException();
    }

    public void Follow()
    {
        throw new System.NotImplementedException();
    }

    public void Movement()
    {
        movement.Move(this);
    }

    public void Path()
    {
        throw new System.NotImplementedException();
    }

    public void Phase()
    {
        throw new System.NotImplementedException();
    }

    public string Type()
    {
        throw new System.NotImplementedException();
    }


    public float GetSpeed()
    {
        return speed;
    }

    public float GetAngle()
    {
        return angle;
    }

}
