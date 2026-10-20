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
    [SerializeField] List<Transform> cardSpawnsPos;
    [SerializeField] List<CardCardGame_Base> cardInShop;
    [SerializeField] List<Card> cardList;

    public List<CardWeight> cardPool { get; private set; }

    [SerializeField] int maxSelectionAmout = 1;
    int selectionAmout ;
    [SerializeField] TMP_Text selectionAmoutT;

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
        selectionAmout = maxSelectionAmout;
        UpdateSelectionAmoutText();

        // Random Card In Shop 
        foreach(var spawnPos in cardSpawnsPos)
        {
            SpawnCards(WeightedRandom(cardPool) , spawnPos);
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

    public void SpawnCards(Card card, Transform spawnPos)
    {
        // อย่าลืมเปลี่ยนไปใช้ Pool 
        GameObject card_ = Instantiate(cardPrefabs[card.cardType], spawnPos.position, quaternion.identity, spawnPos);

        var cardComponent = card_.GetComponent<CardCardGame_Base>();
        cardComponent.Init(card);
        cardInShop.Add(cardComponent);

        var uiComponent = card_.GetComponent<CardCanvas_Event>();
        uiComponent.OnClickEvent.AddListener(ChooseCard);
    }

    public void ChooseCard(CardCardGame_Base card)
    {
        if (selectionAmout <= 0) return;

        RemoveCard(card);
        selectionAmout--;
        UpdateSelectionAmoutText();
        GameManager.Instance.DeckManager.AddCardToDeckList(card.baseCardRef);

        /*if(selectionAmout <= 0)
        {
            GameManager.Instance.UiManager.shopMenu.DisableCardShop();
            GameManager.Instance.UiManager.shopMenu.EndTrasaction();
        }*/
    }

    private void UpdateSelectionAmoutText()
    {
        selectionAmoutT.text = "Selection Remain: " + selectionAmout;
    }

    public void ClearShop()
    {
        while(cardInShop.Count > 0)
        {
            RemoveCard(cardInShop[0]);
        }
    }

    private void RemoveCard(CardCardGame_Base card)
    {   
        if(!cardInShop.Contains(card)) return;

        cardInShop.Remove(card);
        var cardUI = card.GetComponent<CardCanvas_Event>();
        cardUI.OnClickEvent.RemoveListener(ChooseCard);

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
