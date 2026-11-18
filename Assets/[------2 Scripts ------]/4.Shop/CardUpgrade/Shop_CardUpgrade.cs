using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Shop_CardUpgrade : MonoBehaviour
{
    DeckManager _dm;
    [SerializeField] Shop_UpgradeSlot upgradeSlot;

    [SerializeField] Dictionary<CardType, GameObject> cardPrefabs;
    [SerializeField] int randomSelectAmout;

    [Header("Hand")]
    [SerializeField] private RectTransform hand;
    [SerializeField] private float cardSpacing = 100f;
    [SerializeField] private float speed = 10f;

    [SerializeField] List<Card> _upgradeAbleCard;
    [SerializeField] List<CardCardGame_Base> cardInShop;


    public void Init()
    {
        _dm = GameManager.Instance.DeckManager;
        cardInShop = new List<CardCardGame_Base>();
        _upgradeAbleCard = new List<Card>();

        upgradeSlot.Init(this);
    }

    public void SetUpShop()
    {
        GetRandomCardFromDeck();
    }

    public void ClearShop()
    {
        upgradeSlot.OnShopDisable();
        ReturnCardToDeck();
        ClearHand();
    }

    public void Update()
    {
        UpdateHandVisuals();
    }

    #region SetUp
    public void GetRandomCardFromDeck()
        {
            for (int i = 0; i < randomSelectAmout; i++)
            {
                if (_dm.deckLists.Count != 0)
                {
                    AddCardToList(_dm);
                }
                else
                {
                    break;
                }
            }
        }
    
        private void AddCardToList(DeckManager dm)
        {
            int r = UnityEngine.Random.Range(0, dm.deckLists.Count);
    
            ReceiveNewCard(dm.deckLists[r]);
            dm.RemoveCardFromDeckList(dm.deckLists[r]);
        }
    
        public Card SpawnCards(Card card)
        {
            // อย่าลืมเปลี่ยนไปใช้ Pool 
            // hand มี Layout Group อยู่แล้ว ไม่จำเป็นต้องจัดตำแหน่งใหม่
            GameObject card_ = Instantiate(cardPrefabs[card.cardType], hand.transform);
    
            var cardComponent = card_.GetComponent<CardCardGame_Base>();
            cardComponent.Init(card);
            cardInShop.Add(cardComponent);
    
            var uiComponent = card_.GetComponent<CardCanvas_DragAndDrop>();
            uiComponent.OnDragEndEvent.AddListener(ReturnCardToHand);
    
            return cardComponent.baseCardRef;
        }
    #endregion
    
    public void ReceiveNewCard(Card card)
    {
        _upgradeAbleCard.Add(card);
        SpawnCards(card);
    }
    public void RemoveCardFromList(CardCardGame_Base card)
    {
        if(cardInShop.Contains(card))
        {
            _upgradeAbleCard.Remove(card.baseCardRef);
            cardInShop.Remove(card);
        }
    }

    public void ReturnCardToDeck()
    {
        foreach(var card in _upgradeAbleCard)
        {
            _dm.AddCardToDeckList(card);
        }

        _upgradeAbleCard = new List<Card>();
    }

    public void ReturnCardToHand(CardCardGame_Base card)
    {
        if(cardInShop.Contains(card)) return;
        cardInShop.Add(card);
        _upgradeAbleCard.Add(card.baseCardRef);

        if(upgradeSlot.cardInSlot != null)
        upgradeSlot.RemoveCardFromSlot();
    }

    public void ClearHand()
    {
        while(cardInShop.Count > 0)
        {
            Destroy(cardInShop[0].gameObject);
            cardInShop.RemoveAt(0);
            Debug.Log("Destroy");
        }

        cardInShop = new List<CardCardGame_Base>();
    }

    #region Hand Visual
    public void UpdateHandVisuals()
    {
        int cardCount = cardInShop.Count;

        if (cardCount == 0) return;

        if (cardCount == 1)
        {
            RectTransform card = cardInShop[0].GetComponent<RectTransform>();

            card.anchoredPosition = Vector2.zero;
            card.localRotation = Quaternion.identity;
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            Vector2 targetPos = CalculateCardPosition(cardCount, i);

            ApplyPositionToCard(i, targetPos);
        }
    }

    private void ApplyPositionToCard(int i, Vector2 targetPos)
    {
        //กันไม่ให้ snap กลับที่เดิมตอนกำลัง Drag การ์ด
        if (cardInShop[i].gameObject.TryGetComponent(out CardCanvas_DragAndDrop drag))
        {
            if(drag.isDragging) return;
        }

        RectTransform card = cardInShop[i].GetComponent<RectTransform>();

        card.anchoredPosition = Vector2.Lerp(
            card.anchoredPosition,
            targetPos,
            speed * Time.deltaTime
        );

        cardInShop[i].GetComponent<SortingGroup>().sortingOrder = i + 1;
        cardInShop[i].ApplySortingLayerToCanvas(i + 1);
    }

    private Vector2 CalculateCardPosition(int cardCount, int i)
    {
        float horizontalOffset = cardSpacing * (i - (cardCount - 1) / 2f);

        return new Vector2(horizontalOffset, 0f);
    }
    #endregion

}
