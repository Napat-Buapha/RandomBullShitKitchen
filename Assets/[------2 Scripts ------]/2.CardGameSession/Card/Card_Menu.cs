using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Menu Card", menuName = "Card/Menu Card")]
public class Card_Menu : Card
{
    [field: Header("[Food] Card Data Customization")]
    [field: SerializeField] public int priority { get; private set; } = 1;
    [field: SerializeField] public int score { get; private set; }


    
}
