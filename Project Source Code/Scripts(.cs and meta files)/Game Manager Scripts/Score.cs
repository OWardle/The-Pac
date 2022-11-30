using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public int score;
    // Score needs to be consistent throughout all levels, so do not get rid of this game object
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    // Sets initial values
    void Start()
    {
        score = 0;
    }
}
