using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevSerhatSpaceShipAnimationTest : MonoBehaviour
{
    private Animator MyAnimator;
    void Start()
    {
        MyAnimator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MyAnimator.SetBool("right", false);
            MyAnimator.SetBool("left", true);
            
        }
        if (Input.GetKey(KeyCode.D))
        {
            MyAnimator.SetBool("left", false);
            MyAnimator.SetBool("right", true);
        }
        if (Input.GetKey(KeyCode.S))
        {
            MyAnimator.SetBool("crash", true);
        }
       
    }
}
