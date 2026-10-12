using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stove : MonoBehaviour, IDropReceiver
{
    [Header("UI Ref")]
    //Base Card Data Ref
    Card _baseCardKitchenWareRef;
    public bool isOccupied { get; private set; } = false;
    [SerializeField] SpriteRenderer kitchenWareSpriteRenderer;
    [SerializeField] KitchenWare_Variable kitchenWareVariable;

    IngredientCharacteristics[] ingredientSlots;
    [SerializeField] Stove_IngredientUi slotUi;

    [Header("UI")]
    [SerializeField] GameObject addButton;
    [SerializeField] GameObject placeButton;
    [SerializeField] Button cookButton;
    [SerializeField] TMP_Text totalTastePointT;
    [SerializeField] Image kitchenWareIndicator;
    [SerializeField] Image ingredintIndicator;


    public void Init()
    {
        ResetStove();
        UpdateTotalTastePoint();

        GameManager.Instance.OnGameStateChange.AddListener(OnGameStateChange);
    }

    #region GameState Event
    void OnGameStateChange(GameManager.GameState currentGameState)
    {
        switch (currentGameState)
        {
            case GameManager.GameState.CardHolding:
                ManageIndicator(true);
                break;

            default:
                ManageIndicator(false);
                break;
        }
    }

    void ManageIndicator(bool activeState)
    {
        if (activeState == false)
        {
            DisableIndicator();
        }
        else
        {
            EnableIndicator(GameManager.Instance.HandsManager.holdingCard);
        }
    }

    private void EnableIndicator(CardCardGame_Base card)
    {
        if (card is CardCardGame_KitchenWare && !isOccupied)
        {
            kitchenWareIndicator.enabled = true;
            return;
        }

        if (card is CardCardGame_Ingredient && isOccupied)
        {
            if (string.IsNullOrEmpty(ingredientSlots[ingredientSlots.Length - 1].ingredientName))
                ingredintIndicator.enabled = true;
            return;
        }
    }

    private void DisableIndicator()
    {
        kitchenWareIndicator.enabled = false;
        ingredintIndicator.enabled = false;
    }
    #endregion

    public bool DropReceive(CardCardGame_Base card)
    {
        if (card is CardCardGame_Ingredient ingredientCard)
            return AddIngredient(ingredientCard);

        if (card is CardCardGame_KitchenWare kitchenWareCard)
            return PlaceKitchenWare(kitchenWareCard);

        return false;
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
        SetAddButtonState(false);
        SetCookButtonState(false);
        SetPlaceButtonState(false);

        kitchenWareSpriteRenderer.sprite = null;
        slotUi.DisableIngredientUI();
        UpdateTotalTastePoint();
    }

    public bool PlaceKitchenWare(CardCardGame_KitchenWare kitchenWare)
    {
        if (isOccupied) return false;

        _baseCardKitchenWareRef = kitchenWare.baseCardRef;
        kitchenWareVariable = kitchenWare.kitchenWareVariable;
        ingredientSlots = new IngredientCharacteristics[kitchenWareVariable.ingredientSlot];
        kitchenWareSpriteRenderer.sprite = kitchenWareVariable.kitchenWareActiveSprite;
        slotUi.EnableIngredientUI(kitchenWare);
        isOccupied = true;
        return true;
    }

    public void UpdateTotalTastePoint()
    {
        if (ingredientSlots == null)
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
        if (!isOccupied) return false;

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
            SetCookButtonState(true);
        }
    }

    #region Button Event

    // Place KitchenWare
    public void SetPlaceButtonState(bool activeState)
    {
        placeButton.SetActive(activeState);
    }

    public void PlaceKitchenWareButton()
    {
        GameManager.Instance.HandsManager.PlaceKitchenWareOnStove(this);
    }

    // Add ingredients
    public void SetAddButtonState(bool activeState)
    {
        addButton.SetActive(activeState);
    }

    public void AddIngredientButton()
    {
        GameManager.Instance.HandsManager.AddSelectedIngredientToStove(this);
    }

    // Cooking
    public void SetCookButtonState(bool activeState)
    {
        cookButton.interactable = activeState;
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
