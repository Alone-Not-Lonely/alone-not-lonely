﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
public class PanicMeterController : MonoBehaviour
{
    private enum passOutType { faint, sink}
    private passOutType howIGo = passOutType.faint;

    public Image anxietyMeter;
    public float totalAnxietyPoints = 50f, rayDepth = 1f, fallSpeed = 1;
    private float currentAnxietyPoints, monstDist;
    public bool dead = false;
    private Player thisPlayer;
    private PlayerMovementController movement;
    private PlayerAbilityController pAbility;
    public float anxietySpeed = 10f;
    private float anxConst;
    private List<GameObject> monsters;

    private CameraController cam;
    public Volume postProcess;
    private Vignette vignette;
    private ColorAdjustments desaturate;
    public AudioSource breathing;
    private SphereCollider anxietyRadius;
    public RectTransform anxietyMeterHolder;
    private Vector3 origPosMeter;
    public float shakeMagnitude = 3f;

    public Transform wholeBody;

    bool wasHeavyBreathing;

    void Start()
    {
        movement = FindObjectOfType<PlayerMovementController>();
        origPosMeter = anxietyMeterHolder.position;
        monsters = new List<GameObject>();
        anxConst = anxietySpeed;
        currentAnxietyPoints = 0;
        anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        thisPlayer = Player.instance;
        pAbility = thisPlayer.gameObject.GetComponent<PlayerAbilityController>();
        breathing = GetComponent<AudioSource>();
        //breathing.enabled = false;
        //playerAnimator = GetComponent<Animator>();
        anxietyRadius = GetComponent<SphereCollider>();
        cam = FindObjectOfType<CameraController>();
    }

    // Update is called once per frame
    void Update()
    {
        //tip over if dead
        if (thisPlayer.paused)
        {
            return;
        }
        if (dead && !cam.sinking)//not drowning add here
        {
            cam.transform.RotateAround(wholeBody.transform.position, cam.transform.right, -fallSpeed);
            return;
        }

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
                }
                else
                {
                    if(monsters[i].layer == 12) // elevator monster
                    {
                        monstPoints +=  monstDist * 6;
                    }
                    else
                    {
                        monstPoints +=  monstDist;
                    }
                }
            }
            if(monstPoints > 0)
            {
                anxietySpeed = (anxConst) / (monstPoints/monsters.Count);
                //Debug.Log(anxietySpeed + " speed to a total of " + currentAnxietyPoints);

                currentAnxietyPoints += anxietySpeed * Time.deltaTime;
                currentAnxietyPoints = Mathf.Clamp(currentAnxietyPoints, 0, totalAnxietyPoints+10);//makes sure we don't have an insane overflow
            }
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
            if(!wasHeavyBreathing)
            {
                breathing.Play();
            }
            wasHeavyBreathing = true;
        }
        else{
            StopCoroutine("Shake");
            anxietyMeterHolder.position = origPosMeter;
            if(wasHeavyBreathing)
            {
                breathing.Pause();
            }
            wasHeavyBreathing = false;
        }
        if(anxietyMeter.fillAmount > .5f)
        {
            desaturate.saturation.value = (anxietyMeter.fillAmount - .5f) * -50f;
            desaturate.postExposure.value = (anxietyMeter.fillAmount - .5f) * -5f;
        }
        if(anxietyMeter.fillAmount <= 0)
        {
            if(wasHeavyBreathing)
            {
                breathing.Stop();
            }
            wasHeavyBreathing = false;
        }
        if (currentAnxietyPoints > totalAnxietyPoints)
        {
            monsters.Clear();
            Grabber playerAbility = thisPlayer.GetComponent<Grabber>(); //drop object when fainting
            if(playerAbility.holdingObject)
            {
                playerAbility.ReleaseObject();
            }
            prepForFaint();
            if (!dead)
            {
                dead = true;

                StartCoroutine("faint");
            }
        }
        anxietyMeter.fillAmount = (currentAnxietyPoints/totalAnxietyPoints);
    }

    private void FixedUpdate()
    {
        checkFloor();
    }

    private void prepForFaint()
    {
        cam.cameraFree = false;
        pAbility.ReleaseObject();
    }

    private void resetPlayer()
    {
        thisPlayer.backToSpawn();//Moved from faint()
        movement.clearDirections();
        cam.sinking = false;
        cam.cameraFree = true;
        cam.resetHead();
    }

    private IEnumerator faint()
    {
        Debug.Log("Faint Running");
        //dead = true;
        
        yield return new WaitForSeconds(.4f);//should be length of animation
        //prepForFaint();
        StartCoroutine("wakeUp");

    }

    private IEnumerator wakeUp()
    {
        if (dead)
        {
            dead = false;//moved up slightly
            Debug.Log("Wakeup Running");
            resetPlayer();
        }
        while (anxietyMeter.fillAmount > 0f)
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
        
        anxietyMeter.fillAmount = 0;
        currentAnxietyPoints = 0;
        desaturate.saturation.value = 0f;
        desaturate.postExposure.value = 0f;
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
                if(!wasHeavyBreathing)
                {
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
        RaycastHit hit;
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * rayDepth, Color.yellow);

        //could adjust starting position to align with player perception
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, rayDepth, ~LayerMask.GetMask("Grabable")))
        {
            
            //can but other floor based traits here
            if (hit.collider.tag == "Deadly") {
                cam.sinking = true;//CHANGES BOOL IN CAMERA CONTROLLER
                currentAnxietyPoints += (100 * Time.deltaTime);
            }
            else
            {
                cam.sinking = false;
            }
        }
        else
        {
            cam.sinking = false;//CHANGES BOOL IN CAMERA CONTROLLER
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        postProcess = (Volume)FindObjectOfType<Volume>();
        postProcess.profile.TryGet(out vignette);
        postProcess.profile.TryGet(out desaturate);
        if(vignette)
        {
            vignette.intensity.value = 0f;
        }
        else
        {
            Debug.LogWarning("Vignette missing");
        }

        if(desaturate)
        {
            desaturate.saturation.value = 0f;
            desaturate.postExposure.value = 0f;
        }
        else
        {
            Debug.LogWarning("Vignette missing");
        }
        monsters = new List<GameObject>();
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        /*postProcess = (Volume)FindObjectOfType<Volume>();
        postProcess.profile.TryGet(out vignette);
        postProcess.profile.TryGet(out desaturate);
        if(vignette)
        {
            vignette.intensity.value = 0f;
        }
        else
        {
            Debug.LogWarning("Vignette missing");
        }

        if(desaturate)
        {
            desaturate.saturation.value = 0f;
            desaturate.postExposure.value = 0f;
        }
        else
        {
            Debug.LogWarning("Vignette missing");
        }
        monsters = new List<GameObject>();*/
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
        monsters.Clear();
    }
}
