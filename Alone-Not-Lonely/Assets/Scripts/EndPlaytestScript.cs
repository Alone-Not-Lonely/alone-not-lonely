using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPlaytestScript : MonoBehaviour
{
    public Vector3 positionToReturnTo;
    private Player _player;
    private PromptController pC;
    public LockedObject bedroomDoor; //Necessary to avoid error
    // Start is called before the first frame update
    void Start()
    {
        _player = Player.instance;
        positionToReturnTo = new Vector3(30,4,-47); 
        _player._actionMap.Platforming.SkipLevel.performed += skip => LoadNextLevel();
        pC = FindObjectOfType<PromptController>();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            LoadNextLevel();
        }
    }

    void LoadNextLevel()
    {
        //ProgressionTracker[] p = (ProgressionTracker[])FindObjectsOfType<ProgressionTracker>();
        ProgressionTracker.instance.MarkSceneCompleted("Kitchen");
        _player.gameObject.SetActive(false);
        //_player.gameObject.transform.position = positionToReturnTo;
        //_player.gameObject.SetActive(true);
        //SceneManager.LoadScene("GroundFloor");
        Transform newTransform = _player.transform;
        newTransform.position = positionToReturnTo;
        newTransform.rotation = Quaternion.identity; //CHANGE THIS LINE
        LoadingScreen.instance.LoadScene("GroundFloor", newTransform);
        if(pC!=null)
        {
            pC.clearPrompters();
        }
        bedroomDoor.removeFromActions();
    }

    
}
