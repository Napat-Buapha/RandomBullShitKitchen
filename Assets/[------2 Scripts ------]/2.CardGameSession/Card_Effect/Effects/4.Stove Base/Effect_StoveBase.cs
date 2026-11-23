using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class Effect_StoveBase : Effect
{
    protected Stove _targetStove;

    public override void Init(CardCardGame_Base card)
    {
        base.Init(card);
        _targetStove = null;
    }

    public void RegisterStoveRefference(Stove stove)
    {
        _targetStove = stove;
    }

}
