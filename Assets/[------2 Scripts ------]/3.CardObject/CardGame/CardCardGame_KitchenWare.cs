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
        OnClickEvent.Invoke(this);
    }

    public void OnPointerOver()
    {
        
    }

public void OnHold()
    {
        if (GameManager.Instance.currentGameState != GameManager.GameState.NormalMode) return;
        GameManager.Instance.HandsManager.Holding(this);
    }

    public void OnDrop()
    {
        if (GameManager.Instance.currentGameState != GameManager.GameState.CardHolding) return;
        GameManager.Instance.HandsManager.Drop(this);

        if(DetectDropAbleObject())
        {
            GameManager.Instance.HandsManager.Discard(this);
        }
    }

    private bool DetectDropAbleObject()
    {
        Collider2D[] detectedCollider = Physics2D.OverlapBoxAll(
        _col.bounds.center,
        _col.bounds.size,
        10f);

        foreach (var detected in detectedCollider)
        {
            if (detected.TryGetComponent(out IDropReceiver dropReceiver))
            {
                return dropReceiver.DropReceive(this);
            }
        }

        return false;
    }
}


