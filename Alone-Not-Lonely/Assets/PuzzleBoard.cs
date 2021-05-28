using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzleBoard : MonoBehaviour, IDropHandler
{
    public float snapThreshold;
    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            if(Vector2.Distance(eventData.pointerDrag.gameObject.GetComponent<RectTransform>().anchoredPosition, this.GetComponent<RectTransform>().anchoredPosition) < snapThreshold)
            {
                eventData.pointerDrag.gameObject.GetComponent<RectTransform>().anchoredPosition = this.GetComponent<RectTransform>().anchoredPosition;
                eventData.pointerDrag.gameObject.GetComponent<DragDrop>().piecePlaced = true;
            }
            else
            {
                eventData.pointerDrag.gameObject.GetComponent<DragDrop>().piecePlaced = false;
            }

            FindObjectOfType<FinalPuzzleManager>().UpdatePuzzleState();
        }
    }
}
