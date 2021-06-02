using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class PuzzleBoard : MonoBehaviour, IDropHandler
{
    public float snapThreshold;
    public static PuzzleBoard instance;
    public AudioSource pieceDropped;

    void Awake() {
        PuzzleBoard[] objs = (PuzzleBoard[])FindObjectsOfType<PuzzleBoard>();

        if (objs.Length > 1)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            //DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
        pieceDropped = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //.Log("Loaded into " + scene.name);
        if(scene.name != "GroundFloor")
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

    private void OnDisable(){
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(Vector2.Distance(eventData.pointerDrag.gameObject.GetComponent<RectTransform>().anchoredPosition, this.GetComponent<RectTransform>().anchoredPosition) < snapThreshold)
            {
                //Debug.Log("Piece dropped into correct position");
                eventData.pointerDrag.gameObject.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.gameObject.GetComponent<DragDrop>().piecePlaced = true;
                pieceDropped.Play();
            }
            else
            {
                //Debug.Log("Piece dropped in incorrect location");
                eventData.pointerDrag.gameObject.GetComponent<DragDrop>().piecePlaced = false;
                eventData.pointerDrag.gameObject.GetComponent<RectTransform>().anchoredPosition = eventData.pointerDrag.gameObject.GetComponent<DragDrop>().initPosition;
            }

            FinalPuzzleManager.instance.UpdatePuzzleState();
        }
    }
}
