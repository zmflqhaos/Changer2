using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] float speed;
    private Transform player;
    private Vector3 dir;

    private void Start()
    {
        player = FindObjectOfType<PlayerMove>().transform;
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void MoveToPlayer()
    {
        dir = (player.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    private void Pooling()
    {
        PoolManager.Instance.Push(gameObject);
    }
}
