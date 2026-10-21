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
    [field:SerializeField] public int cardPirce {get; private set; }  = 15;
}
public enum CardType
{
    Ingredient,
    KitchenWare,
    Menu,
}
