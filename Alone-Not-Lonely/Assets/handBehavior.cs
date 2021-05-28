using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class handBehavior : MonoBehaviour
{
    private enum handed {right = 1, left = -1};
    [SerializeField]
    private handed handedness;
    private int hQ;
    // Start is called before the first frame update
    void Start()
    {
       hQ = (int)handedness;//makes hand direction simpler
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(Vector3.up * hQ, Space.Self);
    }
}
