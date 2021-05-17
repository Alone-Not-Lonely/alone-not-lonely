using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    private Image loadingAnim;
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

    public void LoadScene(string sceneName)
    {
        StartCoroutine(StartLoad(sceneName));
    }

    IEnumerator StartLoad(string sceneToLoad)
    {
        loadingAnim.enabled = true;
        yield return StartCoroutine(FadeLoadingScreen(1, 1));

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneToLoad);
        while (!operation.isDone)
        {
            yield return null;
        }

        yield return StartCoroutine(FadeLoadingScreen(0, 1));
        loadingAnim.enabled = false;
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration)
    {
        float startValue = loadingAnim.color.a;
        float time = 0;

        while (time < duration)
        {
            loadingAnim.color = new Color(loadingAnim.color.r, loadingAnim.color.g, loadingAnim.color.b, Mathf.Lerp(startValue, targetValue, time / duration));
            time += Time.deltaTime;
            yield return null;
        }
        loadingAnim.color = new Color(loadingAnim.color.r, loadingAnim.color.g, loadingAnim.color.b, targetValue);
    }
}
