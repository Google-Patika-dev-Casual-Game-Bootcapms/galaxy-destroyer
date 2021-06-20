using System;
using System.Collections;
using System.Security.Cryptography;
using Devkit.Base.Object;
using Devkit.Base.Pattern.ObjectPool;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour, IPoolable, IUpdatable
{
    [SerializeField] private float speed = 3;
    [SerializeField] private RectTransform _transform;

    public delegate void BulletDelegate();

    public BulletDelegate OnHitEnemy;

    private float maxVerticalPosition;
    private Vector2 endPosition;
    float elapsedTime = 0;
    Vector2 startingPos;
    private Tween moveTween;


    private void OnEnable()
    {
        startingPos = _transform.position;
        maxVerticalPosition = Camera.main.ViewportToWorldPoint(new Vector2(Random.value, 1)).y;
        endPosition = new Vector2(startingPos.x, maxVerticalPosition + 1);

        //StartCoroutine(Move(speed));
        moveTween = _transform.DOMoveY(endPosition.y, 2f)
            .OnComplete(() =>
            {
                
            });
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            moveTween.Kill();
            OnHitEnemy();
        }
    }

    private void OnDisable()
    {
        moveTween.Kill();
    }

    private void FixedUpdate()
    {
    }

    IEnumerator Move(float duration)
    {
        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
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
    }

    public void CallUpdate()
    {
        transform.position = Vector3.Lerp(startingPos, endPosition, speed * Time.deltaTime);
    }
}