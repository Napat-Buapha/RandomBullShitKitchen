using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CardCardGame_Ingredient : CardCardGame_Base , IPointerInteractAble
{

    public Ingredient_Variable ingredientVariable { get; protected set; }

    [Header("Ingredient Card Visual")]
    [SerializeField] private TMP_Text tastePointT;
    [SerializeField] private TMP_Text cardCostT;
    [SerializeField] private TMP_Text ingredientTypeT;

    void OnEnable()
    {
        OnDeselect();
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


    public virtual void OnClick()
    {
        OnClickEvent.Invoke(this);
    }

    public void OnPointerOver()
    {

    }
}


