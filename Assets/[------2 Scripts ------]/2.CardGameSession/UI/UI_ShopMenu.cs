using TMPro;
using UnityEngine;

public class UI_ShopMenu : MonoBehaviour
{
    [field:SerializeField] public Shop_CardShop cardShop {get; private set;}
    [SerializeField] TMP_Text coinAmout;

    public void Init()
    {
        DisableCardShop();
        cardShop.Init();
    }

    public void EnableCardShop()
    {
        cardShop.gameObject.SetActive(true);
        cardShop.SetUpShop();
        GameManager.Instance.UiManager.UpdateGoldAmout();

        GameManager.Instance.UiManager.HideCanvas();
        GameManager.Instance.UiManager.DisableConcludePanel();
    }

    public void DisableCardShop()
    {
        cardShop.gameObject.SetActive(false);
    }

    public void EndTrasaction()
    {
        cardShop.ClearShop();
        GameManager.Instance.UiManager.UnHideCanvas();
        GameManager.Instance.SceneManager.NextLevel();
    }


}
