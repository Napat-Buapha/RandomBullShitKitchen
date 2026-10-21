using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    GameManager _gm;

    [field:Header("Text List")]
    [field:SerializeField] public TMP_Text levelT {get; private set;}
    [field:SerializeField] public TMP_Text timePointT {get; private set;}
    [field:SerializeField] public TMP_Text turnT {get; private set;}
    [field:SerializeField] public TMP_Text scoreT {get; private set;}

    [field:Header("Menu")]
    [field:SerializeField] public UI_SelectionMenu selectionMenu {get; private set;}
    [field:SerializeField] public UI_ShopMenu shopMenu {get; private set;}


    [field:Header("Game Conclude")]
    [field:SerializeField] public GameObject winPanel {get; private set;}
    [field:SerializeField] public GameObject lostPanel {get; private set;}

     [field:Header("Canvas Groups")]
    [SerializeField] CanvasGroup cardGameCanvasGroup;
    
    public void Init(GameManager gm)
    {
        _gm = gm;

        selectionMenu.Init();
        shopMenu.Init();
    }

    public void Reset()
    {
        shopMenu.DisableCardShop();
        DisableConcludePanel();
        UnHideCanvas();
    }

    public void UpdateLevelText(int value)
    {
        levelT.text = $"Level: {value}";
    }

    public void UpdateTimePointText(int timePoint , int maxTimePoint)
    {
        timePointT.text = $"Time Point: {timePoint}/{maxTimePoint}";
    }

    public void UpdateTurnText(int currentTurn , int turnLimit)
    {
        turnT.text = $"Turn: {currentTurn}/{turnLimit}";
    }

    public void UpdateScore(int score , int targetScore)
    {
        scoreT.text = $"Score: {score} / {targetScore}";
    }



    public void DisableConcludePanel()
    {
        winPanel.SetActive(false);
        lostPanel.SetActive(false);
    }

    public void HideCanvas()
    {
        cardGameCanvasGroup.alpha = 0;
        cardGameCanvasGroup.blocksRaycasts = false;
    }

    public void UnHideCanvas()
    {
        cardGameCanvasGroup.alpha = 1;
        cardGameCanvasGroup.blocksRaycasts = true;
    }
}
