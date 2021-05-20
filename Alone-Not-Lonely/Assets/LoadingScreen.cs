using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    private Image loadingAnim;

    private Player _player;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyImmediate(this);
        }
        loadingAnim = this.GetComponent<Image>();
        loadingAnim.enabled = false;
    }
    private void Start() {
        _player = Player.instance;
    }

    public void LoadScene(string sceneName, Transform playerSpawn)
    {
        _player.gameObject.SetActive(false);
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

        yield return StartCoroutine(FadeLoadingScreen(0, 1, playerSpawn));
        loadingAnim.enabled = false;
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
