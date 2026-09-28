using UnityEngine;

[CreateAssetMenu (fileName = "Effect_TastePointIncrease", menuName = "Effect/Default/TastePointIncrease")]
public class Effect_TastePointIncrease : Effect
{
    [Header("Point Increase")]
    
    [SerializeField]int amout = 1;

    public override void Execute()
    {
        if(_card is CardCardGame_Ingredient ingredient)
        {
            ingredient.ModifiedTastePoint(amout);
        }

    }

}
