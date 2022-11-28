using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnPoints;
    [SerializeField] string enemyName;
    private void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    private IEnumerator EnemySpawn()
    {
        int rand;
        GameObject enemy;
        while(true)
        {
            rand = Random.Range(0, spawnPoints.Length);
            enemy = PoolManager.Instance.GetPoolObject(enemyName);
            enemy.transform.SetPositionAndRotation(spawnPoints[rand].position, Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
