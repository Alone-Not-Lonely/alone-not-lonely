using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Audio;
public class PanicMeterController : MonoBehaviour
{
    public Image anxietyMeter;
    public float totalAnxietyPoints = 50f, rayDepth = .75f;
    private float currentAnxietyPoints, monstDist;
    private Player thisPlayer;
    private PlayerAbilityController pAbility;
    public float anxietySpeed = 10f;
    private float anxConst;
    private List<GameObject> monsters;

    public Volume postProcess;
    private Vignette vignette;
    private ColorAdjustments desaturate;
    public AudioSource breathing;
    private SphereCollider anxietyRadius;
    public RectTransform anxietyMeterHolder;
    private Vector3 origPosMeter;
    public float shakeMagnitude = 3f;

    void Start()
    {
        origPosMeter = anxietyMeterHolder.position;
        monsters = new List<GameObject>();
        anxConst = anxietySpeed;
        currentAnxietyPoints = 0;
        anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        thisPlayer = (Player)FindObjectOfType<Player>();
        pAbility = thisPlayer.gameObject.GetComponent<PlayerAbilityController>();
        postProcess = (Volume)FindObjectOfType<Volume>();
        postProcess.profile.TryGet(out vignette);
        postProcess.profile.TryGet(out desaturate);
        if(vignette)
        {
            vignette.intensity.value = 0f;
        }

        if(desaturate)
        {
            desaturate.saturation.value = 0f;
            desaturate.postExposure.value = 0f;
        }
        breathing = GetComponent<AudioSource>();
        breathing.enabled = false;
        //playerAnimator = GetComponent<Animator>();
        anxietyRadius = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(monsters.Count != 0)
        {
            float monstPoints = 0;
            //Debug.Log(monsters.Count);
            //add points per monster
            for(int i = 0; i< monsters.Count; i++)
            {
                //anxiety points based on monster distance
                monstDist = Vector3.Distance(monsters[i].transform.position, thisPlayer.transform.position);
                if(monstDist > ((anxietyRadius.radius * this.transform.lossyScale.y) + 2f)) // this is just to catch teleporting monsters who are significantly outside of bounds
                {
                    monsters.RemoveAt(i);
                    Debug.Log("Monster Discounted Manually : " + monstDist + " greater than " + anxietyRadius.radius * this.transform.parent.lossyScale.y);
                }
                else
                {
                    monstPoints +=  monstDist;
                }
            }
            anxietySpeed = anxConst;
            currentAnxietyPoints += anxietySpeed * Time.deltaTime;
        }
        else{
            anxietySpeed = anxConst;
        }
        if (anxietyMeter.fillAmount > 0)
        {
            StartCoroutine("Shake");
            if(monsters.Count == 0)
            {
                currentAnxietyPoints -= Time.deltaTime * anxietySpeed;
            }
            vignette.intensity.value = Mathf.Pow(anxietyMeter.fillAmount, 4f);
        }
        else{
            StopCoroutine("Shake");
            anxietyMeterHolder.position = origPosMeter;
        }
        if(anxietyMeter.fillAmount > .5f)
        {
            desaturate.saturation.value = (anxietyMeter.fillAmount - .5f) * -50f;
            desaturate.postExposure.value = (anxietyMeter.fillAmount - .5f) * -5f;
        }
        if(anxietyMeter.fillAmount <= 0)
        {
            if(breathing.enabled)
            {
                breathing.Stop();
            }
            breathing.enabled = false;
        }
        if (currentAnxietyPoints > totalAnxietyPoints)
        {
            monsters.Clear();
            Grabber playerAbility = thisPlayer.GetComponent<Grabber>(); //drop object when fainting
            if(playerAbility.holdingObject)
            {
                playerAbility.ReleaseObject();
            }
            StartCoroutine("faint");
        }
        anxietyMeter.fillAmount = (currentAnxietyPoints/totalAnxietyPoints);
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
        pAbility.ReleaseObject();
        /*anxietyMeter.fillAmount = 0;
        currentAnxietyPoints = 0;
        desaturate.saturation.value = 0f;
        desaturate.postExposure.value = 0f;*/
        thisPlayer.backToSpawn();
        StartCoroutine("wakeUp");
    }

    private IEnumerator wakeUp()
    {
        while(anxietyMeter.fillAmount > 0f)
        {
            currentAnxietyPoints -= Time.deltaTime * anxietySpeed * 10f;
            if(anxietyMeter.fillAmount > .5f)
            {
                desaturate.saturation.value = (anxietyMeter.fillAmount - .5f) * -50f;
                desaturate.postExposure.value = (anxietyMeter.fillAmount - .5f) * -5f;
            }
            vignette.intensity.value = Mathf.Pow(anxietyMeter.fillAmount, 2f);
            yield return new WaitForSeconds(.001f);
        }
        yield break;
    }

    private IEnumerator Shake()
    {
        while(anxietyMeter.fillAmount > 0f)
        {
            anxietyMeterHolder.position = new Vector3(origPosMeter.x + (Mathf.Sin((Time.time % 10) * 50f) * anxietyMeter.fillAmount * shakeMagnitude), 
                                                    origPosMeter.y  + (Mathf.Cos((Time.time % 8)* 50f) * anxietyMeter.fillAmount * shakeMagnitude),
                                                    origPosMeter.z);
            yield return new WaitForSeconds(.001f);
        }
        anxietyMeterHolder.position = origPosMeter;
        yield break;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            //monsterInRadius = true;
            bool found = false;
            foreach(GameObject monster in monsters)
            {
                if(other.gameObject == monster)
                {
                    found = true;
                }
            }
            if(!found)
            {
                monsters.Add(other.gameObject);
                Debug.Log("Monster accounted");
                Debug.Log(monsters.Count);
                if(!breathing.enabled)
                {
                    breathing.enabled = true;
                    breathing.Play();
                }
            }

        }
    }


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Monster"))
        {
            bool found = false;
            foreach(GameObject monster in monsters)
            {
                if(other.gameObject == monster)
                {
                    found = true;
                }
            }
            if(!found)
            {
                monsters.Add(other.gameObject);
            }
        }
    }
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
        Debug.Log("Checking Floor");
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDepth, Color.yellow);

        //could adjust starting position to align with player perception
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDepth))
        {
            
            //can but other floor based traits here
            if (hit.collider.tag == "Deadly") {
                //100 as an arbitrarily high number
                currentAnxietyPoints += (100 * Time.deltaTime);
                Debug.Log(currentAnxietyPoints);
            };
            //There may be a problem w/ calling faint twice, but we'll see
        }
    }
}
