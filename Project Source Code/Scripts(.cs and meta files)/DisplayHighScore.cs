using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] TMP_Text highScore;
    // Changes high Score text to whatever the currently saved high score is
    void Start()
    {
        highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScoreNumber", 0).ToString();
    }

}
