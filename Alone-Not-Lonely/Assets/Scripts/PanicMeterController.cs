using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanicMeterController : MonoBehaviour
{
    public Image anxietyMeter;
    public float totalAnxietyPoints = 50f;
    private float currentAnxietyPoints;

    public float anxietySpeed = 10f;

    bool monsterInRadius;
    
    void Start()
    {
        currentAnxietyPoints = 0;
        anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        monsterInRadius = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(monsterInRadius)
        {
            currentAnxietyPoints += Time.deltaTime * anxietySpeed;
            anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        }
        else if (currentAnxietyPoints > 0)
        {
            currentAnxietyPoints -= Time.deltaTime * anxietySpeed;
            anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            monsterInRadius = true;
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            monsterInRadius = true;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            monsterInRadius = false;
        }
    }
}
