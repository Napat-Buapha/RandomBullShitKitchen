using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UI_SelectionMenu : MonoBehaviour
{
    [field: SerializeField] public Button resolveButton { get; private set; }
    [field: SerializeField] public Button cancleButton { get; private set; }
    [SerializeField] GameObject buttons;
    public void Init()
    {
        GameManager.Instance.OnGameStateChange.AddListener(OnStateChange);
        CloseMenu();
    }

    public void OnStateChange(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.SelectingHand || gameState == GameManager.GameState.SelectingBin)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    public void UpdateResolveCondition(int cardAmout)
    {
        SelectionCondition selectCon = GameManager.Instance.currentSelectionCondition;
        {
            if (GameManager.Instance.CardEffectManager.currentEffect is Effect_CardSelect effectRef)
            {

                resolveButton.interactable = effectRef.selectedList.Count == selectCon.amout;
            }
        }
    }

    public void SubscribeToEffect()
    {
        SelectionCondition selectCon = GameManager.Instance.currentSelectionCondition;
        if (!selectCon.needToBeFull)
        {
            resolveButton.interactable = true;
            return;
        }
        
        if (GameManager.Instance.CardEffectManager.currentEffect is Effect_CardSelect effectRef)
        {
            Debug.Log("Test");
            resolveButton.interactable = false;
            effectRef.OnCardAmoutChange.AddListener(UpdateResolveCondition);
        }
    }
    public void OpenMenu()
    {
        resolveButton.onClick.AddListener(CloseMenu);
        buttons.SetActive(true);

        SubscribeToEffect();
    }
    public void CloseMenu()
    {
        resolveButton.onClick?.RemoveAllListeners();
        buttons.SetActive(false);
    }


}
