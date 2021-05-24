using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReturnToKitchen : ContextualUI
{
    public Vector3 positionToReturnTo;
    private Player _player;

    private bool canGoToAttic;

    public bool bedroomDoor;

    void Start()
    {
        base.Start();
        _player = Player.instance;
        positionToReturnTo = new Vector3(19.5699558f ,1.98000038f ,-69.827858f );
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
            canGoToAttic = true;
            _player._actionMap.Platforming.Use.performed += interact => GoToKitchen();
        }
    }

    void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player"))
        {
            base.OnTriggerExit(other);
            canGoToAttic = false;
            _player._actionMap.Platforming.Use.performed -= interact => GoToKitchen();
        }
    }

    void GoToKitchen()
    {
        if(canGoToAttic)
        {
            _player.gameObject.SetActive(false);
            //_player.gameObject.transform.position =  new Vector3(32.7400017f,4.98999977f,-62.7200012f);
            //_player.gameObject.SetActive(true);
            //SceneManager.LoadScene("Kitchen");
            canGoToAttic = false;
            _player._actionMap.Platforming.Use.performed -= interact => GoToKitchen();
            Transform newTransform = _player.transform;
            if(bedroomDoor)
                newTransform.position = positionToReturnTo;
            else
                newTransform.position = positionToReturnTo + new Vector3(0, 0, 20f);
            newTransform.rotation = Quaternion.identity; //CHANGE THIS LINE
            LoadingScreen.instance.LoadScene("Kitchen", newTransform);
        }
    }
}
