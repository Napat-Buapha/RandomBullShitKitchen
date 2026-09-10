using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CardCardGame_Ingredient : CardCardGame_Base , IPointerInteractAble
{

    public Ingredient_Variable ingredientVariable { get; private set; }
    bool isSelected = false;

    [Header("Ingredient Card Visual")]
    [SerializeField] private TMP_Text tastePointT;
    [SerializeField] private TMP_Text cardCostT;
    [SerializeField] private TMP_Text ingredientTypeT;
    [SerializeField] private SpriteRenderer selectedHighlight;

    void OnEnable()
    {
        OnDeselect();
    }


    #region SetUp
    public override void Init(Card card)
    {
        base.Init(card);
        isSelected = false;
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

    public void OnSelect()
    {
        isSelected = true;
        selectedHighlight.enabled = true;
    }

    public void OnDeselect()
    {
        isSelected = false;
        selectedHighlight.enabled = false;
    }

    public void OnClick()
    {
        if (!isSelected)
            GameManager.Instance.HandsManager.SelectIngredient(this);
        else
            GameManager.Instance.HandsManager.UnSelectIngredient(this);
    }

    public void OnPointerOver()
    {

    }
}


