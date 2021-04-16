using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtticLadderController : MonoBehaviour
{
    //public bool boxBlockingExit = false;
    public Animator closedLadderAnimator;
    public GameObject openLadder;
    public bool canUseLadder;
    private float state = 1;
    //private WinCondition win;
    private Player _player;

    private void Start() {
        canUseLadder = false;
        
        //win = GetComponent<WinCondition>();
        _player = FindObjectOfType<Player>();
        closedLadderAnimator.SetFloat("anim_speed", state);
        _player._actionMap.Platforming.SkipLevel.performed += skip => SkipLevel();
    }

    private void Update()
    {
        
        Debug.Log("state value: " + state);
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
            SceneManager.LoadScene("KitchenGraybox");
        }
    }

    void SkipLevel()
    {
        _player.gameObject.SetActive(false);
        _player.gameObject.transform.position =  new Vector3(32.7400017f,4.98999977f,-62.7200012f);
        _player.gameObject.SetActive(true);
        SceneManager.LoadScene("KitchenGraybox");
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
        Debug.Log("opening");
        state -= .001f;
        state = Mathf.Clamp(state,0f,1f);
        closedLadderAnimator.SetFloat("anim_speed", state);
    }

    public void close() {
        Debug.Log("closing");
        state += .001f;
        state = Mathf.Clamp(state, 0f, 1f);
        closedLadderAnimator.SetFloat("anim_speed", state);
    }
}
