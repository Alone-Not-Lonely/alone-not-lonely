using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquashedObject : MonoBehaviour
{
    BoxContactBehavior boxBehavior;
    Player player;
    private void Start()
    {
        boxBehavior = FindObjectOfType<BoxContactBehavior>();
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        if (boxBehavior.beingHeld && boxBehavior.boxHolder.CompareTag("Player"))
        {
            //transform.eulerAngles = new Vector3(0, 0, 90) + player.transform.eulerAngles;
        }
        else
        {
            //transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
