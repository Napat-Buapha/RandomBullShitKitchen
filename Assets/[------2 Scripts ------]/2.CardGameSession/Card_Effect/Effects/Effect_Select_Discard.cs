using UnityEngine;

[CreateAssetMenu (fileName = "Effect_Select_Discard", menuName = "Effect/Select_Discard")]
public class Effect_Select_Discard : Effect_CardSelect
{
    public override void ResolveExecute()
    {
        foreach(var card in selectedList)
        {
            GameManager.Instance.HandsManager.Discard(card);
        }

        base.ResolveExecute();
    }
}
