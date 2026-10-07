using System.Collections.Generic;
using NUnit.Framework;
using Unity.Burst.Intrinsics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
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

    // Card Selecting //
    [SerializeField] List<CardCardGame_Ingredient> currentSelectingIngredient;
    [SerializeField] CardCardGame_KitchenWare currentSeletingKitchenWare;

    public void Init(GameManager gm)
    {
        gm.OnGameStateChange.AddListener(OnGameStateChange);

        _gm = gm;
        cardsInHand = new();
        currentSelectingIngredient = new();

        UpdateHandVisuals();
    }
    void Update()
    {
        if(_gm.currentGameState == GameManager.GameState.NormalMode)
        UpdateHandVisuals();
        
        if(_gm.currentGameState == GameManager.GameState.CardHolding)
        CardDrag();
    }

    
    #region Card Selecting State
        void OnGameStateChange(GameManager.GameState currentGameState)
        {
            switch (currentGameState)
            {
                case GameManager.GameState.NormalMode:
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
    #endregion
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
        cardsInHand[i].transform.position = new Vector3(worldPos.x, worldPos.y, -i * 0.01f);
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
    }

    public void DiscardHand()
    {
        List<CardCardGame_Base> discardedList = new();
        UnSelectAllCard();

        foreach (var card in cardsInHand)
        {
            //กรณีไม่อยากให้ทิ้งให้สร้างเงื่อนไขเช็คที่นี่

            discardedList.Add(card);
        }

        foreach (var card in discardedList)
        {
            Discard(card, true);
        }
    }
    public void Discard(CardCardGame_Base card, bool IsSendToTrash = false)
    {

        cardsInHand.Remove(card);

        if (IsSendToTrash)
            GameManager.Instance.TrashBinManager.Receive(card.baseCardRef);
        // อย่าลืมเปลี่ยนไปใช้ Pool 
        Destroy(card.gameObject);
    }

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

    public void UnSelectAllCard()
    {
        while (currentSelectingIngredient.Count > 0)
        {
            UnSelectIngredient(currentSelectingIngredient[0]);
        }

        UnselectKitchenWare();


    }
    #endregion
    #region IngredientCardEvent
    public void SelectIngredient(CardCardGame_Ingredient ingredient)
    {
        if(currentSeletingKitchenWare != null) UnselectKitchenWare();

        if (currentSelectingIngredient.Contains(ingredient))
        {
            UnSelectIngredient(ingredient);
            return;
        }

        GameManager.Instance.SceneManager.EnableAllAddButtons();
        currentSelectingIngredient.Add(ingredient);
        ingredient.OnSelect();
    }
    public void UnSelectIngredient(CardCardGame_Ingredient ingredient)
    {
        currentSelectingIngredient.Remove(ingredient);
        ingredient.OnDeSelect();

        if (currentSelectingIngredient.Count <= 0)
        {
            GameManager.Instance.SceneManager.DisableAllAddButtons();
        }
    }
    public void AddSelectedIngredientToStove(Stove targetStove)
    {
        var unSelectedList = new List<CardCardGame_Ingredient>();

        foreach (var ingredient in currentSelectingIngredient)
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
    public void SelectKitchenWare(CardCardGame_KitchenWare kitchenWare)
    {
        if(currentSeletingKitchenWare == kitchenWare)
        {
            UnselectKitchenWare();
            return;
        }

        UnSelectAllCard();
        currentSeletingKitchenWare = kitchenWare;
        kitchenWare.OnSelect();

        GameManager.Instance.SceneManager.EnableAllPlaceButton();
    }

    public void UnselectKitchenWare()
    {
        if(currentSeletingKitchenWare == null) return;

        currentSeletingKitchenWare.OnDeSelect();
        currentSeletingKitchenWare = null;

        GameManager.Instance.SceneManager.DisableAllPlaceButton();
    }

    public void PlaceKitchenWareOnStove(Stove targetStove)
    {
        targetStove.PlaceKitchenWare(currentSeletingKitchenWare);
        GameManager.Instance.SceneManager.DisableAllPlaceButton();
        Discard(currentSeletingKitchenWare);

    }

    #endregion
    #region CardSelectionStateEvent
    void SelectCardForEffect(CardCardGame_Base card)
    {
        bool isApply = _gm.CardEffectManager.ApplyCardDataToEffect(card);

        if (isApply)
        {
            card.OnSelect();
        }
        else
        {
            card.OnDeSelect();
        }
    }
    #endregion
    #region CardHolding
        public CardCardGame_Base holdingCard {get;private set;}
    
        public void Holding(CardCardGame_Base card)
        {
            if(holdingCard != null) return;
    
            UnSelectAllCard();
    
            holdingCard = card;
            GameManager.Instance.SwitchGameState(GameManager.GameState.CardHolding);
    
            holdingCard.GetComponent<SortingGroup>().sortingOrder = 100;
    
            //cardsInHand.Remove(card); //เอาออกจาก List เพื่อกันไม่ให้ถูกเรียงโดยอัตโนมัติ
        }
    
        public void CardDrag()
        {
            if(holdingCard != null)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); 
                holdingCard.gameObject.transform.position = new Vector3(mousePos.x , mousePos.y , 0);
            }
        }
    
        public void Drop(CardCardGame_Base card)
        {
            holdingCard = null;
            GameManager.Instance.SwitchGameState(GameManager.GameState.NormalMode);
        }
    #endregion
}

