using UnityEngine;

public class TestScript :  MonoBehaviour, IPointerInteractAble
{
    public void OnClick()
    {
        Debug.Log("Click");
    }

    public void OnPointerOver()
    {
        Debug.Log("Over");
    }


}
