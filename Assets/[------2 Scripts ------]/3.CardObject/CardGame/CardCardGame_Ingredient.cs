using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CardCardGame_Ingredient : CardCardGame_Base, IPointerInteractAble
{

    public Ingredient_Variable ingredientVariable { get; protected set; }

    [Header("Ingredient Card Visual")]
    [SerializeField] private TMP_Text tastePointT;
    [SerializeField] private TMP_Text cardCostT;
    [SerializeField] private TMP_Text ingredientTypeT;

    void OnEnable()
    {
        OnDeSelect();
    }


    #region SetUp
    public override void Init(Card card)
    {
        base.Init(card);
        ApplyIngredientVisual();
    }

    public void ApplyIngredientVisual()
    {
        tastePointT.text = ingredientVariable.tastePoint.ToString();
        cardCostT.text = ingredientVariable.cardCost.ToString();
        ingredientTypeT.text = ingredientVariable.ingredientType.ToString();
    }

    protected override void RecordCardData(Card card)
    {
        base.RecordCardData(card);

        if (card is Card_Ingredient ingredientCard)
            ingredientVariable = ingredientCard.ingredientVariable;
    }
    #endregion

    public void ModifiedTastePoint(float amout)
    {
        var tempIngredeintVar = ingredientVariable;
        tempIngredeintVar.tastePoint += amout;
        ingredientVariable = tempIngredeintVar;
    }



    #region Interface Method
    public virtual void OnClick()
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
    #endregion
}


