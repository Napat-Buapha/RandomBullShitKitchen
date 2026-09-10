using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardCardGame_KitchenWare : CardCardGame_Base , IPointerInteractAble
{
    public KitchenWare_Variable kitchenWareVariable { get; private set; }

    [Header("KitchenWare Card Visual")]
    [SerializeField] private TMP_Text ingredientSlotT;


    public override void Init(Card card)
    {       
        base.Init(card);
        ApplyKitchenWareVisual();
    }

    public void ApplyKitchenWareVisual()
    {
        ingredientSlotT.text =  kitchenWareVariable.ingredientSlot.ToString();
    }

    protected override void RecordCardData(Card card)
    {
        base.RecordCardData(card);
        if(card is Card_KitchenWare kitchenwareCard)
        kitchenWareVariable = kitchenwareCard.kitchenWare_Variable;
    }

    public void OnClick()
    {
        GameManager.Instance.SceneManager.PlaceKitchenWare(this);
    }

    public void OnPointerOver()
    {
        
    }
}


