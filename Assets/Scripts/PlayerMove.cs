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

    private Camera mainCam;
    private float currentAttackDelay;
    private float h, v;

    private void Awake()
    {
        mainCam = Camera.main;
        Speed = speed;
    }

    private void Update()
    {
        Rotate();
        Move();
        Attack();
        Timer();
    }

    private void Rotate()
    {
        Vector3 mouse = mainCam.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg;
        playerTrs.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }

    private void Move()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        Vector3 moveTo = new Vector3(h, v).normalized;

        if(moveTo.x!=0||moveTo.y!=0)
        {
            transform.position += moveTo * Speed * Time.deltaTime;
        }
    }

    private void Attack()
    {
        if(Input.GetMouseButton(0)&&currentAttackDelay<=0)
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
