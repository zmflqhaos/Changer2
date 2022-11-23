using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float speed;

    private void OnEnable()
    {
        Invoke("Pooling", 2f);
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
