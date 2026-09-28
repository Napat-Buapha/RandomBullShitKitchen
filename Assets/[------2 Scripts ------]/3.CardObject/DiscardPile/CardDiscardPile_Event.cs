using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class CardDiscardPile_Event : MonoBehaviour , IPointerClickHandler
{
    public UnityEvent<CardCardGame_Base> OnClickEvent { get; private set; }
    CardCardGame_Base card;

    private void OnEnable() 
    {
        OnClickEvent = new UnityEvent<CardCardGame_Base>();
        card = GetComponent<CardCardGame_Base>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickEvent?.Invoke(card);
    }
}
