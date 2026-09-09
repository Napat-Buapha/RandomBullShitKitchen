using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ingredient Card", menuName = "Card/Ingredient Card")]
public class Card_Ingredient : Card
{
    [field: Header("[Ingredient] Card Data Customization")]
    [field: SerializeField] public Ingredient_Variable ingredientVariable { get; private set; }

    
}

[System.Serializable]
public struct Ingredient_Variable
{
    [field: SerializeField] public string ingredientId { get; private set; }
    [field: SerializeField] public float tastePoint { get; private set; }
    [field: SerializeField] public int cardCost { get; private set; }
    [field: SerializeField] public IngredientType ingredientType { get; private set; }
}

public enum IngredientType
{
    Vegetable,
    Meat,
    Spice,
    Dairy,
    Grain,
    Fruit,
    Seafood,
    Other
}
