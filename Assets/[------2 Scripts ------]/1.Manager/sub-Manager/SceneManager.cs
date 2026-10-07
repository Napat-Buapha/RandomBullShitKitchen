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
    [SerializeField] List<Stove> _stoves;

    public void Init(GameManager gm)
    {
        _gm = gm;
        _stoves = new List<Stove>(4);
        GenerateStoves();
    }

    #region Stove
    public void GenerateStoves()
    {
        for (int i = 0; i < startStoveCount; i++)
        {
            var stoveObject = Instantiate(stovePrefab, stovePos[i].position, Quaternion.identity, stovePos[i]);

            if (stoveObject.TryGetComponent(out Stove stoveComponent))
                _stoves.Add(stoveComponent);
        }
    }

    public void EnableAllPlaceButton()
    {
        foreach(Stove stove in _stoves)
        {
            if(!stove.isOccupied)
            {
                stove.SetPlaceButtonState(true);
            }
        }
    }

    public void DisableAllPlaceButton()
    {
        foreach(Stove stove in _stoves)
        {
            stove.SetPlaceButtonState(false);
        }
    }

    public void EnableAllAddButtons()
    {
        foreach (Stove stove in _stoves)
        {
            if(stove.isOccupied)
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
}
