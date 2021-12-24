using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string key;
        public GameObject prefab;
        public Stack<GameObject> instances = new Stack<GameObject>();
        public bool prewarm;
        public int prewarmCount;

        public GameObject Spawn()
        {
            if (instances.Count > 0)
            {
                return instances.Pop();
            }
            else
            {
                GameObject go = Instantiate(prefab);
                go.name = prefab.name;
                return go;
            }
        }

        public void Despawn(GameObject go)
        {
            instances.Push(go);
        }

        public void Prewarm()
        {
            if (!prewarm) return;

            for (int i = 0; i < prewarmCount; i++)
                instances.Push(Instantiate(prefab));
        }
    }

    private static PoolingManager _instance;

    [SerializeField] private Pool[] _pools;
    [SerializeField] private Dictionary<string, Pool> _poolDic = new Dictionary<string, Pool>();

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        for (int i = 0; i < _pools.Length; i++)
            _poolDic.Add(_pools[i].key, _pools[i]);        
    }


    public static GameObject Spawn(string key, Transform parent)
    {
        bool valid = CheckValidity(key);

        if (valid)
        {
            GameObject go = _instance._poolDic[key].Spawn();
            go.transform.SetParent(parent);
            go.transform.localScale = Vector3.one;
            return go;
        }
        else
        {
            return null;            
        }
    }

    public static void Despawn(GameObject go)
    {
        bool valid = CheckValidity(go.name);
        if (valid) _instance._poolDic[go.name].Despawn(go);
    }

    private static bool CheckValidity(string key)
    {
        if (_instance._poolDic.ContainsKey(key))
        {
            return true;
        }
        else
        {
            Debug.LogError("Wrong key");
            return false;
        }
    }
}
