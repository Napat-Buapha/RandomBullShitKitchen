using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Effect_Select_Discard", menuName = "Effect/SelectBase/Discard")]
public class Effect_Select_Discard : Effect_CardSelect
{
    [Header("On Resolve Effect")]
    [SerializeField] List<Effect> effectsOnResolve = new List<Effect>();

    public override void ResolveExecute()
    {
        foreach(var card in selectedList)
        {
            GameManager.Instance.HandsManager.Discard(card , true);
        }

        foreach(var effect in effectsOnResolve)
        {
            effect.Init(_card);
            GameManager.Instance.CardEffectManager.AddEffect(effect , false);
        }

        base.ResolveExecute();
    }
}
