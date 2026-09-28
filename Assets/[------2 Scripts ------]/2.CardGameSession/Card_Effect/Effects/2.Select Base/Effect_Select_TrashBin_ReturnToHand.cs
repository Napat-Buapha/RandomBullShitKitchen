using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Effect_Select_TrashBin_ReturnToHand", menuName = "Effect/SelectBase/TrashBin_ReturnToHand")]
public class Effect_Select_TrashBin_ReturnToHand : Effect_CardSelect_TrashBin
{

    public override void ResolveExecute()
    {
        foreach(var card in selectedList)
        {
            GameManager.Instance.HandsManager.AddedCard(GameManager.Instance.TrashBinManager.Remove(card));
        }

        base.ResolveExecute();
    }
}
