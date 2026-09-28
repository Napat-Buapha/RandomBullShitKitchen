using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stove : MonoBehaviour
{
    [Header("UI Ref")]
    //Base Card Data Ref
    Card _baseCardKitchenWareRef;


    [SerializeField] SpriteRenderer kitchenWareSpriteRenderer;
    [SerializeField] KitchenWare_Variable kitchenWareVariable;

    IngredientCharacteristics[] ingredientSlots;
    [SerializeField] Stove_IngredientUi slotUi;

    [SerializeField] GameObject addButton;
    [SerializeField] Button cookButton;

    [SerializeField] TMP_Text totalTastePointT;




    public bool isOccupied { get; private set; } = false;

    void Start()
    {
        ResetStove();
        UpdateTotalTastePoint();
    }

    private void ThrownInToTrashBin()
    {
        GameManager.Instance.TrashBinManager.Receive(_baseCardKitchenWareRef);
        foreach (var ingreditnCard in ingredientSlots)
        {
            GameManager.Instance.TrashBinManager.Receive(ingreditnCard.baseCard);
        }
    }

    private void ResetStove()
    {
        _baseCardKitchenWareRef = null;

        isOccupied = false;
        DisableAddButton();
        DisableCookButton();

        kitchenWareSpriteRenderer.sprite = null;
        slotUi.DisableIngredientUI();
    }

    public void PlaceKitchenWare(CardCardGame_KitchenWare kitchenWare)
    {
        _baseCardKitchenWareRef = kitchenWare.baseCardRef;

        kitchenWareVariable = kitchenWare.kitchenWareVariable;
        ingredientSlots = new IngredientCharacteristics[kitchenWareVariable.ingredientSlot];
        kitchenWareSpriteRenderer.sprite = kitchenWareVariable.kitchenWareActiveSprite;
        slotUi.EnableIngredientUI(kitchenWare);

        isOccupied = true;
        GameManager.Instance.HandsManager.UnSelectAllIngredient();
    }

    public void UpdateTotalTastePoint()
    {
        if(ingredientSlots == null)
        {
            totalTastePointT.text = "0";
            return;
        }

        float totalTastePoint = 0;

        foreach (var ingredient in ingredientSlots)
        {
            totalTastePoint += ingredient.ingredientVariable.tastePoint;
        }

        totalTastePointT.text = totalTastePoint.ToString();
    }


    /// <summary>
    /// Return false mean KitchenWare is full 
    /// </summary>
    public bool AddIngredient(CardCardGame_Ingredient ingredient)
    {
        if (!GameManager.Instance.ResourceManager.PayTimePoint(ingredient.ingredientVariable.cardCost))
        {
            Debug.Log("Not Enough Time Point");
            return false;
        }


        for (int i = 0; i < ingredientSlots.Length; i++)
        {
            if (string.IsNullOrEmpty(ingredientSlots[i].ingredientName))
            {
                ingredient.ExecuteEffects();

                ingredientSlots[i] = new IngredientCharacteristics
                (
                    ingredient.baseCardRef,
                    ingredient.cardData.cardName,
                    ingredient.ingredientVariable,
                    ingredient.cardData.cardImage
                );

                CheckIsFull();
                slotUi.UpdateSlots(ingredientSlots);
                UpdateTotalTastePoint();

                return true;
            }

        }

        return false;
    }

    private void CheckIsFull()
    {
        if (!string.IsNullOrEmpty(ingredientSlots[ingredientSlots.Length - 1].ingredientName))
        {
            EnableCookButton();
        }
    }

    #region Button Event

    // Add ingredients
    public void EnableAddButton()
    {
        addButton.SetActive(true);
    }
    public void DisableAddButton()
    {
        addButton.SetActive(false);
    }
    public void AddButton()
    {
        GameManager.Instance.HandsManager.AddSelectedIngredientToStove(this);
    }

    // Cooking
    public void EnableCookButton()
    {
        cookButton.interactable = true;
    }
    public void DisableCookButton()
    {
        cookButton.interactable = false;
    }
    public void CookButton()
    {
        GameManager.Instance.CookingManager.Cook(kitchenWareVariable.recipeList, ingredientSlots.ToList());
        Debug.Log("Cook Complete");
        ThrownInToTrashBin();
        ResetStove();
    }
    #endregion
}

[System.Serializable]
public struct IngredientCharacteristics
{
    public Card baseCard;
    public string ingredientName;
    public Ingredient_Variable ingredientVariable;
    public Sprite ingredientSprite;

    public IngredientCharacteristics(Card baseCard, string ingredientName, Ingredient_Variable ingredientVariable, Sprite ingredientSprite)
    {
        this.baseCard = baseCard;
        this.ingredientName = ingredientName;
        this.ingredientVariable = ingredientVariable;
        this.ingredientSprite = ingredientSprite;
    }
}
