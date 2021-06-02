using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField]private Canvas canvas;
    RectTransform thisTransform;

    private CanvasGroup canvasGroup;
    public bool piecePlaced = false;
    AudioSource startDrag;

    public Vector2 initPosition;
    private void Awake() {
        thisTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Image>().alphaHitTestMinimumThreshold = .001f;
        startDrag = GetComponent<AudioSource>();
        initPosition = thisTransform.anchoredPosition;
        //canvas.worldCamera = Camera.main;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("Clicked");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Start Drag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("EndDrag");
        canvasGroup.blocksRaycasts = true;
        if(Vector2.Distance(thisTransform.anchoredPosition, PuzzleBoard.instance.GetComponent<RectTransform>().anchoredPosition) < PuzzleBoard.instance.snapThreshold)
        {
            //Debug.Log("Piece dropped into correct position");
            thisTransform.anchoredPosition = PuzzleBoard.instance.GetComponent<RectTransform>().anchoredPosition;
            this.piecePlaced = true;
            FinalPuzzleManager.instance.UpdatePuzzleState();
            PuzzleBoard.instance.pieceDropped.Play();
        }
        else
        {
            //Debug.Log("Snap Back to start");
            this.piecePlaced = false;
            thisTransform.anchoredPosition = initPosition;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        thisTransform.anchoredPosition += eventData.delta / (canvas.scaleFactor * transform.lossyScale); 
        startDrag.Play();
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Drop from DragDrop");
    }
}
