using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCanManager : MonoBehaviour
{    
    GameManager _gm;
    public List<CardCardGame_Base> discardPile {get; private set;}
    [Header("Ui Manager")]    
    [SerializeField] Dictionary<CardType,GameObject> cardPrefabs;
    [SerializeField] GameObject trashBinUiPanel;
    [SerializeField] GameObject cardContainer;



    public void Init(GameManager gm)
    {
        _gm = gm;
        discardPile = new List<CardCardGame_Base>();
        DisableDiscardPilePanel();
    }

    public void Receive(Card card)
    {
        GameObject _card = Instantiate(cardPrefabs[card.cardType], cardContainer.transform);
        if(_card.TryGetComponent(out CardCardGame_Base cardBase))
        {
            discardPile.Add(cardBase);
            cardBase.Init(card);
        }
    }

    public Card Remove(CardCardGame_Base card)
    {
        discardPile.Remove(card);
        Destroy(card.gameObject);
        return card.baseCardRef;
    }

    public void SwitchDiscardPilePanelState()
    {
        if(trashBinUiPanel.activeInHierarchy)
        {
            DisableDiscardPilePanel();
        }
        else
        {
            EnableDiscardPilePanel();
        }
    }

    public void EnableDiscardPilePanel()
    {
        trashBinUiPanel.SetActive(true);
    }

    public void DisableDiscardPilePanel()
    {
        trashBinUiPanel.SetActive(false);
    }


}
