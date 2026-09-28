using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Effect_CardSelect_TrashBin : Effect_CardSelect
{
    public override void Execute()
    {
        GameManager.Instance.TrashBinManager.EnableDiscardPilePanel(); 
        base.Execute();
    }

    public override void CancleSelected()
    {
        base.CancleSelected();
        GameManager.Instance.TrashBinManager.DisableDiscardPilePanel(); 
    }

}
