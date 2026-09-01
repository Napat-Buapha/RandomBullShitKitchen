using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    [SerializeField] private int currentTurn;
    [SerializeField] private int TurnLimit;

    [SerializeField] TMP_Text turnText;

    [Header("On Turn Reset Customizer")]
    [SerializeField] int startingHand = 5;

    GameManager _gm;
    public void Init(GameManager gm)
    {
        _gm = gm;
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

        if (currentTurn > TurnLimit)
        {
            Debug.Log("Game Over");
            return;
        }

        _gm.DeckManager.TurnDeckListToStack();
        _gm.DeckManager.DrawCard(startingHand);
        _gm.ResourceManager.ResetTimePoint();
    }
    
    void UpdateTurnText()
    {
        turnText.text = $"Turn: {currentTurn}/{TurnLimit}";
    }
}   
