using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance => instance;

    protected virtual void Awake()
    {
        if (instance == null || instance == this) instance = this as T;
        else Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (instance == this) instance = null;
    }
}
