using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashMonsterController : MonoBehaviour
{
    public float timeToSmash = 5f;
    public float smashCountdown = 0;
    private bool smashing = true;
    private Vector3 groundLocation;
    private Vector3 raisedLocation;
    private Rigidbody rb;
    public float liftHeight = 5f;
    public float deltaPos = .25f;

    public GameObject mostRecentSquash = null;
    public float timeToUnSquash = 5f;
    private float currentSquashTime = 0f;

    public Color colorToFlash;
    public bool currentlyFlashColor = false;
    private Color usualColor;

    private void Start() {
        groundLocation = this.transform.position;
        groundLocation -= new Vector3(0, liftHeight, 0);
        raisedLocation = this.transform.position;
        //raisedLocation += new Vector3(0, liftHeight, 0);
        rb = this.GetComponent<Rigidbody>();
    }

    private void Update() {
        if(!smashing && smashCountdown < timeToSmash)
        {
            smashCountdown += Time.deltaTime;
        }
        else if(!smashing && smashCountdown >= timeToSmash)
        {
            smashing = true;
            smashCountdown = 0f;
        }
        //manage squash
        if(mostRecentSquash != null && currentSquashTime < timeToUnSquash)
        {
            currentSquashTime += Time.deltaTime;
        }
        else if(mostRecentSquash != null && currentSquashTime >= timeToUnSquash)
        {
            mostRecentSquash.GetComponent<BoxSquashBehavior>().UnSquash();
            GameObject unsquashedObj = Instantiate(mostRecentSquash.GetComponent<BoxSquashBehavior>().regularVariant, mostRecentSquash.transform.position + (transform.up * .5f), Quaternion.identity);
            unsquashedObj.GetComponent<BoxSquashBehavior>().squashed = false;
            unsquashedObj.GetComponent<BoxSquashBehavior>().squashedVariant = mostRecentSquash.GetComponent<BoxSquashBehavior>().squashedVariant;
            unsquashedObj.GetComponent<BoxSquashBehavior>().regularVariant = mostRecentSquash.GetComponent<BoxSquashBehavior>().regularVariant;
            unsquashedObj.GetComponentInChildren<MeshRenderer>().material.SetColor("Color_C8F70FC4", usualColor);
            Destroy(mostRecentSquash);
            mostRecentSquash = null;
            currentSquashTime = 0f;
        }

        //applying effects & feedback!
        if(mostRecentSquash != null && currentSquashTime >= (3*timeToUnSquash)/4)
        {
            if(!currentlyFlashColor && Mathf.Round(Mathf.Sin((5 * currentSquashTime))) == 0)
            {
                mostRecentSquash.GetComponentInChildren<MeshRenderer>().material.SetColor("Color_C8F70FC4", colorToFlash);
                currentlyFlashColor = true;
            }
            else if(currentlyFlashColor && Mathf.Round(Mathf.Sin((5 * currentSquashTime))) == 1 || Mathf.Round(Mathf.Sin((5 * currentSquashTime))) == -1)
            {
                mostRecentSquash.GetComponentInChildren<MeshRenderer>().material.SetColor("Color_C8F70FC4", usualColor);
                currentlyFlashColor = false;
            }
        }
    }

    private void FixedUpdate() {
        if(smashing)
        {
            rb.MovePosition(this.transform.position - new Vector3(0,deltaPos * Time.deltaTime, 0));
            deltaPos += .1f;
            if(transform.position.y < groundLocation.y)
            {
                smashing = false;
                deltaPos = 1f;
            }
        }
        else
        {
            rb.MovePosition(this.transform.position - new Vector3(0,-deltaPos * Time.deltaTime, 0));
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(smashing && other.gameObject.CompareTag("Grabable") && !other.isTrigger && other.gameObject.GetComponentInParent<SquashedObject>() == null && mostRecentSquash == null)
        {
            other.gameObject.GetComponentInParent<BoxSquashBehavior>().Squash();
            mostRecentSquash = Instantiate(other.gameObject.GetComponentInParent<BoxSquashBehavior>().squashedVariant, other.gameObject.transform.position, Quaternion.identity);
            mostRecentSquash.GetComponent<BoxSquashBehavior>().squashed = true;
            mostRecentSquash.GetComponent<BoxSquashBehavior>().squashedVariant = other.gameObject.GetComponentInParent<BoxSquashBehavior>().squashedVariant;
            mostRecentSquash.GetComponent<BoxSquashBehavior>().regularVariant = other.gameObject.GetComponentInParent<BoxSquashBehavior>().regularVariant;
            usualColor = mostRecentSquash.GetComponentInChildren<MeshRenderer>().material.GetColor("Color_C8F70FC4");
            Destroy(other.gameObject.transform.parent.gameObject);
        }
        else if (smashing && other.gameObject.CompareTag("Grabable") && !other.isTrigger && other.gameObject.GetComponentInParent<SquashedObject>() == null && mostRecentSquash != null)
        {
            //UNSQUASH
            mostRecentSquash.GetComponent<BoxSquashBehavior>().UnSquash();
            GameObject unsquashedObj = Instantiate(mostRecentSquash.GetComponent<BoxSquashBehavior>().regularVariant, mostRecentSquash.transform.position + (transform.up * .5f), Quaternion.identity);
            unsquashedObj.GetComponent<BoxSquashBehavior>().squashed = false;
            unsquashedObj.GetComponent<BoxSquashBehavior>().squashedVariant = mostRecentSquash.GetComponent<BoxSquashBehavior>().squashedVariant;
            unsquashedObj.GetComponent<BoxSquashBehavior>().regularVariant = mostRecentSquash.GetComponent<BoxSquashBehavior>().regularVariant;
            unsquashedObj.GetComponentInChildren<MeshRenderer>().material.SetColor("Color_C8F70FC4", usualColor);
            Destroy(mostRecentSquash);
            mostRecentSquash = null;
            currentSquashTime = 0f;

            //NEW SQUASH
            other.gameObject.GetComponentInParent<BoxSquashBehavior>().Squash();
            mostRecentSquash = Instantiate(other.gameObject.GetComponentInParent<BoxSquashBehavior>().squashedVariant, other.gameObject.transform.position, Quaternion.identity);
            mostRecentSquash.GetComponent<BoxSquashBehavior>().squashed = true;
            mostRecentSquash.GetComponent<BoxSquashBehavior>().squashedVariant = other.gameObject.GetComponentInParent<BoxSquashBehavior>().squashedVariant;
            mostRecentSquash.GetComponent<BoxSquashBehavior>().regularVariant = other.gameObject.GetComponentInParent<BoxSquashBehavior>().regularVariant;
            usualColor = mostRecentSquash.GetComponentInChildren<MeshRenderer>().material.GetColor("Color_C8F70FC4");
            Destroy(other.gameObject.transform.parent.gameObject);
        }
    }
}
