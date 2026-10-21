using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private int currentTurn;
    [SerializeField] private int turnLimit = 5;

    [Header("On Turn Reset Customizer")]
    [SerializeField] int startingHand = 5;

    GameManager _gm;
    public void Init(GameManager gm)
    {
        _gm = gm;
        _gm.DeckManager.TurnDeckListActiveDeck();
        StartNewTurn();
    }

    public void TurnEnd()
    {
        _gm.HandsManager.DiscardHand();
        StartNewTurn();
    }

    public void StartNewTurn()
    {
        currentTurn++;
        UpdateTurnText();

        if (currentTurn > turnLimit)
        {
            _gm.SceneManager.GameConclude(false);
            return;
        }

        _gm.DeckManager.DrawCard(startingHand);
        _gm.ResourceManager.ResetTimePoint();
    }
    
    void UpdateTurnText()
    {
        _gm.UiManager.UpdateTurnText(currentTurn,turnLimit);
    }

    public void Reset()
    {
        currentTurn = 0;
        StartNewTurn();
    }
}   
