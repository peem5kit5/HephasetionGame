using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    
    private CanvasGroup canvasGroup;
   

    private void Awake()
    {
       
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
       
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Begin Drag");
        canvasGroup.alpha = .6f; 
        canvasGroup.blocksRaycasts = false;
    }
    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("Dragging");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("End Drag");
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        Debug.Log("Pointed Click");
    }
    public void OnDrop(PointerEventData eventData)
    {

    }
}
