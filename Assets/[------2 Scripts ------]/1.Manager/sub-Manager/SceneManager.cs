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

    [Header("Time Point Customizer")]
    [SerializeField] private int maxTimePoint;
    private int _currentTimePoint;


    public void Init(GameManager gm)
    {
        _gm = gm;
        _stoves = new List<Stove>(4);
        GenerateStoves();
    }
    #region TimePoint
    public void ResetTimePoint()
    {
        _currentTimePoint = maxTimePoint;
    }

    public void ReduceTimePoint(int amout)
    {
        _currentTimePoint -= amout;

        if(_currentTimePoint <= 0)
        {
            _gm.TurnManager.TurnEnd();
        }
    }
    #endregion

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
    #endregion
}
