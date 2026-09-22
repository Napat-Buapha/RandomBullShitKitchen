using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffectManager : MonoBehaviour
{
    GameManager _gm;
    Queue<Effect> _effectQueue;
    public Effect currentEffect {get; private set;}
    bool _isRunningQueue;

    public void Init(GameManager gm)
    {
        _gm = gm;
        _effectQueue = new Queue<Effect>();
    }

    public void AddEffect(Effect effect)
    {
        _effectQueue.Enqueue(effect);
        if (!_isRunningQueue)
        {
            _isRunningQueue = true;
            RunningEffectQueue();
        }
    }

    public void RunningEffectQueue()
    {
        if(_effectQueue.Count <= 0) 
        {
            _isRunningQueue = false;
            return;
        }

        currentEffect = _effectQueue.Dequeue();

        if (currentEffect.effectType == Effect.EffectType.manualResolve)
        {
            currentEffect.Execute();
        }
        else
        {
            ExecuteEffect();
        }
    }

    private void ExecuteEffect()
    {
        currentEffect.Execute();
        RunningEffectQueue();
    }

    public bool ApplyCardDataToEffect(CardCardGame_Base card)
    {
        if (currentEffect is Effect_CardSelect targetEffect)
        {
            if(!CheckSelectionCondition(card)) return false;

            return targetEffect.AddCard(card);
        }

        return false;
    }

    public bool CheckSelectionCondition(CardCardGame_Base card)
    {
        SelectionCondition condition = _gm.currentSelectionCondition;

        if (condition.cardNameList.Count > 0)
        {
            return condition.cardNameList.Contains(card.cardData.cardName);
        }
        
        if(condition.cardTypesList.Count > 0)
        {
            return condition.cardTypesList.Contains(card.cardData.cardType);
        }

        if(condition.ingredientTypesList.Count > 0)
        {
            if (card is CardCardGame_Ingredient ingredientCard)
            {
                return condition.ingredientTypesList.Contains(ingredientCard.ingredientVariable.ingredientType);
            }
            else
            {
                return false;
            }
        }

        return true;
    }
}
