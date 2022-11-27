using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float deleteTime;

    private void OnEnable()
    {
        Invoke("Pooling", deleteTime);
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void Pooling()
    {
        PoolManager.Instance.Push(gameObject);
    }
}
