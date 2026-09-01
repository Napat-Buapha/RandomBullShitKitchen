using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    GameManager _gm;
    [SerializeField] List<Card> deckLists;
    private Stack<Card> deck;

    public void Init(GameManager gm)
    {
        _gm = gm;
        TurnDeckListToStack();
    }

    public void TurnDeckListToStack()
    {
        deck = new Stack<Card>(deckLists);
        Shuffle(deck);
    }

    public void Shuffle<T>(Stack<T> stack)
    {
        List<T> list = stack.ToList();

        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (list[i], list[j]) = (list[j], list[i]);
        }

        stack.Clear();

        // Reverse เพื่อให้ลำดับ Top ของ Stack ตรงกับลำดับที่ Shuffle
        for (int i = list.Count - 1; i >= 0; i--)
        {
            stack.Push(list[i]);
        }
    }

    public void DrawCard(int amout)
    {
        for (int i = 0; i < amout; i++)
        {
            if (deck.Count > 0)
            {
                var card = deck.Pop();
                _gm.HandsManager.AddedCard(card);
            }
            else
            {
                // On Deck Out
            }
        }
    }
}
