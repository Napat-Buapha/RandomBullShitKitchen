using UnityEngine;
using UnityEngine.InputSystem;

public class PointerManager : MonoBehaviour
{
    public bool CanInteract = false;
    private GameManager _gm;
    public void Init(GameManager gameManager)
    {
        _gm = gameManager;
        CanInteract = true;
    }

    void Update()
    {
        if (!CanInteract) return;

        if (_gm.InputManager.MouseClick)
        {
            Interact(CreateRaycast());
        }
        if (_gm.InputManager.MouseHold)
        {
            PointerHolding(CreateRaycast());
        }
        if (_gm.InputManager.MouseRelease)
        {
            PointerRelease(CreateRaycast());
        }

        PointerOver(CreateRaycast());
    }

    #region Pointer Event Method
    Collider2D CreateRaycast()
    {
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

        if (hit.collider == null)
        {
            return null;
        }

        return hit.collider;
    }

    void Interact(Collider2D clickedObject)
    {
        if (clickedObject == null) return;

        if (clickedObject.TryGetComponent(out IPointerInteractAble interactAble))
        {
            interactAble.OnClick();
        }
    }

    void PointerOver(Collider2D mouseOverObject)
    {
        if (mouseOverObject == null) return;

        if (mouseOverObject.TryGetComponent(out IPointerInteractAble interactAble))
        {
            interactAble.OnPointerOver();
        }
    }

    void PointerHolding(Collider2D holdingObject)
    {
        if (holdingObject == null) return;

        if (holdingObject.TryGetComponent(out IPointerInteractAble interactAble))
        {
            interactAble.OnHold();
        }
    }

    void PointerRelease(Collider2D releaseObject)
    {
        if (releaseObject == null) return;

        if (releaseObject.TryGetComponent(out IPointerInteractAble interactAble))
        {
            interactAble.OnDrop();
        }
    }
    #endregion
}
