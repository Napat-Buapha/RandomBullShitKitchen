using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Recipe", menuName = "Recipe", order = 0)]
public class Recipe : ScriptableObject 
{
    // String is ID while int is Amout 
    [SerializeField] Dictionary<string , int> specificIngredients = new ();
    [SerializeField] List<IngredientType> positiveIngredients;
    [SerializeField] List<IngredientType> negativeIngredients;
    [SerializeField] int Priority = 1;
    [SerializeField] int ingredientAmout = 1;
    [SerializeField] Card_Menu cardMenu;

    int score;
    Dictionary<string , int> specificIngPool;

    public Card_Menu verify(List<IngredientCharacteristics> ingredients)
    {
        score = 0;
        specificIngPool = specificIngredients;

        foreach(var ingredient in ingredients)
        {
            if(!CheckSpecificIngredients(ingredient.ingredientVariable.ingredientId))
            {
                score += 1;
                continue;
            }

            if(positiveIngredients.Contains(ingredient.ingredientVariable.ingredientType)) score += 1;

            if(negativeIngredients.Contains(ingredient.ingredientVariable.ingredientType)) return null;
        }

        if(score == ingredientAmout) return cardMenu;

        return null;
        
    }

    private bool CheckSpecificIngredients(string ingredientId)
    {
        if(specificIngPool.Count == 0) return false;

        if(specificIngPool.ContainsKey(ingredientId))
        {
            // Delete to prevent scoring with duplicate specific ingredient
            specificIngPool.Remove(ingredientId);
            return true;
        }
        return false;
    }
}



