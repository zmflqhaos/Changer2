using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private float attackDelay;
    [SerializeField] private string bulletName;
    [SerializeField] private int maxHp;

    public float Speed { get; set; }

    private bool isInvinsible;
    private int hp;
    private Camera mainCam;
    private float currentAttackDelay;
    private float h, v;

    private void Awake()
    {
        mainCam = Camera.main;
        Speed = speed;
        hp = maxHp;
    }

    private void Update()
    {
        if(GameManager.Instance.IsGameOver) return;
        Rotate();
        Move();
        Attack();
        Timer();
    }

    private void Rotate()
    {
        Vector3 mouse = mainCam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        playerSprite.transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveTo = new Vector3(h, v).normalized;

        if (moveTo.x != 0 || moveTo.y != 0)
        {
            transform.position += moveTo * Speed * Time.deltaTime;
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButton(0) && currentAttackDelay <= 0)
        {
            currentAttackDelay = attackDelay;
            var bullet = PoolManager.Instance.GetPoolObject(bulletName);
            bullet.transform.SetPositionAndRotation(transform.position, playerSprite.transform.rotation);
        }
    }

    private void Timer()
    {
        if (currentAttackDelay > 0)
        {
            currentAttackDelay -= Time.deltaTime;
        }
        if (currentAttackDelay < 0)
        {
            currentAttackDelay = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "enemy" || isInvinsible) return;
        StartCoroutine(PlayerHit());
    }

    private IEnumerator PlayerHit()
    {
        hp--;
        if (hp <= 0) Die();
        else
        {
            isInvinsible = true;
            for (int i = 0; i < 3; i++)
            {
                playerSprite.color = new Color(0, 0, 0, 0);
                yield return new WaitForSeconds(0.1f);
                playerSprite.color = new Color(255, 255, 255, 255);
                yield return new WaitForSeconds(0.1f);
            }
            isInvinsible = false;
        }
    }

    private void Die()
    {
        GameManager.Instance.GameOver();
    }

    public void ReSetting()
    {
        hp = maxHp;
    }
}
