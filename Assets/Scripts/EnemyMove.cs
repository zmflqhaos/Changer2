using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField] Sprite hitSprite;
    [SerializeField] Sprite defaultSprite;
    [SerializeField] float speed;
    [SerializeField] int maxHp;
    private int hp;
    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Vector3 dir;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        player = FindObjectOfType<PlayerMove>().transform;
        hp = maxHp;
    }

    private void Update()
    {
        MoveToPlayer();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            if (!Die()) StartCoroutine(Flash());
        }
        else if(collision.tag == "Player")
        {
            hp = 0;
            Die();
        }
    }

    private void MoveToPlayer()
    {
        dir = (player.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }

    private bool Die()
    {
        hp--;
        if (hp <= 0)
        {
            Pooling();
            return true;
        }
        return false;
    }

    private void Pooling()
    {
        StopAllCoroutines();
        hp = maxHp;
        spriteRenderer.sprite = defaultSprite;
        PoolManager.Instance.Push(gameObject);
    }

    private IEnumerator Flash()
    {
        spriteRenderer.sprite = hitSprite;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.sprite = defaultSprite;
    }
}
