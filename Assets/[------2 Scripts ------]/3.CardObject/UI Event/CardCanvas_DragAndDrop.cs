using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardCanvas_DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IDragHandler
{
    CardCardGame_Base card;
    public bool isDragging {get; private set;}

    private void OnEnable()
    {
        card = GetComponent<CardCardGame_Base>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        
    }
    public void OnDrag(PointerEventData eventData)
    {
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }


}

