using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayCurrentScore : MonoBehaviour
{
    [SerializeField] TMP_Text currentScore;
    GameObject GM;
    // Finds the game manager to access the current score
    void Start()
    {
        GM = GameObject.FindGameObjectWithTag("GM");
    }
    // Changes current Score text to whatever the current score is
    private void Update()
    {
        currentScore.text = "Your Score: " + GM.GetComponent<Score>().score.ToString();
    }
}
