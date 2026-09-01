using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardCardGame_KitchenWare : MonoBehaviour , IPointerInteractAble
{
    public CardCardGame_Base cardBase { get; private set; }

    [Header("KitchenWare Card Variable")]
    [field: SerializeField] public int ingredientSlot { get; private set; }
    [field: SerializeField] public Sprite kitchenWareActiveSprite { get; private set; }

    [Header("KitchenWare Card Visual")]
    [SerializeField] private GameObject kitchenWareCardTemplate;
    [SerializeField] private TMP_Text ingredientSlotT;


    public void Init(CardCardGame_Base cardBase , Card card)
    {
        kitchenWareCardTemplate.SetActive(false); // Prevent the template from being visible until the data is applied

        this.cardBase = cardBase;
        if(card is Card_KitchenWare kitchenWareCard)
        {
            RecordCardData(kitchenWareCard);
            ApplyKitchenWareVisual();
        }
        else
        {
            Debug.LogError("Card is not of type Card_KitchenWare");
        }
    }

    public void ApplyKitchenWareVisual()
    {
        kitchenWareCardTemplate.SetActive(true);
        ingredientSlotT.text = ingredientSlot.ToString();
    }

    private void RecordCardData(Card_KitchenWare card)
    {
        ingredientSlot = card.ingredientSlot;
        kitchenWareActiveSprite = card.kitchenWareActiveSprite;
    }

    public void OnClick()
    {
        GameManager.Instance.SceneManager.PlaceKitchenWare(this);
    }

    public void OnPointerOver()
    {
        
    }
}


