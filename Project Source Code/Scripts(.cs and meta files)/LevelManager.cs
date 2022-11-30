using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    GameObject GM;
    // Ensures that with every level, the game is properly set
    private void Start()
    {
        GM = GameObject.Find("GameManager");
        spawn();
        GM.GetComponent<WonLevel>().setGame();
    }
    // Ensures enemies are spawned
    void spawn()
    {
        GM.GetComponent<SpawnEnemies>().Spawn();
    }
}
