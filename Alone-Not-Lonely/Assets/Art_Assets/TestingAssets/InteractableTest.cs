using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableTest : Interactable
{
    // Start is called before the first frame update
    void Start()
    {
        base.playerRef = (Player)FindObjectOfType(typeof(Player));
        base.playerRef._actionMap.Platforming.InteractionTest.performed += interact => PlayerInteract();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
