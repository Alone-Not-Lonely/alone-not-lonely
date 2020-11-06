using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpHeight;
    public float gravity = 2f;
    bool moving = false;
    
    protected CharacterController playerController;
    //private DefaultControls _actionMap;
    private float moveDirY = 0;

    private void Awake()
    {
        //_actionMap = new DefaultControls();
        //_actionMap.Enable();
        playerController = this.GetComponent<CharacterController>();
    }

    /*private void OnEnable()
    {
        _actionMap.Platforming.Move.performed += Move;
        _actionMap.Platforming.Jump.performed += Jump;
        _actionMap.Platforming.Camera.performed += MoveCamera;
    }

    private void OnDisable()
    {
        _actionMap.Platforming.Move.performed -= Move;
        _actionMap.Platforming.Jump.performed -= Jump;
        _actionMap.Platforming.Camera.performed -= MoveCamera;
        _actionMap.Disable();
    }

    private void Move(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
        playerController.Move(new Vector3(value.x, 0f, value.y) * walkSpeed * Time.deltaTime);
        Debug.Log("walk");
    }

    private void Jump(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();
    }

    private void MoveCamera(InputAction.CallbackContext context)
    {
        Vector2 value = context.ReadValue<Vector2>();
    }*/

    void Update()
    {
        float horizDirection = Input.GetAxis("Horizontal");
        float vertDirection = Input.GetAxis("Vertical");
        if(playerController.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            moveDirY = jumpHeight;
        }
        if(!playerController.isGrounded)
        {
            moveDirY -= gravity * Time.deltaTime;
        }
        playerController.Move(new Vector3(horizDirection, moveDirY, vertDirection) * walkSpeed * Time.deltaTime);
    }
}
