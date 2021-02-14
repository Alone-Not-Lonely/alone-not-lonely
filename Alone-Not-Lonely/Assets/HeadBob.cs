using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basically should work by putting camera on a sine curve
//Camera will be moved along a set path then quickly lerp back to standard position
public class HeadBob : MonoBehaviour
{
    public float headAmplitude = 1f, headSpeed = 0.3f;
    private float startingY, headProgress = 0; 
    private Player _player;
    //private bool moving = false;
    private Vector3 lastLocation;
    
    
    // Start is called before the first frame update
    void Start()
    {
        startingY = transform.position.y;
        _player = (Player)FindObjectOfType(typeof(Player));
        lastLocation = _player.transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //will need to handle case of jumping
        if(_player.transform.position != lastLocation){
            //player is moving
            lastLocation = _player.transform.position;
            headProgress += headSpeed;
        }
        else
        {//Player is not moving
            headProgress = 0;
        }

        bobHead();
    }

    void bobHead()
    {
        float newHeight = (Mathf.Sin(headProgress)*headAmplitude) + 2.5f;
        Debug.Log(newHeight);
        transform.position = _player.transform.position + new Vector3(0, newHeight, 0); //new Vector3(_player.transform.position.x, newHeight, _player.transform.position.z);
    }


}
