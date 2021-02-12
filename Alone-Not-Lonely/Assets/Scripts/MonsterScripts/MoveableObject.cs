using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveableObject : MonoBehaviour
{
    private Rigidbody thisRB;
    public float cooldown = 2f;
    private float currCooldown = 0f;
    private bool inCooldown;
    // Start is called before the first frame update
    void Start()
    {
        thisRB = GetComponent<Rigidbody>();
        inCooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(inCooldown)
        {
            currCooldown += Time.deltaTime;
        }
        if(inCooldown && currCooldown >= cooldown)
        {
            currCooldown = 0;
            inCooldown = false;
            thisRB.WakeUp();
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Portal"))
        {
            thisRB.Sleep();
            inCooldown = true;
        }
    }
}
