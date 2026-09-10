using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;

public class ServeTableManager : MonoBehaviour
{
    [SerializeField] CardCardGame_Menu cardPrefab;
    public List<CardCardGame_Menu> CardsInTable;
    [SerializeField] Transform serveTable; //จุดกึ่งกลางของ hand

    [Header("Card Spacing Customizer")]
    [SerializeField] quaternion cardRotation;
    [SerializeField] float cardSpacing;


    void Update()
    {
        UpdateHandVisuals();
    }

    public void UpdateHandVisuals()
    {
        int cardCount = CardsInTable.Count;

        if (cardCount == 0) return;

        if (cardCount == 1)
        {
            CardsInTable[0].transform.position = serveTable.position;
            CardsInTable[0].transform.rotation = cardRotation;
            return;
        }

        for (int i = 0; i < cardCount; i++)
        {
            Vector3 worldPos = CalculateCardPosition(i);
            ApplyPositionToCard(i, worldPos);
            ApplyRotationToCard(i);
        }
    }

    private void ApplyPositionToCard(int i, Vector3 worldPos)
    {
        CardsInTable[i].transform.position = new Vector3(worldPos.x, worldPos.y, -i * 0.1f);
        CardsInTable[i].GetComponent<SortingGroup>().sortingOrder = i + 1;
        CardsInTable[i].ApplySortingLayerToCanvas(i + 1);
    }

    private void ApplyRotationToCard(int i)
    {
        CardsInTable[i].transform.rotation = cardRotation;
    }

    private Vector3 CalculateCardPosition(int i)
    {
        // Horizontal offset (Y axis in world)
        float verticalOffset = cardSpacing * i;

        // Final world position (relative to hand center)
        Vector3 worldPos = serveTable.position
                         + -serveTable.up * verticalOffset;
        return worldPos;
    }

    public void AddedMenuCard(Card card, int ingredientsScore)
    {
        // อย่าลืมเปลี่ยนไปใช้ Pool 
        GameObject card_ = Instantiate(cardPrefab.gameObject, serveTable.transform.position, quaternion.identity, serveTable.transform);

        var cardComponent = card_.GetComponent<CardCardGame_Menu>();
        cardComponent.ApplyBaseScore(ingredientsScore);
        cardComponent.Init(card);
        CardsInTable.Add(cardComponent);
        UpdateHandVisuals();

        GameManager.Instance.ScoreManager.CalculateScore(cardComponent, ingredientsScore);
    }
}
