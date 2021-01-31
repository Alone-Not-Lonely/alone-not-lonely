﻿using System.Collections;
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

        thisPlayer._actionMap.Platforming.Move.performed += cont => Move(cont.ReadValue<Vector2>());
        thisPlayer._actionMap.Platforming.Move.canceled += cont => Move(Vector2.zero);

        thisPlayer._actionMap.Platforming.Jump.performed += jump => Jump();
    }

    /*
    private void OnDisable()
    {
        _actionMap.Platforming.Move.performed -= move;
        _actionMap.Platforming.Jump.performed -= Jump;
        _actionMap.Platforming.Camera.performed -= MoveCamera;
        _actionMap.Disable();
    }
   */
    
    public void Move(Vector2 movement)
    {
        Debug.Log("move: " + movement);
        horizDirection = movement.x;
        vertDirection = movement.y;
    }

    private void Jump()
    {
        if (playerController.isGrounded)
        {
            moveDirY = jumpHeight;
        }
    }

    //private void MoveCamera(InputAction.CallbackContext context)
    //{
    //    Vector2 value = context.ReadValue<Vector2>();
    //}
 
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
            //Debug.Log("moveDirection: " + moveDirection);
            playerController.Move(moveDirection * walkSpeed * Time.deltaTime);
            Vector3 camRotation = camController.GetCameraRotation(); 
            playerController.gameObject.transform.eulerAngles = (new Vector3(0, camRotation.y, 0)); 
        }
    }
}
