using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeEvent : MonoBehaviour
{
    public Animator[] animationList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Shake()
    {
        foreach(Animator anim in animationList)
        {
            anim.SetBool("Play", true);
        }
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Shake();
        }
    }
}
