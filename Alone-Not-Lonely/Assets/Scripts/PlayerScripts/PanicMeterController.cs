using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PanicMeterController : MonoBehaviour
{
    public Image anxietyMeter;
    public float totalAnxietyPoints = 50f, rayDepth = 1f, anxConst = 1;
    private float currentAnxietyPoints, monstDist;
    private Player thisPlayer;
    public float anxietySpeed = 10f;
    private List<GameObject> monsters;
    //private Animator playerAnimator;

    //bool monsterInRadius;

    public Volume postProcess;
    private Vignette vignette;
    private ColorAdjustments desaturate;
    void Start()
    {
        monsters = new List<GameObject>();

        currentAnxietyPoints = 0;
        anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        //monsterInRadius = false;
        thisPlayer = (Player)FindObjectOfType<Player>();
        postProcess.profile.TryGet(out vignette);
        postProcess.profile.TryGet(out desaturate);
        if(vignette)
        {
            vignette.intensity.value = 0f;
        }
        //else//TEST TEST TEST
        //{
        //    Debug.Log("vignette missing");
        //}

        if(desaturate)
        {
            desaturate.saturation.value = 0f;
            desaturate.postExposure.value = 0f;
        }

        
    

        //playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(monsters.Count != 0)
        {
            
            //add points per monster
            foreach(GameObject monster in monsters)
            {
                //anxiety points based on monster distance
                monstDist = Vector3.Distance(monster.transform.position, thisPlayer.transform.position);
                currentAnxietyPoints += (1/monstDist*anxConst);
                Debug.Log("monst contribution: " + (1 / monstDist * anxConst));
            }
            anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        }
        else if (currentAnxietyPoints > 0)
        {
            currentAnxietyPoints -= Time.deltaTime * anxietySpeed;
            anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        }
        if(anxietyMeter.fillAmount > .5f)
        {
            desaturate.saturation.value = (anxietyMeter.fillAmount - .5f) * -50f;
            desaturate.postExposure.value = (anxietyMeter.fillAmount - .5f) * -5f;
        }
        if (currentAnxietyPoints > totalAnxietyPoints)
        {
            foreach(GameObject monster in monsters)
            {
                monsters.Remove(monster);
            }
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
        desaturate.saturation.value = 0f;
        desaturate.postExposure.value = 0f;
        thisPlayer.backToSpawn();
    }
    

    private void OnTriggerEnter(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            //monsterInRadius = true;
            monsters.Add(other.gameObject);
            Debug.Log("Monster accounted");
            Debug.Log(monsters.Count);
        }
    }

    /*
    private void OnTriggerStay(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            monsterInRadius = true;
        }
    }
    */
    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Monster"))
        {
            monsters.Remove(other.gameObject);
            Debug.Log("Monster discounted");
            Debug.Log(monsters.Count);
        }
    }

    //fires a beam just long enough to hit the floor just below the player
    //returns a tag if found
    private void checkFloor()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDepth, Color.yellow);

        //could adjust starting position to align with player perception
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down)*rayDepth, out hit))
        {
            //can but other floor based traits here
            if (hit.collider.tag == "Deadly") { StartCoroutine("faint"); };
            //There may be a problem w/ calling faint twice, but we'll see
        }
    }
}
