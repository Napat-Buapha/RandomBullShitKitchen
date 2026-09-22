using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SelectionCondition
{
    public bool needToBeFull;
    public int amout;
    public List<string> cardNameList;
    public List<CardType> cardTypesList;
    public List<IngredientType> ingredientTypesList;
}

