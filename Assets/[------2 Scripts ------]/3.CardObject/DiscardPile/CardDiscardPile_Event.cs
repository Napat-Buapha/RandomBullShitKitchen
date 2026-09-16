using UnityEngine;
using UnityEngine.EventSystems;

public class CardDiscardPile : IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Test");
    }
}
