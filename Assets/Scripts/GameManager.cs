using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] GameObject gameOverText;
    private PlayerMove player;
    public bool IsGameOver { get; set; }

    private void Awake()
    {
        player = FindObjectOfType<PlayerMove>();
        gameOverText.SetActive(false);
    }

    private void Update()
    {
        RestartGame();   
    }

    public void GameOver()
    {
        IsGameOver = true;
        gameOverText.SetActive(true);
    }

    private void RestartGame()
    {
        if (!IsGameOver) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            IsGameOver = false;
            PoolManager.Instance.PushAll();
            player.ReSetting();
            gameOverText.SetActive(false);
        }
    }
}
