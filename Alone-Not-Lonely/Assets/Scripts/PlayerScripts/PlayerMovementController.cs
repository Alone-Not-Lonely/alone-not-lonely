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
    public bool climbing = false;
    public bool jumping;

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

    //Climb stuff
    private ClimbChecker cCheck;
    public float lerpEpsilon = 0.01f, climbSpeed = 10f, crawlHeight = 1f;

    public List<AudioClip> footstepSounds;

    private void Start() 
    {
        //thisPlayer = (Player)FindObjectOfType<Player>();
        thisPlayer = Player.instance;
        playerController = GetComponent<CharacterController>();

        thisPlayer._actionMap.Platforming.MoveVert.performed += cont => MoveVert(cont.ReadValue<float>());
        thisPlayer._actionMap.Platforming.MoveVert.canceled += cont => MoveVert(0f);

        thisPlayer._actionMap.Platforming.MoveHoriz.performed += cont => MoveHoriz(cont.ReadValue<float>());
        thisPlayer._actionMap.Platforming.MoveHoriz.canceled += cont => MoveHoriz(0f);

        thisPlayer._actionMap.Platforming.Jump.performed += jump => Jump();

        camController = (CameraController)FindObjectOfType(typeof(CameraController));
        playerAb = GetComponent<PlayerAbilityController>();
        footsteps.Play();
        footsteps.clip = footstepSounds[0];
        footsteps.Pause();
        cCheck = (ClimbChecker)FindObjectOfType(typeof(ClimbChecker));

        Vector3 camRotation = camController.GetCameraRotation(); 
        playerController.gameObject.transform.eulerAngles = (new Vector3(0, camRotation.y, 0)); 

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
        if (horizDirection == 0 && vertDirection == 0)
        {
            Debug.Log("Footsteps");
            footsteps.clip = footstepSounds[Mathf.FloorToInt(Random.Range(0, footstepSounds.Count))];
            footsteps.Play();
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
        if (vertDirection == 0 && horizDirection == 0)
        {
            Debug.Log("Footsteps");
            footsteps.clip = footstepSounds[Mathf.FloorToInt(Random.Range(0, footstepSounds.Count))];
            footsteps.Play();
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
        //allows climbing if no object in hand or the object is a squashed object
        
        if (playerAb.holdingObject && playerAb.heldObject.gameObject.GetComponent<SquashedObject>()==null)
        {
            Debug.Log("Squashed object component not detected");
            //we don't ever want to perform jump based action in these circumstances 
            return;
        }

        if (playerController.isGrounded)
        {
           if(cCheck.climbablePoint != Vector3.zero && !climbing)
           {
                climbTypeCheck();
           }
        }
    }

    void climbTypeCheck()
    {
        GameObject pointToClimb = new GameObject();
        pointToClimb.transform.position = cCheck.climbablePoint;
        pointToClimb.transform.parent = cCheck.climbableObject.transform;
        StartCoroutine("climb", pointToClimb);
    }


    IEnumerator climb(GameObject pointToClimb)
    {
        Debug.Log("Climbing");
        climbing = true;
        //getting normal stuff out of the way
        camController.headbob = false;
        playerController.detectCollisions = false;

        //setting climbing parameters
        Vector3 finalPosition = pointToClimb.transform.position;
        
        //A point just below the necessary height to represent climbing part of the way up.
        Vector3 climbHeight = new Vector3(transform.position.x, (finalPosition.y - crawlHeight), transform.position.z);
        //Debug.Log("climb distance: "+ Vector3.Distance(transform.position, finalPosition));
        
        //climb to slide height
        while (cCheck.climbableDistance(transform.position, finalPosition) && Vector3.Distance(transform.position, climbHeight) > lerpEpsilon)
        {
            transform.position = Vector3.Lerp(transform.position, climbHeight, climbSpeed * Time.deltaTime);
            yield return null;
        }

        //The length the charcter "slides" along the object
        Vector3 slidePoint = new Vector3(finalPosition.x, finalPosition.y - .5f, finalPosition.z);

        while (cCheck.climbableDistance(transform.position, finalPosition) && Vector3.Distance(transform.position, slidePoint) > lerpEpsilon)
        {
            
            transform.position = Vector3.Lerp(transform.position, slidePoint, climbSpeed * Time.deltaTime);
            yield return null;
        }

        //Standing Up
        while (cCheck.climbableDistance(transform.position, finalPosition) && Vector3.Distance(transform.position, finalPosition) > lerpEpsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, finalPosition, climbSpeed * Time.deltaTime);
            yield return null;
        }

        camController.headbob = true;
        playerController.detectCollisions = true;
        climbing = false;
        Destroy(pointToClimb);
        
    }

 
    void FixedUpdate()
    {
        if(horizDirection == 0 && vertDirection == 0 && footsteps.isPlaying)
        {
            footsteps.Pause();
            Debug.Log("Stop footsteps");
        }
        if(!thisPlayer.paused && !climbing)
        {

            if (!playerController.isGrounded)
            {
                moveDirY -= gravity * Time.deltaTime;
            }

            moveDirection = new Vector3(horizDirection, moveDirY, vertDirection);
            moveDirection = transform.TransformDirection(moveDirection);
            //Debug.Log("moveDirection: " + moveDirection);
            RaycastHit holdingObjectCheck;
            Vector3 camRotation;
            if(playerAb.holdingObject && 
            Physics.Raycast(transform.position, transform.forward, out holdingObjectCheck, playerAb.heldObject.GetComponent<BoxContactBehavior>().holdOffset + playerAb.holdDistance + 1f, ~(1<<13)))
            {
                if(!holdingObjectCheck.collider.isTrigger && !holdingObjectCheck.collider.gameObject.CompareTag("Grabable") && Vector3.Dot(moveDirection, transform.forward) > .5 && playerAb.heldObject.GetComponent<SquashedObject>() == null)
                {
                    camRotation = camController.GetCameraRotation(); 
                    //playerController.gameObject.transform.eulerAngles = (new Vector3(0, camRotation.y, 0));
                }
                else
                {
                    playerController.Move(moveDirection * walkSpeed * Time.deltaTime);
                    camRotation = camController.GetCameraRotation(); 
                    //playerController.gameObject.transform.eulerAngles = (new Vector3(0, camRotation.y, 0));
                }
            }
            else{
                playerController.Move(moveDirection * walkSpeed * Time.deltaTime);
                camRotation = camController.GetCameraRotation(); 
                //playerController.gameObject.transform.eulerAngles = (new Vector3(0, camRotation.y, 0)); 
            }
            //begin foolin
            RaycastHit rotationCheck;
            float deltaRotation = camRotation.y - playerController.gameObject.transform.eulerAngles.y;
            if(deltaRotation > 0)
            {
                deltaRotation = 1;
            }
            else if(deltaRotation < 0)
            {
                deltaRotation = -1;
            }
            else
            {
                deltaRotation = 0;
            }
            if(playerAb.holdingObject)
                Debug.DrawRay(transform.position + (transform.forward * (playerAb.heldObject.GetComponent<BoxContactBehavior>().holdOffset + playerAb.holdDistance)), transform.right * deltaRotation);
            if(playerAb.holdingObject && 
            !Physics.Raycast(transform.position + (transform.forward * (playerAb.heldObject.GetComponent<BoxContactBehavior>().holdOffset + playerAb.holdDistance)), transform.right * deltaRotation, out rotationCheck, 1.5f, ~(1<<13)))
            {
                playerController.gameObject.transform.eulerAngles = (new Vector3(0, camRotation.y, 0));
            }
            else if(!playerAb.holdingObject)
            {
                playerController.gameObject.transform.eulerAngles = (new Vector3(0, camRotation.y, 0));
            }
            //end foolin
        }
        else{
            footsteps.Pause();
        }
    }
}
