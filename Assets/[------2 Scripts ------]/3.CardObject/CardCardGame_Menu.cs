using System.Collections.Generic;

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardCardGame_Menu : CardCardGame_Base , IPointerInteractAble
{
    public Menu_Variable menuVariable { get; private set; }


    public int score { get; private set; }

    [Header("Menu Card Visual")]
    [SerializeField] private TMP_Text baseIngredientScoreT;
    [SerializeField] private TMP_Text menuMultiplierT;


    public override void Init(Card card)
    {       
        base.Init(card);
        ApplyKitchenWareVisual();
    }

    public void ApplyBaseScore(int IngredientsScore)
    {
        score = IngredientsScore;
        UpdateBaseScoreText();
    }

    private void UpdateBaseScoreText()
    {
        baseIngredientScoreT.text = score.ToString();
    }

    public void ApplyKitchenWareVisual()
    {
        char sym = GetMathSymbol(menuVariable.mathSymbol);

        menuMultiplierT.text =  sym + menuVariable.amout.ToString();
    }

    public char GetMathSymbol(MathSymbol symbol)
    {
        switch(symbol)
        {
            case MathSymbol.plus:
                return '+';
            case MathSymbol.minus:
                return '-';
            case MathSymbol.multiply:
                return '×';
            case MathSymbol.divide:
                return '÷';
            default:
                return ' ';
        }
    }

    protected override void RecordCardData(Card card)
    {
        base.RecordCardData(card);
        if(card is Card_Menu menuCard)
        menuVariable = menuCard.menuVariable;
    }

    public void OnClick()
    {
        
    }

    public void OnPointerOver()
    {
        
    }
}


