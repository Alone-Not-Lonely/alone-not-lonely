using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public static Player instance;
    //Will contain all player status effects
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private Vector3 camSpawnPosition;
    private Quaternion camSpawnRotation;
    private PauseMenuController pmController;
    public DefaultControls _actionMap;
    public bool paused;

    private void Awake()
    {
        Player[] objs = (Player[])FindObjectsOfType<Player>();

        if (objs.Length <= 1)
        {
            instance = this;
            if(_actionMap == null)
            {
                _actionMap = new DefaultControls();
                _actionMap.Enable();
            }
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    public void InstantiateControls()
    {
        if(_actionMap == null)
        {
            _actionMap = new DefaultControls();
            _actionMap.Enable();
        }
    }

    void Start()
    {
        pmController = (PauseMenuController)FindObjectOfType(typeof(PauseMenuController));
        paused = pmController.isPaused();
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        camSpawnPosition = Camera.main.gameObject.transform.position;
        camSpawnRotation = Camera.main.gameObject.transform.rotation;
    }

    //Resets player to beginning state
    public void backToSpawn()
    {
        //Debug.Log("Return to start");
        transform.position = spawnPosition;
        transform.rotation = spawnRotation;
    }

    // Update is called once per frame
    void Update()
    {
        paused = pmController.isPaused();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("SetSpawn"))
        {
            spawnPosition = transform.position;
            spawnRotation = transform.rotation;

            camSpawnPosition = Camera.main.gameObject.transform.position;
            camSpawnRotation = Camera.main.gameObject.transform.rotation;
        }
    }

    private void OnDisable() {
        
    }
    public void OnDestroy() {
        if(_actionMap != null)
        {
            _actionMap.Disable();
        }
    }

    public void SoftResetLevel()
    {   
        backToSpawn();
        pmController.Unpause();
        LoadingScreen.instance.LoadScene(SceneManager.GetActiveScene().name, this.transform);
    }

}
