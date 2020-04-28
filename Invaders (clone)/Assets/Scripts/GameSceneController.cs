using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneController : MonoBehaviour
{
    public float playerSpeed;
    public Vector3 screenBounds;
    public EnemyController enemyPrefab;
    public ParticleSystem BGStars;

    public Text scoreText;
    public int pointTotal;

    public Text HiScoreText;
    public int highScore;

    private bool restartGame = false;
    public GameObject StartBtn;

    public void Start()
    {
        playerSpeed = 10;
        screenBounds = GetScreenBounds();

        if(BGStars != null)
        {
            BGStars.shape.scale.Set(Screen.width,0,0);
        }

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        HiScoreText.text = highScore.ToString();

        StartBtn = GameObject.Find("StartButton");
    }

    public void StartGame()
    {
        if (!restartGame)
        {
            StartBtn.SetActive(false);
            StartBtn.GetComponentInChildren<Text>().text = "Restart";
            restartGame = true;
            StartCoroutine(SpawnEnemies());
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private IEnumerator SpawnEnemies()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);

        while(true)
        {
            float horizontalPosition = UnityEngine.Random.Range(-screenBounds.x + 1, screenBounds.x - 1);
            Vector2 spawnPosition = new Vector2(horizontalPosition, screenBounds.y);

            EnemyController enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

            enemy.EnemyKilled += EnemyKilled;

            yield return wait;
        }
    }

    void EnemyKilled(int pointValue)
    {
        pointTotal += pointValue;
        scoreText.text = pointTotal.ToString();

        if(pointTotal > highScore)
        {
            PlayerPrefs.SetInt("HighScore", pointTotal);
            HiScoreText.text = pointTotal.ToString();
        }
    }

    private Vector3 GetScreenBounds()
    {
        Camera mainCamera = Camera.main;

        Vector3 screenVector = new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z);

        return mainCamera.ScreenToWorldPoint(screenVector);
    }
}
