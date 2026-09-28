using System.Collections.Generic;
using NUnit.Framework;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class HandsManager : MonoBehaviour
{
    GameManager _gm;

    // Card in hand //
    [SerializeField] Dictionary<CardType, GameObject> cardPrefabs;
    public List<CardCardGame_Base> cardsInHand { get; private set; }
    [SerializeField] Transform hand; //จุดกึ่งกลางของ hand

    [Header("Card Spacing Customizer")]
    [SerializeField] float cardSpacing;

    // Ingredient Selecting //
    [SerializeField] List<CardCardGame_Ingredient> currentSelectedIngredient;


    public void Init(GameManager gm)
    {
        gm.OnGameStateChange.AddListener(OnGameStateChange);

        _gm = gm;
        cardsInHand = new();
        currentSelectedIngredient = new();

        UpdateHandVisuals();
    }
    void Update()
    {
        UpdateHandVisuals();
    }

    #region Hand Virtual Update
    public void UpdateHandVisuals()
    {
        int cardCount = cardsInHand.Count;

        if (cardCount == 0) return;

        if (cardCount == 1)
        {
            cardsInHand[0].transform.position = hand.position;
            cardsInHand[0].transform.rotation = Quaternion.Euler(Vector3.zero);
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            Vector3 worldPos = CalculateCardPosition(cardCount, i);
            ApplyPositionToCard(i, worldPos);

        }
    }

    private void ApplyPositionToCard(int i, Vector3 worldPos)
    {
        cardsInHand[i].transform.position = new Vector3(worldPos.x, worldPos.y, -i * 0.1f);
        cardsInHand[i].GetComponent<SortingGroup>().sortingOrder = i + 1;
        cardsInHand[i].ApplySortingLayerToCanvas(i + 1);
    }

    private Vector3 CalculateCardPosition(int cardCount, int i)
    {
        // Horizontal offset (X axis in world)
        float horizontalOffset = cardSpacing * (i - (cardCount - 1) / 2f);

        // Final world position (relative to hand center)
        Vector3 worldPos = hand.position
                         + hand.right * horizontalOffset;
        return worldPos;
    }
    #endregion
    #region Hand Public Method
    public void AddedCard(Card card)
    {
        // อย่าลืมเปลี่ยนไปใช้ Pool 
        GameObject card_ = Instantiate(cardPrefabs[card.cardType], hand.transform.position, quaternion.identity, hand.transform);

        var cardComponent = card_.GetComponent<CardCardGame_Base>();
        cardComponent.Init(card);
        cardComponent.OnClickEvent.AddListener(CardSelect);
        cardsInHand.Add(cardComponent);
        UpdateHandVisuals();
    }
    public void DiscardHand()
    {
        List<CardCardGame_Base> discardedList = new();

        foreach (var card in cardsInHand)
        {
            //กรณีไม่อยากให้ทิ้งให้สร้างเงื่อนไขเช็คที่นี่

            discardedList.Add(card);
        }

        foreach (var card in discardedList)
        {
            _gm.TrashBinManager.Receive(card.baseCardRef);
            Discard(card);
        }
    }
    public void Discard(CardCardGame_Base card, bool IsSendToTrash = false)
    {

        cardsInHand.Remove(card);

        if (IsSendToTrash)
            GameManager.Instance.TrashBinManager.Receive(card.baseCardRef);
        // อย่าลืมเปลี่ยนไปใช้ Pool 
        Destroy(card.gameObject);

        UpdateHandVisuals();
    }
    #endregion

    public void CardSelect(CardCardGame_Base card)
    {
        if (card is CardCardGame_Ingredient ingredientCard)
        {
            SelectIngredient(ingredientCard);
        }

        if (card is CardCardGame_KitchenWare kitchenWareCard)
        {
            SelectKitchenWare(kitchenWareCard);
        }
    }

    #region IngredientCardEvent
    public void SelectIngredient(CardCardGame_Ingredient ingredient)
    {
        if (currentSelectedIngredient.Contains(ingredient))
        {
            UnSelectIngredient(ingredient);
            return;
        }

        GameManager.Instance.SceneManager.EnableAllAddButtons();
        currentSelectedIngredient.Add(ingredient);
        ingredient.OnSelect();
    }
    public void UnSelectIngredient(CardCardGame_Ingredient ingredient)
    {
        currentSelectedIngredient.Remove(ingredient);
        ingredient.OnDeselect();

        if (currentSelectedIngredient.Count <= 0)
        {
            GameManager.Instance.SceneManager.DisableAllAddButtons();
        }
    }
    public void UnSelectAllIngredient()
    {
        while (currentSelectedIngredient.Count > 0)
        {
            UnSelectIngredient(currentSelectedIngredient[0]);
        }
    }
    public void AddSelectedIngredientToStove(Stove targetStove)
    {
        var unSelectedList = new List<CardCardGame_Ingredient>();

        foreach (var ingredient in currentSelectedIngredient)
        {
            if (targetStove.AddIngredient(ingredient))
            {
                unSelectedList.Add(ingredient);
            }
        }

        foreach (var ingredient in unSelectedList)
        {
            UnSelectIngredient(ingredient);
            Discard(ingredient);
        }
    }
    #endregion
    #region KitchenWareCardEvent
    public void SelectKitchenWare(CardCardGame_KitchenWare card)
    {
        GameManager.Instance.SceneManager.PlaceKitchenWare(card);
    }

    #endregion


    #region CardSelectionStateEvent
    // Card Selecting State
    void OnGameStateChange(GameManager.GameState currentGameState)
    {
        switch (currentGameState)
        {
            case GameManager.GameState.normalMode:
                SetNormal();
                break;

            case GameManager.GameState.SelectingHand:
                SetHandSelect();
                break;

            default:
                SetOther();
                break;
        }
    }
    private void SetNormal()
    {
        foreach (var card in cardsInHand)
        {
            card.OnClickEvent?.RemoveAllListeners();
            card.OnClickEvent.AddListener(CardSelect);
        }
    }
    private void SetHandSelect()
    {
        foreach (var card in cardsInHand)
        {
            card.OnClickEvent?.RemoveAllListeners();
            card.OnClickEvent.AddListener(SelectCardForEffect);
        }
    }

    private void SetOther()
    {
        foreach (var card in cardsInHand)
        {
            card.OnClickEvent?.RemoveAllListeners();

        }
    }

    void SelectCardForEffect(CardCardGame_Base card)
    {
        bool isApply = _gm.CardEffectManager.ApplyCardDataToEffect(card);

        if (isApply)
        {
            card.OnSelect();
        }
        else
        {
            card.OnDeselect();
        }
    }
}
    #endregion
