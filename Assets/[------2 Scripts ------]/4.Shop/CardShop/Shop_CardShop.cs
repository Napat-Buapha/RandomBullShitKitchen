using System;
using System.Collections.Generic;
using NUnit.Framework;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class Shop_CardShop : MonoBehaviour
{
    [SerializeField] Dictionary<CardType, GameObject> cardPrefabs;
    [SerializeField] List<Shop_CardSlot> cardSlots;
    [SerializeField] List<CardCardGame_Base> cardInShop;
    [SerializeField] List<Card> cardList;

    public List<CardWeight> cardPool { get; private set; }



    public void Init()
    {
        cardInShop = new List<CardCardGame_Base>();
        cardPool = new List<CardWeight>();

        SeUpPool();

    }

    private void SeUpPool()
    {
        foreach (var card_ in cardList)
        {
            CardWeight cardWeight = new CardWeight
            {
                card = card_,
                weight = 10,
            };
            cardPool.Add(cardWeight);
        }
    }
    public void SetUpShop()
    {
        // Random Card In Shop 
        foreach (var cardSlot in cardSlots)
        {
            cardSlot.Init(this);
            Card spawnedCard = SpawnCards(WeightedRandom(cardPool), cardSlot);
            cardSlot.SetPriceTag(spawnedCard.cardPirce);
        }
    }

    public Card WeightedRandom(List<CardWeight> cardWeights)
    {
        float totalWeight = 0f;

        foreach (CardWeight card in cardWeights)
        {
            totalWeight += card.weight;
        }

        float randomValue = UnityEngine.Random.Range(0f, totalWeight);

        foreach (CardWeight card in cardWeights)
        {
            randomValue -= card.weight;

            if (randomValue < 0f)
            {
                return card.card;
            }
        }

        Debug.Log("Error random value is " + randomValue);
        return null;
    }

    public Card SpawnCards(Card card, Shop_CardSlot cardSlot)
    {
        // อย่าลืมเปลี่ยนไปใช้ Pool 
        GameObject card_ = Instantiate(cardPrefabs[card.cardType], cardSlot.transform.position, quaternion.identity, cardSlot.transform);

        var cardComponent = card_.GetComponent<CardCardGame_Base>();
        cardComponent.Init(card);
        cardInShop.Add(cardComponent);

        var uiComponent = card_.GetComponent<CardCanvas_Event>();
        uiComponent.OnClickEvent.AddListener(cardSlot.Buy);

        return cardComponent.baseCardRef;
    }

    public void ClearShop()
    {
        while (cardInShop.Count > 0)
        {
            RemoveCard(cardInShop[0]);
        }

    }

    public void RemoveCard(CardCardGame_Base card)
    {
        if (!cardInShop.Contains(card)) return;
        cardInShop.Remove(card);

        //อย่าลืมเปลี่ยนเป็นส่งกลับ pool
        Destroy(card.gameObject);
    }
}

[System.Serializable]
public struct CardWeight
{
    public Card card;
    public float weight;
}
