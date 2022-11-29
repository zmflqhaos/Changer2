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
        if (GameManager.Instance.IsGameOver) return;
        BackGroundMoving();
    }

    private void BackGroundMoving()
    {
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        mat.mainTextureOffset += new Vector2(h, v).normalized * Time.deltaTime;
    }
}
