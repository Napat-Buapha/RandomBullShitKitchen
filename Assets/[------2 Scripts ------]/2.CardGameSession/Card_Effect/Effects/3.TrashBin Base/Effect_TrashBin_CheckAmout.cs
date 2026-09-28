using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_TrashBin_CheckAmout", menuName = "Effect/TrashBin/CheckAmout")]
public class Effect_TrashBin_CheckAmout : Effect
{
    [Header("Condition")]
    [SerializeField] int cardNeed;

    [Header("On Condition Meet Effect")]
    [SerializeField] List<Effect> effectsOnResolve = new List<Effect>();

    public override void Execute()
    {
        if (GameManager.Instance.TrashBinManager.discardPile.Count >= cardNeed)
        {
            ResolveEffects();
        }
    }

    private void ResolveEffects()
    {
        foreach (var effect in effectsOnResolve)
        {
            effect.Init(_card);
            GameManager.Instance.CardEffectManager.AddEffect(effect);
        }
    }
}
