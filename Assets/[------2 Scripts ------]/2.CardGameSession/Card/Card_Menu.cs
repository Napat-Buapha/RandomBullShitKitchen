using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Menu Card", menuName = "Card/Menu Card")]
public class Card_Menu : Card
{
    [field: Header("[Food] Card Data Customization")]
    [field: SerializeField] public Menu_Variable menuVariable {get; private set;}
}

[System.Serializable]
public struct Menu_Variable
{
    [field: SerializeField] public int priority { get; private set; }
    [field: SerializeField] public int amout { get; private set; }
    [field: SerializeField] public MathSymbol mathSymbol { get; private set; }
}

public enum MathSymbol
{
    plus,
    minus,
    multiply,
    divide,
    none,
}