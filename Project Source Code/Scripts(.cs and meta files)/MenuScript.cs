using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    [SerializeField] private TMP_Text DisplayText;
    [SerializeField] private TMP_Text SoundEffects;
    [SerializeField] private TMP_Text Music;
    [SerializeField] private GameObject Menu;
    [SerializeField] private GameObject Setting;
    [SerializeField] private AudioSource Source;
    [SerializeField] private AudioClip MenuMusic;

    // This method ensures that all initial values are set
    void Start()
    {
        PlayerPrefs.SetInt("Music", PlayerPrefs.GetInt("Music", 1));
        PlayerPrefs.SetInt("SoundEffects", PlayerPrefs.GetInt("SoundEffects", 1));
        DisplayText.text = "High Score: " + PlayerPrefs.GetString("HighScoreName", "EMPTY") + ": " 
            + PlayerPrefs.GetInt("HighScoreNumber", 0).ToString();
        Music.text = "Menu Music: " + (PlayerPrefs.GetInt("Music", 1) == 1 ? "ON" : "OFF");
        SoundEffects.text = "Sound Effects: " + (PlayerPrefs.GetInt("SoundEffects", 1) == 1 ? "ON" : "OFF");
        if (PlayerPrefs.GetInt("Music", 1) == 1)
        {
            Source.Play();
            //Source.PlayOneShot(MenuMusic);
            Source.loop = true;
        }
        if(GameObject.Find("GameManager") != null)
            Destroy(GameObject.Find("GameManager"));
    }

    // Updates music preference on computer
    public void OnMusicButtonPress()
    {
        PlayerPrefs.SetInt("Music", PlayerPrefs.GetInt("Music", 1) == 1 ? 0 : 1);
        Music.text = "Music: " + (PlayerPrefs.GetInt("Music", 1) == 1 ? "ON" : "OFF");
        if(PlayerPrefs.GetInt("Music", 1) != 1)
        {
            Source.Stop();
        }
        else
        {
            Source.Play();
            Source.loop = true;
        }
    }
    // Updates sound effect preference on computer
    public void OnSoundEffectsButtonPress()
    {
        PlayerPrefs.SetInt("SoundEffects", PlayerPrefs.GetInt("SoundEffects", 1) == 1 ? 0 : 1);
        SoundEffects.text = "Sound Effects: " + (PlayerPrefs.GetInt("SoundEffects", 1) == 1 ? "ON" : "OFF");
    }
    // Loads level_01
    public void NewGame()
    {
        SceneManager.LoadScene("Level_01", LoadSceneMode.Single);
    }
    // Opens settings menu
    public void Settings()
    {
        Menu.SetActive(false);
        Setting.SetActive(true);
    }
    // Returns to main menu
    public void Return()
    {
        Menu.SetActive(true);
        Setting.SetActive(false);
    }
    // Quits the application
    public void QuitGame()
    {
        Application.Quit();
    }
}
