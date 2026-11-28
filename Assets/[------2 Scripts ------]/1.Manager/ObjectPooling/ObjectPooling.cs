using UnityEngine;
using System.Collections.Generic;

public class ObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class PoolItem
    {
        public string Pkey;
        public GameObject Pobj;
        public int Psize;
    }

    private GameManager _gm;

    public List<PoolItem> ListPool = new List<PoolItem>();
    private Dictionary<string, Queue<GameObject>> pools = new();

    public void Init(GameManager gm)
    {
        _gm = gm;
        CreateList();
    }

    protected void CreateList()
    {
        foreach (var item in ListPool)
        {
            if (item.Pobj == null) continue;
            pools[item.Pkey] = new Queue<GameObject>();

            for (int i = 0; i < item.Psize; i++)
                Create(item.Pobj);
        }
    }
    
    protected void Create(GameObject obj)
    {
        if (obj == null) return;

        var o = Instantiate(obj, Vector3.zero, Quaternion.identity);
        o.name = obj.name;
        o.transform.SetParent(gameObject.transform);

        if (pools.ContainsKey(o.name))
        {
            pools[obj.name].Enqueue(o);
        }
        else
        {
            Debug.Log(o.name + "not have in pools");
            pools[obj.name] = new Queue<GameObject>();
            pools[obj.name].Enqueue(o);
        }

        o.SetActive(false);
    }

    public virtual GameObject Spawn(GameObject objName , Vector3 position = default, Quaternion rotation = default, Transform parent = default)
    {
        PoolItem item = ListPool.Find(obj => obj.Pkey == objName.name);// Find gameObj in list

        if (item == null) return null;

        var queue = pools[item.Pkey];
        if (queue.Count <= 0) Create(item.Pobj);// Count check

        GameObject obj = pools[objName.name].Dequeue();
        obj.SetActive(true);
        obj.transform.SetParent(parent == default ? null : parent);
        obj.transform.SetPositionAndRotation(position , rotation == default ? Quaternion.identity : rotation);

        return obj;
    }

    public virtual void Return(GameObject obj)
    {
        if (obj == null) return;

        if (pools.ContainsKey(obj.name))
        {
            pools[obj.name].Enqueue(obj);
            obj.transform.SetParent(gameObject.transform);
            obj.transform.SetLocalPositionAndRotation(Vector3.zero,Quaternion.identity);
            obj.SetActive(false);
        }
        else Destroy(obj);
    }
}
