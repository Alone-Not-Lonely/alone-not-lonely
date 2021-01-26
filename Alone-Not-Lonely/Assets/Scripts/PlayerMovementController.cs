using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpHeight;
    public float gravity = 2f;
    //Character will move along this vector 
    private Vector3 moveDirection = Vector3.zero;
    private bool jumpInput = false;

    protected CharacterController playerController;
    private DefaultControls _actionMap;
    private float moveDirY = 0;
    private Quaternion rotation = Quaternion.identity;
    private CameraController camController;
    private Player thisPlayer;

    private void Awake()
    {
        _actionMap = new DefaultControls();
        _actionMap.Enable();
        playerController = this.GetComponent<CharacterController>();
        camController = (CameraController)FindObjectOfType(typeof(CameraController));
        thisPlayer = (Player)FindObjectOfType(typeof(Player));
    //}

    //private void OnEnable()
    //{

        _actionMap.Platforming.Move.performed += move => 
            { moveDirection = new Vector3(move.ReadValue<Vector2>().x, moveDirY, move.ReadValue<Vector2>().y);
              print("moving: " + moveDirection);
            };
        _actionMap.Platforming.Move.canceled += move => moveDirection = new Vector3 (0, moveDirY, 0);

        _actionMap.Platforming.Jump.performed += jump => jumpInput = true;
        _actionMap.Platforming.Jump.canceled += jump => jumpInput = false;

        //_actionMap.Platforming.Camera.performed += MoveCamera;
    }

    /*
    private void OnDisable()
    {
        _actionMap.Platforming.Move.performed -= move;
        _actionMap.Platforming.Jump.performed -= Jump;
        _actionMap.Platforming.Camera.performed -= MoveCamera;
        _actionMap.Disable();
    }

    
    private void Move(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        //playerController.Move(new Vector3(value.x, 0f, value.y) * walkSpeed * Time.deltaTime);
        Debug.Log("walk");
    }

    private void Jump()//InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
    }

    private void MoveCamera(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
    }
    */

    void Update()
    {
        if(!thisPlayer.paused)
        {
            //float horizDirection = Input.GetAxis("Horizontal");
            //float vertDirection = Input.GetAxis("Vertical");
            if(playerController.isGrounded && jumpInput)//Input.GetKeyDown(KeyCode.Space))
            {
                moveDirY = jumpHeight;
            }

            if (!playerController.isGrounded)
            {
                moveDirY -= gravity * Time.deltaTime;
            }
            //Vector3 moveDirection = new Vector3(horizDirection, moveDirY, vertDirection);
            //moveDirection = transform.TransformDirection(moveDirection);
            playerController.Move(moveDirection * walkSpeed * Time.deltaTime);
            //playerController.Move(moveVector * walkSpeed * Time.deltaTime);
            //print("Player should be moving: " + (moveVector * walkSpeed * Time.deltaTime));
            //Vector3 camRotation = camController.GetCameraRotation(); WILL USE, JUST DEACTIVATING
            //playerController.gameObject.transform.eulerAngles = (new Vector3(0, camRotation.y, 0)); //TEMPORARILY DEACTIVATED
        }
    }

}
