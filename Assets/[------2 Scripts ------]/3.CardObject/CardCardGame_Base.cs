using System.Collections.Generic;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CardCardGame_Base : MonoBehaviour 
{
    public CardData cardData { get; private set; }
    public Card baseCardRef { get; private set; }
    public UnityEvent<CardCardGame_Base> OnClickEvent { get; private set; }
    protected Collider2D _col;
    [Header("Visual")]
    [SerializeField] TMP_Text cardNameT;
    [SerializeField] TMP_Text cardDescriptionT;
    [SerializeField] Image cardImageS;    
    [SerializeField ]List<Canvas> childCanvases = new List<Canvas>();
    [SerializeField] protected Image selectedHighlight;

    public virtual void Init(Card card)
    {
        OnClickEvent = new UnityEvent<CardCardGame_Base>();
        _col = GetComponent<Collider2D>();
        
        OnDeSelect();
        RecordCardData(card);
        ApplyCardVisual();
    }


    void OnDisable()
    {
        OnClickEvent.RemoveAllListeners();   
    }

    protected virtual void RecordCardData(Card card)
    {
        baseCardRef = card;
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

    public void OnSelect()
    {
        selectedHighlight.enabled = true;
    }

    public void OnDeSelect()
    {
        selectedHighlight.enabled = false;
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
            effect.Init(this);
            GameManager.Instance.CardEffectManager.AddEffect(effect);
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
