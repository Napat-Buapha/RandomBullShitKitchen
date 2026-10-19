using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    GameManager _gm;

    [field:Header("Text List")]
    [field:SerializeField] public TMP_Text levelT {get; private set;}

    [field:Header("Menu")]
    [field:SerializeField] public UI_SelectionMenu selectionMenu {get; private set;}


    [field:Header("Game Conclude")]
    [field:SerializeField] public GameObject winPanel {get; private set;}
    [field:SerializeField] public GameObject lostPanel {get; private set;}

     [field:Header("Canvas Groups")]
    [SerializeField] CanvasGroup cardGameCanvasGroup;
    
    public void Init(GameManager gm)
    {
        _gm = gm;

        selectionMenu.Init();
    }

    public void UpdateLevelText(int value)
    {
        levelT.text = $"Level: {value}";
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
