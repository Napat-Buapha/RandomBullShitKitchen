using TMPro;
using UnityEngine;

public class UI_ShopMenu : MonoBehaviour
{
    [field:SerializeField] public Shop_CardShop cardShop {get; private set;}
    [field:SerializeField] public Shop_CardUpgrade upgradeShop {get; private set;}
    [SerializeField] TMP_Text coinAmout;

    public void Init()
    {
        DisableShop();
        cardShop.Init();
        upgradeShop.Init();
    }

    public void EnableCardShop()
    {
        SetShopPanelActibeStatus(cardShop.gameObject.GetComponent<CanvasGroup>() , true);
        //SetShopPanelActibeStatus(upgradeShop.gameObject.GetComponent<CanvasGroup>() , true);

        cardShop.SetUpShop();
        upgradeShop.SetUpShop();
        
        GameManager.Instance.UiManager.UpdateGoldAmout();
        GameManager.Instance.UiManager.HideCanvas();
        GameManager.Instance.UiManager.DisableConcludePanel();
    }

    public void DisableShop()
    {
        SetShopPanelActibeStatus(cardShop.gameObject.GetComponent<CanvasGroup>() , false);
        SetShopPanelActibeStatus(upgradeShop.gameObject.GetComponent<CanvasGroup>() , false);
    }

    public void SetShopPanelActibeStatus(CanvasGroup canvasGroup, bool activeStatus)
    {
        if(activeStatus)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    // Button Event //

    public void EndTrasaction()
    {
        cardShop.ClearShop();
        upgradeShop.ClearShop();
        GameManager.Instance.UiManager.UnHideCanvas();
        GameManager.Instance.SceneManager.NextLevel();
    }

    public void OpenCardShop()
    {
        DisableShop();
        SetShopPanelActibeStatus(cardShop.gameObject.GetComponent<CanvasGroup>() , transform);
    }

    public void OpenUpgradeShop()
    {
        DisableShop();
        SetShopPanelActibeStatus(upgradeShop.gameObject.GetComponent<CanvasGroup>() , transform);
    }


}
