using UnityEngine;

[CreateAssetMenu (fileName = "Effect_TastePointModifier", menuName = "Effect/Default/TastePointModifier")]
public class Effect_TastePointModifier : Effect
{
    [Header("Point Modifier")]
    
    [SerializeField] protected MathSymbol symbol;
    [SerializeField] protected int amout = 1;
    

    public override void Execute()
    {
        ModifiedTastePoint(_card);
    }

    protected void ModifiedTastePoint(CardCardGame_Base card)
    {
        if (card is CardCardGame_Ingredient ingredient)
        {
            ingredient.ModifiedTastePoint(amout, symbol);
        }
    }
}
