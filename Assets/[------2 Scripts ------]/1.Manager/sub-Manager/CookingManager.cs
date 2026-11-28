using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class CookingManager : MonoBehaviour
{
    
    Card_Menu currentMenu;
    
    public void Cook(List<Recipe> recipeList ,List<IngredientCharacteristics> ingredients, int totalScore)
    {
        currentMenu = null;
        foreach(var recipe in recipeList)
        {
            var newMenu = recipe.verify(ingredients);
            Debug.Log(newMenu);

            if (newMenu == null) continue;
            Examine(newMenu);
        }


        if(currentMenu == null) 
        {
            Debug.Log("Can't found any menu");
            return;
        }
        GameManager.Instance.ServeTableManager.AddedMenuCard(currentMenu ,totalScore);
    }

    private void Examine(Card_Menu newMenu)
    {
        if (currentMenu == null) currentMenu = newMenu;
        if (currentMenu.menuVariable.priority < newMenu.menuVariable.priority) currentMenu = newMenu;
    }
}
