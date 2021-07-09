using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private float startPosition, length, temp, dist;
    private GameObject myCamera;
    public float parallaxSpeed;
    

    private void Start()
    {
        SetCamera();
        SetBackgroundPosition();
        GetBackgroundValues();
    }
    //TODO: Refactor use callupdate in inGameState 
    public void Update()
    {
        TriggerParallaxEffect();
    }

    private void GetBackgroundValues()
    {
        startPosition = transform.position.y;
        length = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void SetCamera()
    {
        myCamera = Camera.main.gameObject;
    }

    private void SetBackgroundPosition()
    {
        transform.position = new Vector3(0, myCamera.transform.position.y, transform.position.z);
    }

    private void TriggerParallaxEffect()
    {
        var cameraPos = myCamera.transform.position;
        temp = cameraPos.y * (1 - parallaxSpeed);
        dist = cameraPos.y * parallaxSpeed;

        var position = transform.position;
        position = new Vector3(position.x, startPosition + dist, position.z);
        transform.position = position;

        if (temp > startPosition + length)
            startPosition += length;
        else if (temp < startPosition - length)
            startPosition -= length;
    }
}