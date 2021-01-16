using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Will contain all player status effects
    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private Vector3 camSpawnPosition;
    private Quaternion camSpawnRotation;
    private PauseMenuController pmController;
    private void Awake()
    {
        
    }
    public bool paused;

    void Start()
    {
        pmController = (PauseMenuController)FindObjectOfType(typeof(PauseMenuController));
        paused = pmController.isPaused();
        spawnPosition = transform.position;
        spawnRotation = transform.rotation;

        camSpawnPosition = Camera.main.gameObject.transform.position;
        camSpawnRotation = Camera.main.gameObject.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        paused = pmController.isPaused();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.CompareTag("Deadly"))
        {
            this.gameObject.SetActive(false);
            transform.position = spawnPosition;
            transform.rotation = spawnRotation;
            Camera.main.gameObject.transform.position = camSpawnPosition;
            Camera.main.gameObject.transform.rotation = camSpawnRotation;
            this.gameObject.SetActive(true);
        }
    }
}
