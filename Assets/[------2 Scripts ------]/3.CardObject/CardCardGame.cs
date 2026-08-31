using UnityEngine;

public class CardCardGame : MonoBehaviour , IPointerInteractAble
{
    Card _cardData;

    public void Init(Card card)
    {
        _cardData = card;
    }

    public void OnClick()
    {
        Debug.Log(_cardData.cardName);
    }

    public void OnPointerOver()
    {
        //Show Bigger Card Ui
    }
}
