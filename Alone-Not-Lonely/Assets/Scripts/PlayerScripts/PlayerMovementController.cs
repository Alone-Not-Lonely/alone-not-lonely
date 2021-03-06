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
    private PlayerAbilityController playerAb;
    public AudioSource footsteps;
    private bool wasMovingLastFrame = false;

    private void Awake()
    {
        playerController = this.GetComponent<CharacterController>();
        camController = (CameraController)FindObjectOfType(typeof(CameraController));
        playerAb = (PlayerAbilityController)FindObjectOfType(typeof(PlayerAbilityController));
        footsteps.Play();
        footsteps.Pause();
    }

    private void Start() 
    {
        thisPlayer = (Player)FindObjectOfType<Player>();

        thisPlayer._actionMap.Platforming.MoveVert.performed += cont => MoveVert(cont.ReadValue<float>());
        thisPlayer._actionMap.Platforming.MoveVert.canceled += cont => MoveVert(0f);

        thisPlayer._actionMap.Platforming.MoveHoriz.performed += cont => MoveHoriz(cont.ReadValue<float>());
        thisPlayer._actionMap.Platforming.MoveHoriz.canceled += cont => MoveHoriz(0f);

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
    bool stopFootstepsH;
    bool stopFootstepsV;
    public void MoveHoriz(float horizMvmt)
    {
        if(horizDirection == 0 && vertDirection == 0 && playerController.isGrounded)
        {
            footsteps.UnPause();
            Debug.Log("Play steps");
        }
        else if(horizMvmt == 0 && (horizDirection != 0 || vertDirection != 0))
        {
            //footsteps.Stop();
            stopFootstepsH = true;
        }
        horizDirection = horizMvmt;
    }

    public void MoveVert(float vertMvmt)
    {
        if(vertDirection == 0 && horizDirection == 0 && playerController.isGrounded)
        {
            footsteps.UnPause();
            Debug.Log("Play steps");
        }
        else if(vertMvmt == 0 && (horizDirection != 0 || vertDirection != 0))
        {
            //footsteps.Stop();
            stopFootstepsV = true;
        }
        vertDirection = vertMvmt;
    }

    private void Jump()
    {
        if (playerController.isGrounded && !playerAb.holdingObject)
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
        if(horizDirection == 0 && vertDirection == 0)
        {
            footsteps.Pause();
            Debug.Log("Pause steps");
        }
        /*{
            footsteps.Pause();
            stopFootstepsH = false;
            stopFootstepsV = false;
        }*/
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
        else{
            footsteps.Stop();
        }
    }
}
