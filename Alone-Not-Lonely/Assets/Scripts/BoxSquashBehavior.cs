using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSquashBehavior : MonoBehaviour
{
    public bool squashed = false;
    public GameObject squashedVariant;
    public GameObject regularVariant;
    BoxContactBehavior bh;

    private void Start() 
    {
        bh = this.GetComponent<BoxContactBehavior>();
    }

    public void Squash()
    {
        squashed = true;
        if(bh.boxHolder != null)
        {
            bh.boxHolder.GetComponent<Grabber>().ReleaseObject();
        }
    }

    public void UnSquash()
    {
        squashed = false;
        if(bh.boxHolder != null)
        {
            bh.boxHolder.GetComponent<Grabber>().ReleaseObject();
        }
    }
}
