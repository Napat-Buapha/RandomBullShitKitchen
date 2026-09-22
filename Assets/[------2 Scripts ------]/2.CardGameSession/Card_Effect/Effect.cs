using UnityEngine;

public class Effect : ScriptableObject
{
    public enum EffectType
    {
        autoResolve,
        manualResolve,
    }

    [field:SerializeField] public EffectType effectType {get; private set;}

    public virtual void Execute()
    {
        
    }
}
