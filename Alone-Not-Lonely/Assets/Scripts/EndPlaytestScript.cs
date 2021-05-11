﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndPlaytestScript : MonoBehaviour
{
    public Vector3 positionToReturnTo;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<Player>();
        positionToReturnTo = new Vector3(30,4,-49);
        _player._actionMap.Platforming.SkipLevel.performed += skip => LoadNextLevel();
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _player.gameObject.SetActive(false);
            _player.gameObject.transform.position = positionToReturnTo;
            _player.gameObject.SetActive(true);
            SceneManager.LoadScene("Bedroom1");
        }
    }

    void LoadNextLevel()
    {
        ProgressionTracker[] p = (ProgressionTracker[])FindObjectsOfType<ProgressionTracker>();
        p[0].MarkSceneCompleted("Kitchen");
        _player.gameObject.SetActive(false);
        _player.gameObject.transform.position = positionToReturnTo;
        _player.gameObject.SetActive(true);
        SceneManager.LoadScene("Bedroom1");
    }

    
}
