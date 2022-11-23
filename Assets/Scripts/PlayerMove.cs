using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoSingleton<PlayerMove>
{
    [SerializeField] private float speed;
    [SerializeField] private Transform playerTrs;
    [SerializeField] private float attackDelay;
    [SerializeField] private string bulletName;

    public float Speed { get; set; }

    private float currentAttackDelay;
    private float h, v;

    private void Awake()
    {
        Speed = speed;
    }

    private void Update()
    {
        Move();
        Attack();
        Timer();
    }

    private void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveTo = new Vector3(h, v).normalized;

        if(moveTo.x!=0||moveTo.y!=0)
        {
            playerTrs.rotation = Quaternion.FromToRotation(Vector3.up, moveTo);
            transform.position += moveTo * Speed * Time.deltaTime;
        }
    }

    private void Attack()
    {
        if(Input.GetKey(KeyCode.Space)&&currentAttackDelay<=0)
        {
            currentAttackDelay = attackDelay;
            var bullet = PoolManager.Instance.GetPoolObject(bulletName);
            bullet.transform.SetPositionAndRotation(playerTrs.position, playerTrs.rotation);
        }
    }

    private void Timer()
    {
        if(currentAttackDelay>0)
        {
            currentAttackDelay -= Time.deltaTime;
        }
        if(currentAttackDelay<0)
        {
            currentAttackDelay = 0;
        }
    }
}
