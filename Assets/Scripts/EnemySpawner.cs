using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] string enemyName;

    private float timer;
    private int rand;
    private GameObject enemy;

    private void Update()
    {
        if(GameManager.Instance.IsGameOver) return;
        EnemySpawn();
        Timer();
    }

    private void EnemySpawn()
    {
        if (timer <= 0.5f) return;
        rand = Random.Range(0, spawnPoints.Length);
        enemy = PoolManager.Instance.GetPoolObject(enemyName);
        enemy.transform.SetPositionAndRotation(spawnPoints[rand].position, Quaternion.identity);
        timer = 0;
    }

    private void Timer()
    {
        timer += Time.deltaTime;
    }
}
