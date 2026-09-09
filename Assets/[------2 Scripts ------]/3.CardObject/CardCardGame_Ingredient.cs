using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class CardCardGame_Ingredient : MonoBehaviour , IPointerInteractAble
{
    public CardCardGame_Base cardBase {get; private set; }

    [Header("Ingredient Card Variable")]
    [field: SerializeField] public Ingredient_Variable ingredientVariable { get; private set; }
    bool isSelected = false;

    [Header("Ingredient Card Visual")]
    [SerializeField] private GameObject ingredintCardTemplate;
    [SerializeField] private TMP_Text tastePointT;
    [SerializeField] private TMP_Text cardCostT;
    [SerializeField] private TMP_Text ingredientTypeT;
    [SerializeField] private SpriteRenderer selectedHighlight;

    void OnEnable()
    {
        OnDeselect();
    }


    #region SetUp
    public void Init(CardCardGame_Base cardBase , Card card)
        {
            isSelected = false;
            ingredintCardTemplate.SetActive(false); // Prevent the template from being visible until the data is applied
    
            this.cardBase = cardBase;
            if(card is Card_Ingredient ingredientCard)
            {
                RecordCardData(ingredientCard);
                ApplyIngredientVisual();
            }
            else
            {
                Debug.LogError("Card is not of type Card_Ingredient");
            }
        }
    
        public void ApplyIngredientVisual()
        {
            ingredintCardTemplate.SetActive(true);
            tastePointT.text = ingredientVariable.tastePoint.ToString();
            cardCostT.text = ingredientVariable.cardCost.ToString();
            ingredientTypeT.text = ingredientVariable.ingredientType.ToString();
        }
    
        private void RecordCardData(Card_Ingredient card)
        {
            ingredientVariable = card.ingredientVariable;
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
        if(!isSelected)
            GameManager.Instance.HandsManager.SelectIngredient(this);
        else    
            GameManager.Instance.HandsManager.UnSelectIngredient(this);
    }

    public void OnPointerOver()
    {
        
    }
}


