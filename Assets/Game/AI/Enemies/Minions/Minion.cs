using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minion : MonoBehaviour
{
    [SerializeField] private Route[] routes;
    [SerializeField] private float speed;

    [SerializeField]
    Sprite minionSprite;

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

    public void Movement()
    {
        movement.Move(this);
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
