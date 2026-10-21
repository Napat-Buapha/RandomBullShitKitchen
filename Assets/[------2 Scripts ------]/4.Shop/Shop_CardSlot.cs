using TMPro;
using UnityEngine;

public class Shop_CardSlot : MonoBehaviour
{
    Shop_CardShop _shop;

   [SerializeField] TMP_Text priceT;
   
    public void Init(Shop_CardShop shop)
    {
        _shop = shop;
    }

    public void Buy(CardCardGame_Base card)
    {
        if(!GameManager.Instance.ResourceManager.PayGold(card.baseCardRef.cardPirce))
        {
            return;
        }

        _shop.RemoveCard(card);
        SetSold();
        GameManager.Instance.DeckManager.AddCardToDeckList(card.baseCardRef);
        GameManager.Instance.UiManager.UpdateGoldAmout();
    }

    public void SetPriceTag(int price)
    {
        priceT.text = $"{price} gold";
    }

    public void SetSold()
    {
        priceT.text = "Sold!";
    }
}
