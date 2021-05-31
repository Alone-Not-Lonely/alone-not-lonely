using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUpEndAnimations : MonoBehaviour
{
    public List<GameObject> gameObjectsToActivate;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in gameObjectsToActivate)
        {
                g.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.CompareTag("Player"))
        {
            foreach(GameObject g in gameObjectsToActivate)
            {
                g.SetActive(true);
            }
        }
    }
}
