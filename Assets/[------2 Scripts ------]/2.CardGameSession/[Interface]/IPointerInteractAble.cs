using UnityEditor.Embree;
using UnityEngine;

public interface IPointerInteractAble
{
    public void OnPointerOver();
    public void OnClick();
    public void OnHold();
    public void OnDrop();
}
