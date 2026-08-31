using Unity.VisualScripting;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
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
        _gm.DeckManager.DrawCard(startingHand);
        _gm.SceneManager.ResetTimePoint();
    }
}   
