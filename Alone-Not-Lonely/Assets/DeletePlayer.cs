using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Player _player = FindObjectOfType<Player>();
        Destroy(Player.instance);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
