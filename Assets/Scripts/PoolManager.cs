using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoSingleton<PoolManager>
{
    [SerializeField] private PoolList poolList;

    Dictionary<string, GameObject> poolingList = new Dictionary<string, GameObject>();

    GameObject _object;

    private void Awake()
    {
        GameObject poolObject;
        foreach (PoolObject pool in poolList.poolObjects)
        {
            poolingList.Add(pool.name, pool.gameObject);
            for(int i=0; i<pool.defaultCount; i++)
            {
                poolObject = Instantiate(pool.gameObject);
                poolObject.name = pool.name;
                poolObject.transform.SetParent(gameObject.transform);
                poolObject.SetActive(false);
            }
        }
    }

    public GameObject GetPoolObject(string name)
    {
        _object = transform.Find(name)?.gameObject;
        if (_object == null)
        {
            _object = Instantiate(poolingList[name]);
            _object.name = name;
        }
        _object.SetActive(true);
        _object.transform.SetParent(null);
        return _object;
    }

    public void Push(GameObject pushObject)
    {
        pushObject.SetActive(false);
        pushObject.transform.SetParent(gameObject.transform);
    }
}