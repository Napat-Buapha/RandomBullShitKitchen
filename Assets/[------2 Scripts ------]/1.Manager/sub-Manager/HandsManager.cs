using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class HandsManager : MonoBehaviour
{
    GameManager _gm;

    // Card in hand //
    [SerializeField] GameObject cardPrefab;
    public List<CardCardGame> CardsInHand;
    [SerializeField] Transform hand; //จุดกึ่งกลางของ hand

    [Header("Card Spacing Customizer")]
    [SerializeField] float cardSpacing;

    public void Init(GameManager gm)
    {
        _gm = gm;
        CardsInHand = new();
        UpdateHandVisuals();
    }
    void Update()
    {
        UpdateHandVisuals();
    }

    #region Hand Virtual Update
    public void UpdateHandVisuals()
    {
        int cardCount = CardsInHand.Count;

        if (cardCount == 0) return;

        if (cardCount == 1)
        {
            CardsInHand[0].transform.position = hand.position;
            CardsInHand[0].transform.rotation = Quaternion.Euler(Vector3.zero);
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
        CardsInHand[i].transform.position = new Vector3(worldPos.x, worldPos.y, -i * 0.1f);
        CardsInHand[i].GetComponent<SortingGroup>().sortingOrder = i + 1;
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
        GameObject card_ = Instantiate(cardPrefab, hand.transform.position, quaternion.identity, hand.transform);

        var cardComponent = card_.GetComponent<CardCardGame>();
        cardComponent.Init(card);
        CardsInHand.Add(cardComponent);
        UpdateHandVisuals();
    }

    public void DiscardHand()
    {
        List<CardCardGame> discardedList = new();

        foreach(var card in CardsInHand)
        {
            //กรณีไม่อยากให้ทิ้งให้สร้างเงื่อนไขเช็คที่นี่

            discardedList.Add(card);
        }

        foreach(var card in discardedList)
        {
            Discard(card);
        }
    }
    public void Discard(CardCardGame card)
    {
        CardsInHand.Remove(card);
        // อย่าลืมเปลี่ยนไปใช้ Pool 
        Destroy(card.gameObject);

        UpdateHandVisuals();
    }
    #endregion
}
