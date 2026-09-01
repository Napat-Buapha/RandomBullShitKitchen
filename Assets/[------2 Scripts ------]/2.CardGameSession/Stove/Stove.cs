using System.Linq;
using UnityEngine;

public class Stove : MonoBehaviour
{
    [SerializeField] SpriteRenderer kitchenWareSpriteRenderer;
    [SerializeField] IngredientCharacteristics[] slots;
    [SerializeField] GameObject addButton;

    public bool isOccupied { get; private set; } = false;

    void Start()
    {
        isOccupied = false;
        DisableButton();
    }
    public void PlaceKitchenWare(CardCardGame_KitchenWare kitchenWare)
    {
        slots = new IngredientCharacteristics[kitchenWare.ingredientSlot];
        kitchenWareSpriteRenderer.sprite = kitchenWare.kitchenWareActiveSprite;
        isOccupied = true;
        GameManager.Instance.HandsManager.UnSelectAllIngredient();
    }

    public void EnableButton()
    {
        addButton.SetActive(true);
    }
    public void DisableButton()
    {
        addButton.SetActive(false);
    }
    public void AddButton()
    {
        GameManager.Instance.HandsManager.AddSelectedIngredientToStove(this);
    }

    /// <summary>
    /// Return false mean KitchenWare is full 
    /// </summary>
    public bool AddIngredient(CardCardGame_Ingredient ingredient)
    {
        if (!GameManager.Instance.ResourceManager.PayTimePoint(ingredient.cardCost))
        {
            Debug.Log("Not Enough Time Point");
            return false;
        }


        for (int i = 0; i < slots.Length; i++)
        {
            if (string.IsNullOrEmpty(slots[i].ingredientName))
            {
                slots[i] = new IngredientCharacteristics
                (ingredient.cardBase.cardData.cardName,
                ingredient.tastePoint,
                ingredient.cardCost, ingredient.
                ingredientType, ingredient.cardBase.cardData.cardImage);
                return true;
            }
        }

        return false;
    }
}

[System.Serializable]
public struct IngredientCharacteristics
{
    public string ingredientName;
    public float tastePoint;
    public int cardCost;
    public IngredientType ingredientType;
    public Sprite ingredientSprite;

    public IngredientCharacteristics(string ingredientName, float tastePoint, int cardCost, IngredientType ingredientType, Sprite ingredientSprite)
    {
        this.ingredientName = ingredientName;
        this.tastePoint = tastePoint;
        this.cardCost = cardCost;
        this.ingredientType = ingredientType;
        this.ingredientSprite = ingredientSprite;
    }
}
