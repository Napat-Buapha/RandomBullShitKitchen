using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    int _score;
    [SerializeField] TMP_Text scoreT;

    public void Init()
    {
        scoreT.text = "Score: 0";
    }

    public void CalculateScore(CardCardGame_Menu menuCard, int ingredientsScore)
    {
        switch(menuCard.menuVariable.mathSymbol)
        {
            case MathSymbol.plus:
                _score += ingredientsScore + menuCard.menuVariable.amout;
            break;

            case MathSymbol.minus:
                _score += ingredientsScore - menuCard.menuVariable.amout;
            break;

            case MathSymbol.multiply:
                _score += ingredientsScore * menuCard.menuVariable.amout;
            break;

            case MathSymbol.divide:
                _score += ingredientsScore / menuCard.menuVariable.amout;
            break;

            case MathSymbol.none:
                _score += ingredientsScore;
            break;
        }

        UpdateScore();
    }

    private void UpdateScore()
    {
        scoreT.text = $"Score: {_score}";
    }
}
