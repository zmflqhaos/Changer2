using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/PoolList")]
public class PoolList : ScriptableObject
{
    public PoolObject[] poolObjects;
}

[Serializable]
public class PoolObject
{
    public string name;
    public GameObject gameObject;
    public int defaultCount;
}
