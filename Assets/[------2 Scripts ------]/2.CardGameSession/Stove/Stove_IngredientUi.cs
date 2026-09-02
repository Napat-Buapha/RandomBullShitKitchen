using System;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;

public class Stove_IngredientUi : MonoBehaviour
{
    [SerializeField] GameObject container;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] Stove_IngredientSlot[] slots;


    public void EnableIngredientUI(CardCardGame_KitchenWare kitchenWare)
    {
        gameObject.SetActive(true);
        SpawnIngredientSlotDisplayer(kitchenWare.ingredientSlot);
        Invoke("RefreshVerticalGroup" ,0.1f);
    }

    public void DisableIngredientUI()
    {
        if(slots != null)
        {
            while(slots.Length > 0)
            {
                Destroy(slots[0]);
            }
        }
        gameObject.SetActive(false);
    }

    void SpawnIngredientSlotDisplayer(int ingredientSlotAmout)
    {
        slots = new Stove_IngredientSlot[ingredientSlotAmout];

        for (int i = 0; i < ingredientSlotAmout; i++)
        {
            var slot = Instantiate(slotPrefab, container.transform);
            if (slot.TryGetComponent(out Stove_IngredientSlot ingredientSlot))
            {
                slots[i] = ingredientSlot;
            }
        }

    }

    public void UpdateSlots(IngredientCharacteristics[] ingredientArray)
    {
        for(int i = 0; i < ingredientArray.Length; i++)
        {
            if(ingredientArray[i].ingredientName != null)
            {
                slots[i].SetupSprite(ingredientArray[i].ingredientSprite);
            }
        }    
    }
}
