using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startPosition, length, temp, dist;
    public GameObject myCamera;
    public float parallaxSpeed;

    private void Start()
    {
        GetBackgroundValues();
    }

    public void Update()
    {
        TriggerParallaxEffect();
    }

    private void GetBackgroundValues()
    {
        startPosition = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void TriggerParallaxEffect()
    {
        temp = myCamera.transform.position.y * (1 - parallaxSpeed);
        dist = myCamera.transform.position.y * parallaxSpeed;

        transform.position = new Vector3(transform.position.x, startPosition + dist, transform.position.z);

        if (temp > startPosition + length)
            startPosition += length;
        else if (temp < startPosition - length)
            startPosition -= length;
    }
}