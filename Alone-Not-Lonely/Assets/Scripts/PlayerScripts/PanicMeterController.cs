using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PanicMeterController : MonoBehaviour
{
    public Image anxietyMeter;
    public float totalAnxietyPoints = 50f;
    private float currentAnxietyPoints;
    private Player thisPlayer;
    public float anxietySpeed = 10f;
    //private Animator playerAnimator;

    bool monsterInRadius;

    public Volume postProcess;
    private Vignette vignette;
    private ColorAdjustments desaturate;
    void Start()
    {
        currentAnxietyPoints = 0;
        anxietyMeter.fillAmount = currentAnxietyPoints/totalAnxietyPoints;
        monsterInRadius = false;
        thisPlayer = (Player)FindObjectOfType<Player>();
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
        vignette.intensity.value = anxietyMeter.fillAmount * 1.25f;
        if(anxietyMeter.fillAmount > .5f)
        {
            desaturate.saturation.value = (anxietyMeter.fillAmount - .5f) * -50f;
            desaturate.postExposure.value = (anxietyMeter.fillAmount - .5f) * -5f;
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
        desaturate.saturation.value = 0f;
        desaturate.postExposure.value = 0f;
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
