using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : MonoBehaviour, IMinionController
{
    [SerializeField] private Route[] routes;
    [SerializeField] private float speed;
    protected IMovement movement;

    protected abstract void Initialize();

    void Start()
    {
        Initialize();
        movement.Initialize(this);
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

    public Route[] GetRoutes()
    {
        return routes;
    }

    public Route GetRoute(int index)
    {
        return routes[index];
    }

    public void SetPosition(Vector2 newPosition)
    {
        this.transform.position = newPosition;
    }

    public Vector2 GetPosition()
    {
        return this.transform.position;
    }

    public void AddRoute(Route newRoute)
    {
        routes[routes.Length] = newRoute;
    }

}
