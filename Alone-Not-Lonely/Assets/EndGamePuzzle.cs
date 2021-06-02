using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGamePuzzle : ContextualUI
{
    Player _player;
    bool canInteract = false;

    bool inPuzzleMode = false;

    public Transform cameraTarget;

    FinalPuzzleManager puzzleManager;
    PuzzleBoard puzzleBoard;

    FinalPuzzleManager worldPuzzleManager;

    public float interactionCooldown = 1f;
    private float currentInteractionCooldown = 0f;
    bool inInteractionCooldown = false;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Executing Start");
        base.Start();
        _player = Player.instance;
        puzzleManager = FinalPuzzleManager.instance;
        puzzleBoard = PuzzleBoard.instance;
        puzzleManager.gameObject.SetActive(false);
        puzzleBoard.gameObject.SetActive(false);
    }

    void PlayerInteract()
    {
        if(!_player.paused && !inInteractionCooldown)
        {
            if(!inPuzzleMode && canInteract)
            {
                _player._actionMap.Platforming.InteractionTest.performed -= interact => PlayerInteract();
                _player._actionMap.Platforming.Disable();
                _player._actionMap.PuzzleAssembly.Enable();
                _player._actionMap.PuzzleAssembly.Exit.performed += close => PlayerInteract();
                Camera.main.GetComponent<CameraController>().MoveToFixedPosition(cameraTarget.position, this.gameObject);
                inPuzzleMode = true;
                
                //base.ChangeToContextSecondary();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                puzzleManager.gameObject.SetActive(true);
                puzzleBoard.gameObject.SetActive(true);
                inInteractionCooldown = true;
                if(puzzleManager)
                    FinalPuzzleManager.instance.UpdatePuzzleState();
            }
            else if (canInteract)
            {
                _player._actionMap.PuzzleAssembly.Exit.performed -= close => PlayerInteract();
                _player._actionMap.Platforming.Enable();
                _player._actionMap.PuzzleAssembly.Disable();
                _player._actionMap.Platforming.InteractionTest.performed += interact => PlayerInteract();
                Camera.main.GetComponent<CameraController>().cameraFree = true;
                inPuzzleMode = false;
                //base.ChangeToContextInit();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                puzzleManager.gameObject.SetActive(false);
                puzzleBoard.gameObject.SetActive(false);
                inInteractionCooldown = true;
            }
        }
    }

    void Update() 
    {
        if(inInteractionCooldown && currentInteractionCooldown < interactionCooldown)
        {
            currentInteractionCooldown += Time.deltaTime;
        }
        else if(inInteractionCooldown && currentInteractionCooldown >= interactionCooldown)
        {
            currentInteractionCooldown = 0;
            inInteractionCooldown = false;
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            //base.OnTriggerEnter(other);
            canInteract = true;
            //puzzleManager.UpdatePuzzleState();
            _player._actionMap.Platforming.InteractionTest.performed += interact => PlayerInteract();
        }
    }

    void OnTriggerExit(Collider other) 
    {
         if(other.CompareTag("Player"))
        {
            base.OnTriggerExit(other);
            _player._actionMap.Platforming.InteractionTest.performed -= interact => PlayerInteract();
            canInteract = false;
        }
    }
}
