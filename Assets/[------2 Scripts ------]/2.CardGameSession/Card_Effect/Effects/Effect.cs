using UnityEngine;

public class Effect : ScriptableObject
{
    public enum EffectType
    {
        autoResolve,
        manualResolve,
    }
 
    protected CardCardGame_Base _card;
    [field:SerializeField] public EffectType effectType {get; private set;}
    
    public virtual void Init(CardCardGame_Base card)
    {
        _card = card;
    }

    public virtual void Execute()
    {
        
    }
}
