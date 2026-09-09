using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    Card_Menu currentMenu;
    
    public Card_Menu Cook(List<Recipe> recipeList ,List<IngredientCharacteristics> ingredients)
    {
        currentMenu = null;
        foreach(var recipe in recipeList)
        {
            
            var newMenu = recipe.verify(ingredients);

            if (newMenu == null) continue;
            Examine(newMenu);
        }

        return currentMenu;
    }

    private void Examine(Card_Menu newMenu)
    {
        if (currentMenu == null) currentMenu = newMenu;
        if (currentMenu.priority < newMenu.priority) currentMenu = newMenu;
    }
}
