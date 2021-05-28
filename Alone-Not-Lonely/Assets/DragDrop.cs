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
        canvas.worldCamera = Camera.main;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("TextAsset");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        thisTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnDrop(PointerEventData eventData)
    {
        
    }
}
