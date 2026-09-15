using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanManager : MonoBehaviour
{
    private Stack<Card> _discardPile;
    GameManager _gm;

    public void Init(GameManager gm)
    {
        _gm = gm;
        _discardPile = new Stack<Card>();
    }

    public void Receive(Card card)
    {
        _discardPile.Push(card);
    }

    public void OpenDiscardPilePanel()
    {
        
    }
}
