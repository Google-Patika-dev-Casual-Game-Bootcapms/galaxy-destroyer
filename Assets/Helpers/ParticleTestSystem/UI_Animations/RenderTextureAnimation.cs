using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureAnimation : MonoBehaviour
{
    public Animator animator,animator_Button;

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void buttonClicked()
    {
        AnimatorStateInfo a =  animator.GetCurrentAnimatorStateInfo(0);
        if (a.IsName("Closed"))
        {
            open();
        }
        else
            close();
    }

    public void open()
    {
        animator.SetTrigger("Open");
        animator_Button.SetTrigger("Open");
    }
    public void close()
    {
        animator.SetTrigger("Close");
        animator_Button.SetTrigger("Close");
    }
}
