using System.Collections.Generic;
using UnityEngine;

public class Card_Ingredient : Card
{
    [Header("[Ingredient] Card Data Customization")]
    [field: SerializeField] public float tastePoint { get; private set; }
    [field: SerializeField] public int cardCost { get; private set; }
    
}
