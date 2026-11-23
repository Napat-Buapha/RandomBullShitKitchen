using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_StoveBase_BonusModifier", menuName = "Effect/StoveBase/BonusModifier")]
public class Effect_StoveBase_BonusModifier:Effect_StoveBase
{
    [Header("Bomus Modifier")]
    
    [SerializeField] protected MathSymbol symbol;
    [SerializeField] protected float amout = 1;

    public override void Execute()
    {
        _targetStove.ModifyBonusStats(amout,symbol);
    }

}
