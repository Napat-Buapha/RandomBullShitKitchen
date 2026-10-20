using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    GameManager _gm;

    [Header("Stove Customizer")]
    [SerializeField] GameObject stovePrefab;
    [SerializeField, Range(1, 3)] int startStoveCount;
    [SerializeField] List<Transform> stovePos = new List<Transform>(4);
    List<Stove> _stoves;


    public void Init(GameManager gm)
    {
        _gm = gm;
        GenerateStoves();
        _gm.UiManager.DisableConcludePanel();
    }

    #region Stove
    public void GenerateStoves()
    {
        _stoves = new List<Stove>(4);

        for (int i = 0; i < startStoveCount; i++)
        {
            var stoveObject = Instantiate(stovePrefab, stovePos[i].position, Quaternion.identity, stovePos[i]);

            if (stoveObject.TryGetComponent(out Stove stoveComponent))
            {
                stoveComponent.Init();
                _stoves.Add(stoveComponent);
            }
        }
    }

    public void Reset()
    {
        ResetStove();
    }

    private void ResetStove()
    {
        foreach (var stove in _stoves)
        {
            Destroy(stove.gameObject);
        }
        _stoves.Clear();

        GenerateStoves();
    }

    public void EnableAllPlaceButton()
    {
        foreach (Stove stove in _stoves)
        {
            if (!stove.isOccupied)
            {
                stove.SetPlaceButtonState(true);
            }
        }
    }

    public void DisableAllPlaceButton()
    {
        foreach (Stove stove in _stoves)
        {
            stove.SetPlaceButtonState(false);
        }
    }

    public void EnableAllAddButtons()
    {
        foreach (Stove stove in _stoves)
        {
            if (stove.isOccupied)
                stove.SetAddButtonState(true);
        }
    }

    public void DisableAllAddButtons()
    {
        foreach (Stove stove in _stoves)
        {
            stove.SetAddButtonState(false);
        }
    }

    #endregion
    #region  Game Conclude
    public void GameConclude(bool isWin)
    {
        _gm.UiManager.HideCanvas();
        _gm.PointerManager.CanInteract = false;

        if (isWin)
        {
            _gm.UiManager.shopMenu.EnableCardShop();
        }
        else
        {
            _gm.UiManager.lostPanel.SetActive(true);
        }
    }

    #region Button Event
    public void Quit()
    {
        Application.Quit();
    }
    public void NextLevel()
    {
        _gm.NextLevel();
        ResumeGameplay();
    }
    public void ResetGame()
    {
        _gm.GameOver();
        ResumeGameplay();
    }

    private void ResumeGameplay()
    {
        _gm.UiManager.UnHideCanvas();
        _gm.UiManager.DisableConcludePanel();
        _gm.PointerManager.CanInteract = true;
    }
    #endregion

    #endregion
}
