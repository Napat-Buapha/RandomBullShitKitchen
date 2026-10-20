using UnityEngine;

public class UI_ShopMenu : MonoBehaviour
{
    [field:SerializeField] public Shop_CardShop cardShop {get; private set;}

    public void Init()
    {
        DisableCardShop();
        cardShop.Init();
    }
    public void EnableCardShop()
    {
        cardShop.gameObject.SetActive(true);
        cardShop.SetUpShop();
        GameManager.Instance.UiManager.HideCanvas();
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
