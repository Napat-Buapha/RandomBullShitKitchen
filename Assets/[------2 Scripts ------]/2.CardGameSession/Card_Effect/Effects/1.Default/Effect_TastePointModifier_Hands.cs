using UnityEngine;

[CreateAssetMenu (fileName = "Effect_TastePointModifier_Hands", menuName = "Effect/Default/TastePointModifier_Hands")]
public class Effect_TastePointModifier_Hands : Effect_TastePointModifier
{
    public override void Execute()
    {
        foreach(var card in GameManager.Instance.HandsManager.cardsInHand)
        {
            ModifiedTastePoint(card);
        }

    }

}
