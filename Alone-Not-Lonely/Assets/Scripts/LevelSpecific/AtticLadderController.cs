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
    private PlayerAbilityController pAbil;
    public float doorProgress = 0;
    public ContextualUI cu;
    private PromptController gOPU;
    public Sprite tIm;

    private void Start()
    {
        canUseLadder = false;
        pAbil = FindObjectOfType<PlayerAbilityController>();
        //win = GetComponent<WinCondition>();
        _player = Player.instance;
        //closedLadderAnimator.SetFloat("anim_speed", state);
        _player._actionMap.Platforming.SkipLevel.performed += skip => SkipLevel();
        gOPU = FindObjectOfType<PromptController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pAbil.ReleaseObject();//drop box just in case still holding it
            //LoadingScreen.instance.gameObject.SetActive(true);
           
            _player.gameObject.SetActive(false);
            //_player.gameObject.transform.position =  new Vector3(32.7400017f,4.98999977f,-62.7200012f);
            //_player.gameObject.SetActive(true);
            ProgressionTracker.instance.MarkSceneCompleted("Attic2");
            //SceneManager.LoadSceneAsync(nextScene);
            Transform newTransform = _player.transform;
            newTransform.position = new Vector3(32.7400017f, 4.98999977f, -62.7200012f);
            newTransform.rotation = Quaternion.identity; //CHANGE THIS LINE

            LoadingScreen.instance.LoadScene(nextScene, newTransform);
            gOPU.clearSpecificPrompter(cu);
        }
    }

    void SkipLevel()
    {
        //LoadingScreen.instance.gameObject.SetActive(true);
        _player.gameObject.SetActive(false);
        //_player.gameObject.transform.position =  new Vector3(32.7400017f,4.98999977f,-62.7200012f);
        //_player.gameObject.SetActive(true);
        //ProgressionTracker[] p = FindObjectsOfType<ProgressionTracker>();
        //Debug.Log(p.Length);
        ProgressionTracker.instance.MarkSceneCompleted("Attic2");
        //SceneManager.LoadSceneAsync(nextScene);
        Transform newTransform = _player.transform;
        newTransform.position = new Vector3(32.7400017f, 4.98999977f, -62.7200012f);
        newTransform.rotation = Quaternion.identity; //CHANGE THIS LINE
        //LoadingScreen.instance.SetReturning(tIm, false);
        LoadingScreen.instance.LoadScene(nextScene, newTransform);
        gOPU.clearSpecificPrompter(cu);

    }


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
        if (_player.paused)
        {
            return;
        }
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
            cu.startPoint = 2;//loop around for the next part of text (sort of a hack)
            cu.endPoint = 2;
            cu.nextPrompt();
        }
    }

    public void close()
    {
        if (_player.paused)
        {
            return;
        }
        doorProgress -= upspeed;
        doorProgress = Mathf.Clamp(doorProgress, 0f, 1f);
        state = easeInOutQuint(doorProgress);
        state = Mathf.Clamp(state, 0f, 1f);
        closedLadderAnimator.SetFloat("anim_speed", state);
    }

    //taken from easings.net
    private float easeInOutQuint(float x)
    {
        return x < 0.5 ? (16 * Mathf.Pow(x, 5)) : (1 - Mathf.Pow((-2 * x + 2), 5) / 2);
    }
}