using UnityEngine;

public class UiManager : MonoBehaviour
{
    GameManager _gm;

    [SerializeField] private UI_SelectionMenu selectionMenu;
    public UI_SelectionMenu SelectionMenu => selectionMenu;
    
    
    public void Init(GameManager gm)
    {
        _gm = gm;

        selectionMenu.Init();
    }


}
