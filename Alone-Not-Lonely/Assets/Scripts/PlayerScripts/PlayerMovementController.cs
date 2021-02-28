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
    private PlayerAbilityController playerAb;
    private ClimbChecker cCheck;

    private void Awake()
    {
        playerController = this.GetComponent<CharacterController>();
        camController = (CameraController)FindObjectOfType(typeof(CameraController));
        playerAb = (PlayerAbilityController)FindObjectOfType(typeof(PlayerAbilityController));
        cCheck = (ClimbChecker)FindObjectOfType(typeof(ClimbChecker));
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
    
    public void MoveHoriz(float horizMvmt)
    {
        horizDirection = horizMvmt;
    }

    public void MoveVert(float vertMvmt)
    {
        vertDirection = vertMvmt;
    }

    private void Jump()
    {
        if (playerAb.holdingObject || !playerController.isGrounded)
        {
            //we don't ever want to perform jump based acation in these circumstances 
            return;
        }

        if (playerController.isGrounded)
        {
            //float checkedHeight = cCheck.checkHeight();
           // Debug.Log(checkedHeight);
            //if (checkedHeight > 0)
            //{
            //    Debug.Log("would be climbing");
           // }
           // else {
                moveDirY = jumpHeight;
            //}
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
