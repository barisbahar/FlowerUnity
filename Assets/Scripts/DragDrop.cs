using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DragDrop : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IEndDragHandler,IDragHandler,IDropHandler{
    [SerializeField] private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    Vector3 initialPosition;
private void Start() {
            initialPosition = gameObject.transform.position;
}
    private void Awake() {

        rectTransform=GetComponent<RectTransform>();
        canvasGroup=GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha=.6f;
        canvasGroup.blocksRaycasts=false;
    }

    
    public void OnDrag(PointerEventData eventData)
    { 
        rectTransform.anchoredPosition+=eventData.delta/canvas.scaleFactor;    
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha=1f;
        canvasGroup.blocksRaycasts=true;
        this.gameObject.transform.position=initialPosition;
        if(ItemSlot.FindObjectOfType<ItemSlot>().CanHydrate()==0){
            this.gameObject.GetComponent<DragDrop>().enabled=false;
        }     
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
