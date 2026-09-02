using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Stove_IngredientSlot : MonoBehaviour
{
    [SerializeField] Image ingredintSpriteI;

    void OnEnable()
    {
        ingredintSpriteI.sprite = null;
    }

    public void SetupSprite(Sprite ingredientSprite)
    {
        ingredintSpriteI.sprite = ingredientSprite;
    }
}
