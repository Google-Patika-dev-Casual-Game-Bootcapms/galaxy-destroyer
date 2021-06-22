using System;
using System.Collections;
using System.Security.Cryptography;
using Devkit.Base.Object;
using Devkit.Base.Pattern.ObjectPool;
using DG.Tweening;
using SpaceShooterProject.Component;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private RectTransform _transform;

    public delegate void BulletTriggerDelegate();

    public delegate void BulletPoolDelegate(Bullet bullet);

    public BulletTriggerDelegate OnHitEnemy;
    public BulletPoolDelegate OnBulletOutOfScreen;

    private GameCamera gameCamera;

    private void Update()
    {
        _transform.Translate(Vector3.up * (speed + gameCamera.CameraSpeed) * Time.deltaTime,
            Space.World);
        if (_transform.position.y > Camera.main.ViewportToWorldPoint(new Vector2(Random.value, 1)).y)
            OnBulletOutOfScreen(this);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            OnHitEnemy();
        }
    }

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public void Initialize()
    {
        gameCamera = Camera.main.GetComponent<GameCamera>();
    }
}