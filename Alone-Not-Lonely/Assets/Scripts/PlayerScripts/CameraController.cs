using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // player input object
    private DefaultControls _actionMap;

    // movement constraints
    public float minimumX = -60f;
    public float maximumX = 60f;
    public float minimumY = -306f;
    public float maximum = 360f;

    // sensitivity controls
    public float sensitivityX = 15f;
    public float sensitivityY = 15f;

    // camera reference
    public Camera cam;

    // current roation
    float rotationX = 0f;
    float rotationY = 90f;

    //controller input values:
    float inX = 0f;
    float iny = 0f;

    //headbob values
    public float headAmplitude = 1f, headSpeed = 0.3f;
    private float startingY, headProgress = 0;
    private Player _player;
    private Vector3 lastLocation;
    public bool headbob = true;

    private Player player;

    private Vector3 velocity = Vector3.zero;
    public bool cameraFree = true;

    // Start is called before the first frame update
    void Start()
    {
        //Moved mouse logic to Pause Menu Controller
        player = Player.instance;
        
        player._actionMap.Platforming.Camera.performed += look =>
        {
            inX = look.ReadValue<Vector2>().x;
            iny = look.ReadValue<Vector2>().y;
        };

        player._actionMap.Platforming.Camera.canceled += look =>
        {
            inX = 0;
            iny = 0;
        };
    }

    void Update()
    {
        if (!player.paused && cameraFree)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            // update current values
            rotationY += inX * sensitivityX * Time.deltaTime;
            rotationX += iny * sensitivityY * Time.deltaTime;

            // constrain x
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

            // rotate game objects accordingly
            transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);
            transform.position = new Vector3(player.transform.position.x, (player.transform.position.y + 2f + getBobHeight()), player.transform.position.z);
        }
        //else
        //{
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        //}
    }

    void FixedUpdate()
    {
        //progresses head bob height by monitoring player motion
        if (!player.paused && headbob)
        {
            //will need to handle case of jumping
            if (player.transform.position.x != lastLocation.x || player.transform.position.z != lastLocation.z)
            {
                //player is moving
                lastLocation = player.transform.position;
                headProgress += headSpeed;
            }
            else
            {//Player is not moving
                headProgress = 0;
            }
        }
    }

    //Determines addition to Y placement by amount of time player has been walking
    float getBobHeight()
    {
        if (headbob)
        {
            return (Mathf.Sin(headProgress) * headAmplitude);
            //fun future idea: tie in with player move speed        
        }
        else
        {
            return 0;
        }
     
    }

    public Vector3 GetCameraRotation()
    {
        return new Vector3(-rotationX, rotationY, 0);
    }

     public void OnSliderValueChanged(float value)
    {
        sensitivityX = 5 + (value * 30);
        sensitivityY = 5 + (value * 30);
    }

    public void MoveToFixedPosition(Vector3 position, GameObject targetLookAt)
    {
        this.transform.position = position;
        this.transform.rotation = Quaternion.Euler(90, 90, 0);
        //this.transform.LookAt(targetLookAt.transform);
        cameraFree = false;
    }
}
