using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    public GameObject gameOverMenu;

    // Ensures the menu is off upon load
    public void Set()
    {
        gameOverMenu.SetActive(false);
    }
    // Turns on the menu
    public void setOn()
    {
        gameOverMenu.SetActive(true);
    }
    // Returns to the main menu screen and sets proper variables
    public void MainMenu()
    {
        Time.timeScale = 1f;
        GetComponent<GameOver>().gameOver = false;
        Destroy(this.gameObject);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    // Quits the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
