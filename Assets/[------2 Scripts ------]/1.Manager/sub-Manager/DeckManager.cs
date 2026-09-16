using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    GameManager _gm;
    [SerializeField] List<Card> deckLists;
    [SerializeField] private List<Card> _deck;

    public void Init(GameManager gm)
    {
        _gm = gm;
    }

    public void TurnDeckListActiveDeck()
    {
        _deck = new List<Card>(deckLists);
        Shuffle();
    }

    /// <summary>
    /// if onTop = true card will place on top of deck, 
    /// if onTop = false it will place under the bottom of deck
    /// </summary>
    public void Receive(Card card, bool onTop = true)
    {
        if (onTop)
        {
            _deck.Add(card);
        }
        else
        {
            _deck.Insert(0, card);
        }
    }

    public void Shuffle()
    {
        for (int i = _deck.Count - 1; i > 0; i--)
        {
            int j = UnityEngine.Random.Range(0, i + 1);

            (_deck[i], _deck[j]) = (_deck[j], _deck[i]);
        }
    }


    [ContextMenu("Draw Card")]
    public void TestDraw()
    {
        DrawCard(1);
    }

    public void DrawCard(int amout)
    {
        for (int i = 0; i < amout; i++)
        {
            if (_deck.Count > 0)
            {
                Draw();
            }
            else
            {
                RecycleDeck();
                Draw();
            }
        }
    }

    private void Draw()
    {
        if(_deck.Count == 1)
        {
            _gm.HandsManager.AddedCard(_deck[0]);
            _deck.RemoveAt(0);
            return;
        }

        var card = _deck[_deck.Count - 1];
        _gm.HandsManager.AddedCard(card);
        _deck.RemoveAt(_deck.Count - 1);
    }

    public void RecycleDeck()
    {
        var tcm = _gm.TrashCanManager;

        while (tcm.discardPile.Count > 0)
        {
            Receive(tcm.Remove(tcm.discardPile[0]));
        }

        Shuffle();
    }
}
