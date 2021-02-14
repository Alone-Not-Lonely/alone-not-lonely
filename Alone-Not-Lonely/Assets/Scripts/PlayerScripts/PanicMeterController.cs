using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PanicMeterController : MonoBehaviour
{
    public Image anxietyMeter;
    public float totalAnxietyPoints = 50f, rayDepth = 1f,rayWidth = 0.5f;
    private float currentAnxietyPoints;
    private Player thisPlayer;
    public float anxietySpeed = 10f;
    //private Animator playerAnimator;

    bool monsterInRadius;

    public Volume postProcess;
    private Vignette vignette;

    void Start()
    {
        currentAnxietyPoints = 0;
        anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        monsterInRadius = false;
        thisPlayer = (Player)FindObjectOfType<Player>();
        postProcess.profile.TryGet(out vignette);
        if(vignette)
        {
            vignette.intensity.value = 0f;
        }
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
        vignette.intensity.value = anxietyMeter.fillAmount;
        if (currentAnxietyPoints > totalAnxietyPoints)
        {
            StartCoroutine("faint");
        }
    }

    private void FixedUpdate()
    {
        checkFloor();
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

    //fires a beam just long enough to hit the floor just below the player
    //returns a tag if found
    private void checkFloor()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDepth, Color.yellow);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down)*rayDepth, out hit))
        {
            //can but other floor based traits here
            if (hit.collider.tag == "Deadly") { StartCoroutine("faint"); };
            //There may be a problem w/ calling faint twice, but we'll see
        }
    }
}
