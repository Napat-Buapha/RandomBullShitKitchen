using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;


[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe", order = 0)]
public class Recipe : ScriptableObject
{
    // String is ID while int is Amout 
    [SerializeField] Dictionary<string, int> specificIngredients;
    [SerializeField] List<IngredientType> positiveIngredients;
    [SerializeField] List<IngredientType> negativeIngredients;
    [SerializeField] int ingredientAmout = 1;
    [SerializeField] Card_Menu cardMenu;

    int score;
    Dictionary<string, int> specificIngPool;

    public Card_Menu verify(List<IngredientCharacteristics> ingredients)
    {
        score = 0;
        List<IngredientCharacteristics> ingredients_ = new(ingredients);
        specificIngPool = new(specificIngredients);

        score += CheckSpecificIngredients(ingredients_);



        foreach (var ingredient in ingredients_)
        {
            if (positiveIngredients.Contains(ingredient.ingredientVariable.ingredientType)) score += 1;

            if (negativeIngredients.Contains(ingredient.ingredientVariable.ingredientType)) return null;
        }

        Debug.Log(cardMenu.cardName + score);

        if (score == ingredientAmout) return cardMenu;

        return null;

    }

    private int CheckSpecificIngredients(List<IngredientCharacteristics> ingredients)
    {
        int specificIngredientAmout = 0;
        List<IngredientCharacteristics> toRemove = new();

        foreach (var ing in ingredients)
        {
            if (ing.baseCard is Card_Ingredient ingCard)
            {
                if (specificIngPool.ContainsKey(ingCard.ingredientVariable.ingredientId))
                {
                    Debug.Log("Contain");
                    specificIngredientAmout++;
                    specificIngPool.Remove(ingCard.ingredientVariable.ingredientId);
                    toRemove.Add(ing);
                }
            }
        }

        foreach (var ing in toRemove)
        {
            if (ingredients.Contains(ing))
            {
                ingredients.Remove(ing);
            }
        }

        Debug.Log(cardMenu.cardName + specificIngPool.Count);
        return specificIngredientAmout - specificIngPool.Count;
    }
}



