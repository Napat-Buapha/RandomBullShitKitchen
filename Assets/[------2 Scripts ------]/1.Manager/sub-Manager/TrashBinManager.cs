using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class TrashBinManager : MonoBehaviour
{
    GameManager _gm;
    public List<CardCardGame_Base> discardPile { get; private set; }
    public List<CardDiscardPile_Event> discordPileEvents { get; private set; }

    [Header("Ui Manager")]
    bool _canInteractUi;
    [SerializeField] Dictionary<CardType, GameObject> cardPrefabs;
    [SerializeField] GameObject trashBinUiPanel;
    [SerializeField] GameObject cardContainer;


    public void Init(GameManager gm)
    {
        gm.OnGameStateChange.AddListener(OnGameStateChange);

        _canInteractUi = true;
        _gm = gm;
        discardPile = new List<CardCardGame_Base>();
        discordPileEvents = new List<CardDiscardPile_Event>();
        DisableDiscardPilePanel();
    }

    void OnGameStateChange(GameManager.GameState currentGameState)
    {
        switch (currentGameState)
        {
            case GameManager.GameState.SelectingBin:
                SetHandSelect();
                break;

            default:
                SetNormal();
                break;
        }
    }

    private void SetHandSelect()
    {
        _canInteractUi = false;
        foreach (var card in discordPileEvents)
        {
            card.OnClickEvent.AddListener(SelectCardForEffect);
        }
    }

    private void SetNormal()
    {
        _canInteractUi = true;
        foreach (var card in discordPileEvents)
        {
            card.OnClickEvent?.RemoveListener(SelectCardForEffect);
        }
    }

    void SelectCardForEffect(CardCardGame_Base card)
    {
        bool isApply = _gm.CardEffectManager.ApplyCardDataToEffect(card);

        if (isApply)
        {
            card.OnSelect();
        }
        else
        {
            card.OnDeSelect();
        }
    }

    public void Receive(Card card)
    {
        GameObject _card = Instantiate(cardPrefabs[card.cardType], cardContainer.transform);
        if (_card.TryGetComponent(out CardCardGame_Base cardBase))
        {
            discardPile.Add(cardBase);
            cardBase.Init(card);

            discordPileEvents.Add(_card.GetComponent<CardDiscardPile_Event>());
        }

    }

    public Card Remove(CardCardGame_Base card)
    {
        discardPile.Remove(card);
        discordPileEvents.Remove(card.gameObject.GetComponent<CardDiscardPile_Event>());

        Destroy(card.gameObject);
        return card.baseCardRef;
    }

    public void SwitchDiscardPilePanelState()
    {
        if (!_canInteractUi) return;

        if (trashBinUiPanel.activeInHierarchy)
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
        if (!_canInteractUi) return;
        trashBinUiPanel.SetActive(true);
    }

    public void DisableDiscardPilePanel()
    {
        if (!_canInteractUi) return;
        trashBinUiPanel.SetActive(false);
    }


}
