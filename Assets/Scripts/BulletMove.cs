using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : PoolObject
{
    [SerializeField] private float speed;
    [SerializeField] private float deleteTime;

    private void OnEnable()
    {
        Invoke("Pooling", deleteTime);
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver) return;
        Move();
    }

    private void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    public override void Pooling()
    {
        PoolManager.Instance.Push(gameObject);
    }
}
