using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    public static LoadingScreen instance;
    private Image loadingAnim;
    Animator animator;

    private Player _player;
    private void Awake() {
        if(instance == null)
        {
            instance = this;
            loadingAnim = GetComponent<Image>();
            animator = this.GetComponent<Animator>();
            loadingAnim.enabled = false;
            //animator.enabled = false;
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
    /*public void SetReturning()
    {
        animator.enabled = false;
        loadingAnim.enabled = true;
        Debug.Log("Image changed, animator: " + animator.enabled);
        animator.enabled = true;
        animator.SetBool("Returning", returning);//determines which animation to play
 
    }*/

    public void LoadScene(string sceneName, Transform playerSpawn, Vector3 desiredCameraRotation)
    {
        _player.gameObject.SetActive(false);
        Camera.main.GetComponent<CameraController>().cameraFree = false;
        if(sceneName == "Attic2")
        {
            _player.transform.position = playerSpawn.position;
            _player.transform.rotation = playerSpawn.rotation;
            _player.gameObject.SetActive(true);
        }
        StartCoroutine(StartLoad(sceneName, playerSpawn, desiredCameraRotation));
    }

    IEnumerator StartLoad(string sceneToLoad, Transform playerSpawn, Vector3 desiredCameraRotation)
    {
        loadingAnim.enabled = true;
        yield return StartCoroutine(FadeLoadingScreen(1, 1, playerSpawn, desiredCameraRotation));

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
            yield return StartCoroutine(FadeLoadingScreen(0, 1, playerSpawn, desiredCameraRotation));
            loadingAnim.enabled = false;
        }
    }

    IEnumerator FadeLoadingScreen(float targetValue, float duration, Transform playerSpawn, Vector3 desiredCameraRotation)
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
            Camera.main.transform.localEulerAngles = desiredCameraRotation;
            StartCoroutine(WaitForCamera());
        }
        loadingAnim.color = new Color(loadingAnim.color.r, loadingAnim.color.g, loadingAnim.color.b, targetValue);
    }

    IEnumerator WaitForCamera()
    {
        yield return new WaitForSeconds(4);
        Camera.main.GetComponent<CameraController>().cameraFree = true;
    }
}
