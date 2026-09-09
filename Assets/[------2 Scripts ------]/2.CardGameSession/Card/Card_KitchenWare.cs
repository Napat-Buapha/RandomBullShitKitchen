using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KitchenWare Card", menuName = "Card/KitchenWare Card")]
public class Card_KitchenWare : Card
{
    [field: Header("[KitchenWare] Card Data Customization")]
    [field: SerializeField] public KitchenWare_Variable kitchenWare_Variable { get; private set; }
    
}

[System.Serializable]
public struct KitchenWare_Variable
{
    [field: SerializeField] public int ingredientSlot { get; private set; }
    [field: SerializeField] public Sprite kitchenWareActiveSprite { get; private set; }
    [field: SerializeField] public List<Recipe> recipeList { get; private set; }
}
