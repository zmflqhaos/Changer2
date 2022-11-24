using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMove : MonoBehaviour
{
    private Material mat;

    float h, v;

    private void Awake()
    {
        mat = GetComponent<SpriteRenderer>().material;
    }

    private void Update()
    {
        BackGroundMoving();
    }

    private void BackGroundMoving()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        mat.mainTextureOffset += new Vector2(h, v).normalized * (PlayerMove.Instance.Speed/15) * Time.deltaTime;
    }
}
