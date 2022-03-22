using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButterFly : MonoBehaviour
{
    Animator anim;

    public void Begin()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isTracking", true);
    }

    public void End()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("isTracking", false);
    }
}
