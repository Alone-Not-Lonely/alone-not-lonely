using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwappingMonster : MonoBehaviour
{
    public bool startingMonster;
    public SwappingMonster partnerMonster;
    public static bool startingMonsterOn = true;
    public static bool evaluatedThisFrame = false; //trying something, idk

    public static float globalTimer = 0f;
    public static float timeToSwap = 5f; //seconds
    private void Awake() {
        if(startingMonster)
        {
            partnerMonster.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }
        else
        {
            partnerMonster.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!evaluatedThisFrame)
        {
            globalTimer += Time.deltaTime;
            if(globalTimer >= timeToSwap)
            {
                globalTimer = 0;
                startingMonsterOn = !startingMonsterOn;
            }
            evaluatedThisFrame = true;
        }
        if(startingMonster)
        {
            partnerMonster.gameObject.SetActive(!startingMonsterOn);
            this.gameObject.SetActive(startingMonsterOn);
        }
        else
        {
            partnerMonster.gameObject.SetActive(startingMonsterOn);
            this.gameObject.SetActive(!startingMonsterOn);
        }
    }

    void LateUpdate() {
        evaluatedThisFrame = false;
    }
}
