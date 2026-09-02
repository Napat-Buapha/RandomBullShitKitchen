using System.Linq;
using UnityEngine;

public class Stove : MonoBehaviour
{

    [SerializeField] Stove_IngredientUi slotUi;
    [SerializeField] SpriteRenderer kitchenWareSpriteRenderer;
    [SerializeField] IngredientCharacteristics[] slots;
    [SerializeField] GameObject addButton;


    [field:SerializeField] public bool isOccupied { get; private set; } = false;

    void Start()
    {
        isOccupied = false;
        DisableButton();
        slotUi.DisableIngredientUI();
    }
    public void PlaceKitchenWare(CardCardGame_KitchenWare kitchenWare)
    {
        slots = new IngredientCharacteristics[kitchenWare.ingredientSlot];
        kitchenWareSpriteRenderer.sprite = kitchenWare.kitchenWareActiveSprite;
        slotUi.EnableIngredientUI(kitchenWare);

        isOccupied = true;
        GameManager.Instance.HandsManager.UnSelectAllIngredient();
    }

#region Add Ingredient Button 
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
#endregion

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

                slotUi.UpdateSlots(slots);
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
