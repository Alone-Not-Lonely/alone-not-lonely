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
    private void Awake() {
        thisTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Image>().alphaHitTestMinimumThreshold = .001f;
        //canvas.worldCamera = Camera.main;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Clicked");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Start Drag");
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("EndDrag");
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        thisTransform.anchoredPosition += eventData.delta / (canvas.scaleFactor + 3f); 
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }
}
