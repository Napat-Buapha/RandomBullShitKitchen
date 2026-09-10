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

    public void PlaceKitchenWare(CardCardGame_KitchenWare kitchenWare)
    {
        foreach(Stove stove in _stoves)
        {
            if(!stove.isOccupied)
            {
                Apply(kitchenWare, stove);
                break;
            }
        }
    }
    private static void Apply(CardCardGame_KitchenWare kitchenWare, Stove stove)
    {
        stove.PlaceKitchenWare(kitchenWare);
        GameManager.Instance.HandsManager.Discard(kitchenWare);
    }

    public void EnableAllAddButtons()
    {
        foreach (Stove stove in _stoves)
        {
            if(stove.isOccupied)
            stove.EnableAddButton();
        }
    }

    public void DisableAllAddButtons()
    {
        foreach (Stove stove in _stoves)
        {
            stove.DisableAddButton();
        }
    }

    #endregion
}
