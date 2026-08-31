using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    [Header("Card Data Customization")]
    [field: SerializeField] public string cardName { get; private set; }
    [field: SerializeField] public string cardDescription { get; private set; }
    [field: SerializeField] public Sprite cardImage { get; private set; }
    [field: SerializeField] public CardType cardType {get; private set; }
    [SerializeField] private List<Effect> effects;    

    public virtual void ExecuteEffects()
    {
        foreach(var effect in effects)
        {
            effect.Execute();
        }
    }
}
public enum CardType
{
    Ingredient,
    Kitchenware,
}
