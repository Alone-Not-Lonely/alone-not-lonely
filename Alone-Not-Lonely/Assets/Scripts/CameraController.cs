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
    float rotationY = 0f;

    //controller input values:
    float inX = 0f;
    float iny = 0f;

    private Player player;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        //Moved mouse logic to Pause Menu Controller
    }

    private void Awake()
    {
        player = (Player)FindObjectOfType(typeof(Player));

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
        if(!player.paused)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;
            // update current values
            rotationY += inX * sensitivityX;
            rotationX += iny * sensitivityY;

            // constrain x
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

            // rotate game objects accordingly
            transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z);
        }
        //else
        //{
            //Cursor.lockState = CursorLockMode.None;
            //Cursor.visible = true;
        //}
    }

    public Vector3 GetCameraRotation()
    {
        return new Vector3(-rotationX, rotationY, 0);
    }
}
