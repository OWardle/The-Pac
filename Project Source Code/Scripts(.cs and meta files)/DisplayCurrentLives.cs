using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCurrentLives : MonoBehaviour
{
    [SerializeField] TMP_Text currentLives;
    GameObject GM;
    // Finds the game manager to access the current lifecount
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");
    }
    // Changes current lives text to whatever the current lifecount is
    private void Update()
    {
        currentLives.text = "Lives: " + GM.GetComponent<GameOver>().lives.ToString();
    }
}
