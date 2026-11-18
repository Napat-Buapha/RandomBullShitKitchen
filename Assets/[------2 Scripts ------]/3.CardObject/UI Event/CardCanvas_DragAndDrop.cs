using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class CardCanvas_DragAndDrop : MonoBehaviour,
IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IDragHandler

{
    [SerializeField] float speed = 30f;
    CardCardGame_Base card;
    RectTransform rectTransform;

    [HideInInspector] public UnityEvent<CardCardGame_Base> OnDragEndEvent;
    public bool isDragging { get; private set; }

    private void OnEnable()
    {
        OnDragEndEvent = new UnityEvent<CardCardGame_Base>();
        card = GetComponent<CardCardGame_Base>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void OnDisable()
    {
        OnDragEndEvent.RemoveAllListeners();
    }

    private void Update()
    {
        if (isDragging)
        {
            MouseDrag();
        }
    }

    private void MouseDrag()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();

        rectTransform.position = Vector2.Lerp(
            rectTransform.position,
            mousePos,
            speed * Time.deltaTime
        );
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        card.GetComponent<SortingGroup>().sortingOrder = 100;
        card.ApplySortingLayerToCanvas(100);
    }
    public void OnDrag(PointerEventData eventData)
    {
        isDragging = true;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isDragging = false;

        CheckDrop();
    }

    public void OnPointerDown(PointerEventData eventData)
    {

    }

    private void CheckDrop()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current);
        pointerData.position = Mouse.current.position.ReadValue();
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.CompareTag("Background"))
            {
                Debug.Log("Hit Background - Stop Raycast");
                break;
            }

            if (result.gameObject.TryGetComponent(out IDropReceiver drop))
            {
                if(drop.DropReceive(card))
                return;
            }
        }

        OnDragEndEvent.Invoke(card);
    }
}

