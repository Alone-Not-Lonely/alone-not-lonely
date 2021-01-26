using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpHeight;
    public float gravity = 2f;
    //Character will move along this vector 
    private float moveDirY = 0, horizDirection = 0, vertDirection = 0;
    private Vector3 moveDirection = Vector3.zero;

    protected CharacterController playerController;

    private Quaternion rotation = Quaternion.identity;
    private CameraController camController;
    private Player thisPlayer;

    private void Awake()
    {
        playerController = this.GetComponent<CharacterController>();
        camController = (CameraController)FindObjectOfType(typeof(CameraController));
        thisPlayer = (Player)FindObjectOfType(typeof(Player));


        thisPlayer._actionMap.Platforming.Move.performed += move => 
        { 
            horizDirection = move.ReadValue<Vector2>().x;
            vertDirection = move.ReadValue<Vector2>().y;
        };

        thisPlayer._actionMap.Platforming.Move.canceled += move => {
            horizDirection = 0;
            vertDirection = 0;
        };

        thisPlayer._actionMap.Platforming.Jump.performed += jump =>
        {
            if (playerController.isGrounded)
            {
                moveDirY = jumpHeight;
            }
        };
    }

    /*
    private void OnDisable()
    {
        _actionMap.Platforming.Move.performed -= move;
        _actionMap.Platforming.Jump.performed -= Jump;
        _actionMap.Platforming.Camera.performed -= MoveCamera;
        _actionMap.Disable();
    }
   
    
    public void move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            horizDirection = context.ReadValue<Vector2>().x;
            vertDirection = context.ReadValue<Vector2>().y;
        }
        else
        {
            horizDirection = 0;
            vertDirection = 0;
        }
        //Vector2 value = context.ReadValue<Vector2>();
        //playerController.Move(new Vector3(value.x, 0f, value.y) * walkSpeed * Time.deltaTime);
        Debug.Log("walk");
    }

    //private void Jump()//InputAction.CallbackContext context)
    //{
        //float value = context.ReadValue<float>();
   // }

    private void MoveCamera(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
    }
     */
    void FixedUpdate()
    {
        if(!thisPlayer.paused)
        {
            //float horizDirection = Input.GetAxis("Horizontal");
            //float vertDirection = Input.GetAxis("Vertical");
            //if(playerController.isGrounded && Input.GetKeyDown(KeyCode.Space))
            //{
            //    moveDirY = jumpHeight;
            //}

            if (!playerController.isGrounded)
            {
                moveDirY -= gravity * Time.deltaTime;
            }

            moveDirection = new Vector3(horizDirection, moveDirY, vertDirection);
            moveDirection = transform.TransformDirection(moveDirection);
            playerController.Move(moveDirection * walkSpeed * Time.deltaTime);
            //print("moving: " + moveDirection);
            Vector3 camRotation = camController.GetCameraRotation(); 
            playerController.gameObject.transform.eulerAngles = (new Vector3(0, camRotation.y, 0)); 
        }
    }
}
