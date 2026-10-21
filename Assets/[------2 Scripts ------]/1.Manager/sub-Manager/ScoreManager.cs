using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    GameManager _gm;

    int _score;

    //Goal
    int _targetScore;

    public void Init(GameManager gm)
    {
        _gm = gm;
        Reset();
    }

    public void Reset()
    {
        _score = 0;
    }


    public void SetUpTargetScore(int amout)
    {
        _targetScore = amout;
        UpdateScore();
    }

    public void CalculateScore(CardCardGame_Menu menuCard, float ingredientsScore)
    {
        switch(menuCard.menuVariable.mathSymbol)
        {
            case MathSymbol.plus:
                _score += (int)(ingredientsScore + menuCard.menuVariable.amout);
            break;

            case MathSymbol.minus:
                _score += (int)(ingredientsScore - menuCard.menuVariable.amout);
            break;

            case MathSymbol.multiply:
                _score += (int)(ingredientsScore * menuCard.menuVariable.amout);
            break;

            case MathSymbol.divide:
                _score += (int)(ingredientsScore / menuCard.menuVariable.amout);
            break;

            case MathSymbol.none:
                _score += (int)ingredientsScore;
            break;
        }

        UpdateScore();
    }

    private void UpdateScore()
    {
        if(_score >= _targetScore)
        {
            _gm.SceneManager.GameConclude(true);
        }
    
        _gm.UiManager.UpdateScore(_score, _targetScore);
    }


}
