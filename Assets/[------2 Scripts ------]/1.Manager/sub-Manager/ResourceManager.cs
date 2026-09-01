using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    GameManager _gm;

    [SerializeField] public int timePoint;
    [SerializeField] private int maxTimePoint;

    [SerializeField] TMP_Text timePointText;

    public void Init(GameManager gm)
    {
        _gm = gm;
        ResetTimePoint();
    }

    public void ResetTimePoint()
    {
        timePoint = maxTimePoint;
        UpdateTimePointText();
    }

    public bool PayTimePoint(int amount)
    {

        // If time point already below 0, return false to indicate that the player cannot pay the cost.
        if(timePoint < 0)
        {
            return false;
        }

        timePoint -= amount;
        UpdateTimePointText();

        // If time point is below 0 after paying the cost, end the turn.
        if(timePoint <= 0)
        {
            _gm.TurnManager.TurnEnd();
        }

        return true;
    }

    public void UpdateTimePointText()
    {
        timePointText.text = $"Time Point: {timePoint}/{maxTimePoint}";
    }
}   
