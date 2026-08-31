using System.Collections.Generic;
using UnityEngine;

public class Card_KitchenWare : Card
{
    [Header("[KitchenWare] Card Data Customization")]
    [field: SerializeField] public int ingredientSlot { get; private set; }
    [field: SerializeField] public Sprite kitchenWareActiveSprite { get; private set; }
    
}
