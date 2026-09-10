using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Stove : MonoBehaviour
{

    [SerializeField] Stove_IngredientUi slotUi;
    [SerializeField] SpriteRenderer kitchenWareSpriteRenderer;
    [SerializeField] IngredientCharacteristics[] ingredientSlots;
    [SerializeField] GameObject addButton;
    [SerializeField] Button cookButton;

    [SerializeField] KitchenWare_Variable kitchenWareVariable;


    [field: SerializeField] public bool isOccupied { get; private set; } = false;

    void Start()
    {
        ResetStove();
    }

    private void ResetStove()
    {
        isOccupied = false;
        DisableAddButton();
        DisableCookButton();

        kitchenWareSpriteRenderer.sprite = null;
        slotUi.DisableIngredientUI();
    }

    public void PlaceKitchenWare(CardCardGame_KitchenWare kitchenWare)
    {
        kitchenWareVariable = kitchenWare.kitchenWareVariable;
        ingredientSlots = new IngredientCharacteristics[kitchenWareVariable.ingredientSlot];
        kitchenWareSpriteRenderer.sprite = kitchenWareVariable.kitchenWareActiveSprite;
        slotUi.EnableIngredientUI(kitchenWare);

        isOccupied = true;
        GameManager.Instance.HandsManager.UnSelectAllIngredient();
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
                ingredientSlots[i] = new IngredientCharacteristics
                (ingredient.cardData.cardName,
                ingredient.ingredientVariable,
                ingredient.cardData.cardImage);
                CheckIsFull();
                slotUi.UpdateSlots(ingredientSlots);
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
        ResetStove();
    }
    #endregion
}

[System.Serializable]
public struct IngredientCharacteristics
{
    public string ingredientName;
    public Ingredient_Variable ingredientVariable;
    public Sprite ingredientSprite;

    public IngredientCharacteristics(string ingredientName, Ingredient_Variable ingredientVariable, Sprite ingredientSprite)
    {
        this.ingredientName = ingredientName;
        this.ingredientVariable = ingredientVariable;
        this.ingredientSprite = ingredientSprite;
    }
}
