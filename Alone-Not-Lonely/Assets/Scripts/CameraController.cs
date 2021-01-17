using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class CameraController : MonoBehaviour
{
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

    private Player player;

    private Vector3 velocity = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        player = (Player)FindObjectOfType(typeof(Player));
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;

        /*if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if(!player.paused)
        {
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            // update current values
            rotationY += Input.GetAxis("Mouse X") * sensitivityX;
            rotationX += Input.GetAxis("Mouse Y") * sensitivityY;

            // constrain x
            rotationX = Mathf.Clamp(rotationX, minimumX, maximumX);

            // rotate game objects accordingly
            transform.localEulerAngles = new Vector3(-rotationX, rotationY, 0);
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 2f, player.transform.position.z);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public Vector3 GetCameraRotation()
    {
        return new Vector3(-rotationX, rotationY, 0);
    }
}
