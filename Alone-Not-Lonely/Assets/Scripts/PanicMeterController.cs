using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanicMeterController : MonoBehaviour
{
    public Image anxietyMeter;
    public float totalAnxietyPoints = 50f;
    private float currentAnxietyPoints;
    private Player thisPlayer;
    public float anxietySpeed = 10f;
    //private Animator playerAnimator;

    bool monsterInRadius;
    
    void Start()
    {
        currentAnxietyPoints = 0;
        anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        monsterInRadius = false;
        thisPlayer = (Player)FindObjectOfType<Player>();
        //playerAnimator = GetComponent<Animator>();
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

        if (currentAnxietyPoints > totalAnxietyPoints)
        {

            StartCoroutine("faint");
        }
    }

    
    private IEnumerator faint()
    {
        //playerAnimator.SetBool("up", false);
        yield return new WaitForSeconds(.001f);//should be length of animation
        //playerAnimator.SetBool("up", true);
        anxietyMeter.fillAmount = 0;
        currentAnxietyPoints = 0;
        thisPlayer.backToSpawn();
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
