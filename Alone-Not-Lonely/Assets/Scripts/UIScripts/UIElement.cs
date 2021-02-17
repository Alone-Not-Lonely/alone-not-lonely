using UnityEngine;
using UnityEngine.UI;
 using UnityEngine.EventSystems;
 public class UIElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
 {
    //private bool mouse_over = false;
    public Sprite[] mmsprites;
    public Image imgComp;
 
    public void OnPointerEnter(PointerEventData eventData)
    {
        imgComp.sprite = mmsprites[1];
        Debug.Log("Mouse enter");
    }
 
    public void OnPointerExit(PointerEventData eventData)
    {
        imgComp.sprite = mmsprites[0];
        Debug.Log("Mouse exit");
    }
 }