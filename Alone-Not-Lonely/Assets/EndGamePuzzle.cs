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
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Executing Start");
        base.Start();
        _player = Player.instance;
        _player._actionMap.Platforming.InteractionTest.performed += interact => PlayerInteract();
        puzzleManager = FinalPuzzleManager.instance;
        puzzleBoard = PuzzleBoard.instance;
        puzzleManager.gameObject.SetActive(false);
        puzzleBoard.gameObject.SetActive(false);
    }

    void PlayerInteract()
    {
        if(!_player.paused)
        {
            if(!inPuzzleMode && canInteract)
            {
                _player._actionMap.Platforming.Disable();
                _player._actionMap.PuzzleAssembly.Enable();
                _player._actionMap.PuzzleAssembly.Exit.performed += close => PlayerInteract();
                Camera.main.GetComponent<CameraController>().MoveToFixedPosition(cameraTarget.position, this.gameObject);
                inPuzzleMode = true;
                
                //base.ChangeToContextSecondary();
                Cursor.lockState = CursorLockMode.None;
                puzzleManager.gameObject.SetActive(true);
                puzzleBoard.gameObject.SetActive(true);
                if(puzzleManager)
                    puzzleManager.UpdatePuzzleState();
            }
            else if (canInteract)
            {
                _player._actionMap.Platforming.Enable();
                _player._actionMap.PuzzleAssembly.Disable();
                _player._actionMap.Platforming.InteractionTest.performed += interact => PlayerInteract();
                Camera.main.GetComponent<CameraController>().cameraFree = true;
                inPuzzleMode = false;
                //base.ChangeToContextInit();
                Cursor.lockState = CursorLockMode.Locked;
                puzzleManager.gameObject.SetActive(false);
                puzzleBoard.gameObject.SetActive(false);
            }
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
        }
    }

    void OnTriggerExit(Collider other) 
    {
         if(other.CompareTag("Player"))
        {
            base.OnTriggerExit(other);
            canInteract = false;
        }
    }
}
