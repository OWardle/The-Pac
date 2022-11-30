using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField] public int lives = 3;
    public bool gameOver = false;
    public GameObject PlayerInfoCanvas;

    // Checks if the game should end
    private void Update()
    {
        if (lives <= 0) EndGame();
    }
    // Sets all appropriate variables
    public void EndGame()
    {
        gameOver = true;
        GetComponent<PausedMenu>().isPaused = true;
        GetComponent<GameOverMenu>().setOn();
        int score = this.GetComponent<Score>().score;
        PlayerInfoCanvas.SetActive(false);
        if (score > PlayerPrefs.GetInt("HighScoreNumber", -1))
        {
            PlayerPrefs.SetInt("HighScoreNumber", score);
            PlayerPrefs.SetString("HighScoreName", PlayerPrefs.GetString("CurrentName", "EMPTY"));
        }
        Time.timeScale = 0f;
    }
    // A call to remove a life from lifecount
    public void decrementLives()
    {
        lives--;
    }
}
