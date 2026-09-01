using System.Collections.Generic;
using UnityEngine;

public class Card : ScriptableObject
{
    [field: Header("Card Data Customization")]
    [field: SerializeField] public string cardName { get; private set; }
    [field: SerializeField] public string cardDescription { get; private set; }
    [field: SerializeField] public Sprite cardImage { get; private set; }
    [field: SerializeField] public CardType cardType {get; private set; }
    [field: SerializeField] public List<Effect> effects {get; private set; } 

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
    KitchenWare,
}
