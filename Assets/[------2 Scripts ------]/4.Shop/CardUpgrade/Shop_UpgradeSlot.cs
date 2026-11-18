using UnityEngine;
using UnityEngine.EventSystems;

public class Shop_UpgradeSlot : MonoBehaviour, IDropReceiver
{

    Shop_CardUpgrade _cu;
    [field:SerializeField] public CardCardGame_Base cardInSlot {get; private set;}
    [SerializeField] int upgradeCost;


    public void Init(Shop_CardUpgrade cu)
    {
        _cu = cu;
    }

    public void OnShopDisable()
    {
        if(cardInSlot == null) return;

        _cu.ReceiveNewCard(cardInSlot.baseCardRef);
        
        Destroy(cardInSlot.gameObject);
        cardInSlot = null;
    }

    public void RemoveCardFromSlot()
    {
        cardInSlot = null;
    }

    public bool DropReceive(CardCardGame_Base card)
    {
        if(cardInSlot != null) return false;

        if (card.baseCardRef.isUpgrade)
        {
            return false;
        }

        cardInSlot = card;
        Snap();

        return true;
    }

    void Snap()
    {
        _cu.RemoveCardFromList(cardInSlot);
        cardInSlot.transform.position = transform.position;
    }

    //Method used by Button event 
    public void Upgrade()
    {
        if (cardInSlot == null) return;

        if(cardInSlot.baseCardRef.upgradedVersion == null)
        {
            Debug.Log("No Upgrade Version Yet!");
            return;
        }

        if(!GameManager.Instance.ResourceManager.PayGold(upgradeCost))
        {
            Debug.Log("Out of Money!");
            return;
        }

        GameManager.Instance.UiManager.UpdateGoldAmout();
        _cu.ReceiveNewCard(cardInSlot.baseCardRef.upgradedVersion);
        Destroy(cardInSlot.gameObject);
    }
}
