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
    [field: SerializeField] public IngredientType ingredientType { get; private set; }
    public float tastePoint;
    public int cardCost;
}

public enum IngredientType
{
    Vegetable = 0,
    Protein = 1,
    Dairy = 2,
    Grain = 3,
}
