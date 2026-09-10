using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    [Header("Menu Serve")]
    [SerializeField] CardCardGame_Menu cardMenu_Prefab;
    [SerializeField] ServeTableManager serveTable;
    
    Card_Menu currentMenu;
    
    public void Cook(List<Recipe> recipeList ,List<IngredientCharacteristics> ingredients)
    {
        currentMenu = null;
        foreach(var recipe in recipeList)
        {
            
            var newMenu = recipe.verify(ingredients);

            if (newMenu == null) continue;
            Examine(newMenu);
        }

        GameManager.Instance.ServeTableManager.AddedMenuCard(currentMenu , CalculateTastePoint(ingredients));
    }

    private int CalculateTastePoint(List<IngredientCharacteristics> ingredients)
    {
        int score = 0;

        foreach(var ingredient in ingredients)
        {
            score += (int)ingredient.ingredientVariable.tastePoint;
        }

        return score;
    }

    private void Examine(Card_Menu newMenu)
    {
        if (currentMenu == null) currentMenu = newMenu;
        if (currentMenu.menuVariable.priority < newMenu.menuVariable.priority) currentMenu = newMenu;
    }
}
