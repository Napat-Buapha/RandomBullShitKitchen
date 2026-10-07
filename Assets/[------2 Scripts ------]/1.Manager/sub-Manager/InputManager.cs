using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;

    public bool MouseClick { get; private set; }
    public bool MouseHold { get; private set; }
    public bool MouseRelease { get; private set; }

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
        MouseHold = HoldingInputCalculator();
        MouseRelease = clickAction.WasReleasedThisFrame();
    }

    float currentHoldingDura = 0;
    [SerializeField] float triggerHoldingDura = 0.2f;
    bool HoldingInputCalculator()
    {
        if(clickAction.IsPressed())
        {
            currentHoldingDura += Time.unscaledDeltaTime;
            if(currentHoldingDura >= triggerHoldingDura)
            {
                return true;                
            }

            return false;
        }
        else
        {
            ResetHoldingDuration();
            return false;
        }
    }

    public void ResetHoldingDuration()
    {
        currentHoldingDura = 0;
    }
}
