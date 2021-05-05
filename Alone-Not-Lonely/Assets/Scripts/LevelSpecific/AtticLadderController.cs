using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtticLadderController : MonoBehaviour
{
    //public bool boxBlockingExit = false;
    public string nextScene = "Kitchen";
    public Animator closedLadderAnimator;
    public GameObject openLadder;
    public bool canUseLadder;
    public float upspeed = .001f, downspeed = .001f, finallyOpen = .9f;
    [SerializeField]
    private float state = 0;
    //private WinCondition win;
    private Player _player;
    private float doorProgress = 0;

    private void Start() {
        canUseLadder = false;
        
        //win = GetComponent<WinCondition>();
        _player = FindObjectOfType<Player>();
        //closedLadderAnimator.SetFloat("anim_speed", state);
        _player._actionMap.Platforming.SkipLevel.performed += skip => SkipLevel();
    }

    private void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other) {
        //if(other.CompareTag("PhysicalGrabable"))
        //{
           // boxBlockingExit = true;
        //}

        if(other.CompareTag("Player"))
        {
            //_player.gameObject.transform.Translate(new Vector3(32.7400017f,4.98999977f,-62.7200012f) - _player.gameObject.transform.position);
            _player.gameObject.SetActive(false);
            _player.gameObject.transform.position =  new Vector3(32.7400017f,4.98999977f,-62.7200012f);
            _player.gameObject.SetActive(true);
            ProgressionTracker[] p = FindObjectsOfType<ProgressionTracker>();
            p[0].MarkSceneCompleted("Attic2");
            SceneManager.LoadScene(nextScene);
        }
    }

    void SkipLevel()
    {
        _player.gameObject.SetActive(false);
        _player.gameObject.transform.position =  new Vector3(32.7400017f,4.98999977f,-62.7200012f);
        _player.gameObject.SetActive(true);
        ProgressionTracker[] p = FindObjectsOfType<ProgressionTracker>();
        p[0].MarkSceneCompleted("Attic2");
        SceneManager.LoadScene(nextScene);
    }

    private void OnTriggerExit(Collider other) {
        //if(other.CompareTag("PhysicalGrabable"))
        //{
           //boxBlockingExit = false;
        //}
    }

    //public void AnimateOpenLadder()
    //{
    //    closedLadderAnimator.SetBool("OpenLadder", true);
    //}

    public void EnableDisableLadders()
    {
        //win.onWin();
        closedLadderAnimator.gameObject.SetActive(false);
        openLadder.SetActive(true);
        canUseLadder = true;
    }

    //Ladder Pho-Animations

    public void open()
    {
        doorProgress += downspeed;
        doorProgress = Mathf.Clamp(doorProgress, 0f, 1f);
        //Debug.Log("DoorProgress: " + doorProgress);
        state = easeInOutQuint(doorProgress);
        
        //Debug.Log("State: " + state);
        closedLadderAnimator.SetFloat("anim_speed", state);

        if (state > finallyOpen)
        {
            //Debug.Log("past threshold");
            upspeed = 0;
        }
    }

    public void close() {

        doorProgress -= upspeed;
        doorProgress = Mathf.Clamp(doorProgress, 0f, 1f);
        state = easeInOutQuint(doorProgress);
        state = Mathf.Clamp(state, 0f, 1f);
        closedLadderAnimator.SetFloat("anim_speed", state);
    }

    //taken from easings.net
    private float easeInOutQuint(float x){
        return x < 0.5 ? (16 * Mathf.Pow(x,5)) : (1 - Mathf.Pow((-2 * x + 2), 5) / 2);
    }
}
