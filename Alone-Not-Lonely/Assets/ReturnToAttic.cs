using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToAttic : MonoBehaviour
{
    public Vector3 positionToReturnTo;
    private Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();
        positionToReturnTo = new Vector3(27.4599991f,4.95430565f,-88.1100006f);
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
            _player.gameObject.transform.position =  positionToReturnTo;
            _player.gameObject.SetActive(true);
            SceneManager.LoadScene("Attic2");
        }
    }
}
