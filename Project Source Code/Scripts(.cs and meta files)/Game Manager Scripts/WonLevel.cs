using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class WonLevel : MonoBehaviour
{
    GameObject[] pointObjects;
    public int pointsRemaining;
    // Sets initial values and ensures that only one game manager is present in the scene
    void Start()
    {
        Set();
        if(GameObject.Find("GameManager") && GameObject.Find("GameManager") != this.gameObject)
        {
            Destroy(this.gameObject);
        }
    }
    // Sets initial values
    void Set()
    {
        pointObjects = GameObject.FindGameObjectsWithTag("Point");
        pointsRemaining = pointObjects.Length;
    }
    // Checks if the level has been won
    void Update()
    {
        if (pointsRemaining <= 0) WinLevel();
    }
    // Loads the next level
    private void WinLevel()
    {
        GetComponent<SpawnEnemies>().level++;
        if(SceneManager.GetActiveScene().name == "Level_01")
            SceneManager.LoadScene("Level_02", LoadSceneMode.Single);
        else if (SceneManager.GetActiveScene().name == "Level_02")
            SceneManager.LoadScene("Level_01", LoadSceneMode.Single);
    }
    // Sets variables in the new level
    public void setGame()
    {
        GetComponent<Reset>().SetValues();
        Set();
        GameObject gMenu = GetComponent<GameOverMenu>().gameOverMenu;
        GetComponent<GameOver>().PlayerInfoCanvas = GameObject.Find("InfoCanvas");
        GameObject pMenu = GetComponent<PausedMenu>().pausedMenu;
        gMenu.SetActive(false);
        pMenu.SetActive(false);
    }
}
