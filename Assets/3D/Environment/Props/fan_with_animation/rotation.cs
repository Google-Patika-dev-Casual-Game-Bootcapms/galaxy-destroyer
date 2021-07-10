using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    public GameObject fan;
    public int rotation_speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        fan.transform.Rotate(Vector3.up, rotation_speed * Time.deltaTime, Space.World);
    }
}
