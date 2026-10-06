using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Effect_CardSelect : Effect
{
    [SerializeField] protected SelectionCondition selectionCondition;
    [SerializeField] protected GameManager.GameState targetSelectingState;

    [field:SerializeField] public List<CardCardGame_Base> selectedList {get;protected set;}
    [HideInInspector] public UnityEvent<int> OnCardAmoutChange;

    public override void Execute()
    {
        OnCardAmoutChange = new UnityEvent<int>();
        GameManager.Instance.SwitchToSelectionState(selectionCondition , targetSelectingState);
        GameManager.Instance.UiManager.SelectionMenu.resolveButton.onClick.AddListener(ResolveExecute);
        GameManager.Instance.UiManager.SelectionMenu.cancleButton.onClick.AddListener(CancleSelected);
        selectedList = new();
    }

    public virtual void ResolveExecute()
    {
        CancleSelected();
    }

    public virtual void CancleSelected()
    {
        GameManager.Instance.SwitchGameState(GameManager.GameState.NormalMode);
        selectedList.Clear();
        GameManager.Instance.UiManager.SelectionMenu.cancleButton.onClick?.RemoveListener(CancleSelected);
        GameManager.Instance.CardEffectManager.RunningEffectQueue();
    }

    public virtual bool AddCard(CardCardGame_Base card)
    {
        if(selectedList.Contains(card)) 
        {
            RemoveCard(card);
            return false;
        }

        if(selectedList.Count >= GameManager.Instance.currentSelectionCondition.amout)
        {
            return false;
        }

        selectedList.Add(card);
        OnCardAmoutChange.Invoke(selectedList.Count);

        return true;
    }

    public virtual void RemoveCard(CardCardGame_Base card)
    {
        selectedList.Remove(card);
        OnCardAmoutChange.Invoke(selectedList.Count);
    }
}
