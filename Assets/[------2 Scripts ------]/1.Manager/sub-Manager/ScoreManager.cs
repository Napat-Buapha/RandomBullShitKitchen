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
        scoreT.text = $"Score: {_score}";
    }
}
