using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;

    public bool MouseClick { get; private set; }

    private InputAction clickAction;

    public void OnEnable()
    {
        clickAction = inputActionAsset.FindAction("Interact");

        clickAction.Enable();
    }

    public void OnDisable()
    {
        clickAction.Disable();
    }

    void Update()
    {
        MouseClick = clickAction.WasPressedThisFrame();
    }
}
