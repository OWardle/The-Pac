using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausedMenu : MonoBehaviour
{

    public bool isPaused;

    public GameObject pausedMenu;
    
    // Don't display pause menu on start
    public void Set()
    {
        pausedMenu.SetActive(false);
    }

    // Checks for escape button for pause menu
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape) && !GetComponent<GameOver>().gameOver) {
        
            if (isPaused) {
            
                ResumeGame();
                
            } else {
            
                PausedGame();
                
            }
        }
    }
    
    // Pauses the game
    public void PausedGame() {
    
        pausedMenu.SetActive(true);
        
        Time.timeScale = 0f;
        
        isPaused = true;
    }
    
    // Resumes the game
    public void ResumeGame()
    {
        pausedMenu.SetActive(false);
        
        Time.timeScale = 1f;
        
        isPaused = false;
        
    }
    
    // Quits the game
    public void QuitGame() {

        Application.Quit();
    }

}

