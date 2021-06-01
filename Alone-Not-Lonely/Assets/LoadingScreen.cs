using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    private Image loadingAnim;
    private Animator animator;

    private Player _player;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            loadingAnim = this.GetComponent<Image>();
            animator = this.GetComponent<Animator>();
            loadingAnim.enabled = false;
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    private void Start() {
        _player = Player.instance;
    }

    //called by various returns to change scene transitions
    public void SetReturning(bool returning)
    {
        animator.SetBool("Returning", returning);//determines which animation to play
    }

    public void LoadScene(string sceneName, Transform playerSpawn)
    {
        _player.gameObject.SetActive(false);
        if(sceneName == "Attic2")
        {
            _player.transform.position = playerSpawn.position;
            _player.transform.rotation = playerSpawn.rotation;
            _player.gameObject.SetActive(true);
        }
        else if(sceneName == "MainMenu")
        {
            
        }
        StartCoroutine(StartLoad(sceneName, playerSpawn));
    }

    IEnumerator StartLoad(string sceneToLoad, Transform playerSpawn)
    {
        loadingAnim.enabled = true;
        yield return StartCoroutine(FadeLoadingScreen(1, 1, playerSpawn));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!operation.isDone)
        {
            yield return null;
        }
        if(sceneToLoad == "MainMenu")
        {
            DestroyImmediate(_player.transform.parent.gameObject);
            yield return null;
        }
        else
        {
            yield return StartCoroutine(FadeLoadingScreen(0, 1, playerSpawn));
            loadingAnim.enabled = false;
        }
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration, Transform playerSpawn)
    {
        float startValue = loadingAnim.color.a;
        float time = 0;

        while (time < duration)
        {
            loadingAnim.color = new Color(loadingAnim.color.r, loadingAnim.color.g, loadingAnim.color.b, Mathf.Lerp(startValue, targetValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        if(targetValue == 0) //fade out load - spawn player
        {
            _player.transform.position = playerSpawn.position;
            _player.transform.rotation = playerSpawn.rotation;
            _player.gameObject.SetActive(true);
        }
        loadingAnim.color = new Color(loadingAnim.color.r, loadingAnim.color.g, loadingAnim.color.b, targetValue);
    }
}
