using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardCardGame_Base : MonoBehaviour
{
    public CardData cardData { get; private set; }

    [Header("Visual")]
    [SerializeField] TMP_Text cardNameT;
    [SerializeField] TMP_Text cardDescriptionT;
    [SerializeField] Image cardImageS;    
    [SerializeField ]List<Canvas> childCanvases = new List<Canvas>();

    public virtual void Init(Card card)
    {
        RecordCardData(card);
        ApplyCardVisual();
    }

    protected virtual void RecordCardData(Card card)
    {
        cardData = new()
        {
            cardName = card.cardName,
            cardDescription = card.cardDescription,
            cardImage = card.cardImage,
            cardType = card.cardType,
            effects = card.effects,
        };
    }

    private void ApplyCardVisual()
    {
        cardNameT.text = cardData.cardName;
        cardDescriptionT.text = cardData.cardDescription;
        cardImageS.sprite = cardData.cardImage;
    }

    public void ApplySortingLayerToCanvas(int sortingOrder)
    {
        foreach (var canvas in childCanvases)
        {
            canvas.sortingOrder = sortingOrder;
        }
    }


    public virtual void ExecuteEffects()
    {
        foreach(var effect in cardData.effects)
        {
            effect.Execute();
        }
    }
}

public struct CardData
{
    public string cardName;
    public string cardDescription;
    public Sprite cardImage;
    public CardType cardType;
    public List<Effect> effects;
}
